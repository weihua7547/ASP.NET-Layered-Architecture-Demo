using Badminton.DataAccess;
using Badminton.Model.Abstract;
using Badminton.Model.Log;
using Badminton.Contract;
using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
//using FileInfo = Badminton.Model.FileInfo;
namespace Badminton.DataAccess
{
    public class BadmintonDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly IUserContext _userContext;
        public BadmintonDbContext(DbContextOptions<BadmintonDbContext> options, IPasswordService passwordService, IConfiguration configuration, IUserContext userContext) : base(options)
        {
            _passwordService = passwordService;
            _configuration = configuration;
            _userContext = userContext;
        }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<ApiAccessLog> ApiAccessLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<DataChangeLog> DataChangeLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadmintonDbContext).Assembly);
            modelBuilder.SetDefaultUserSeed(_passwordService, _configuration);
            //modelBuilder.SetDefaultSystemManagerSeed();
        }
        public override int SaveChanges()
        {
            RecordChangesModify();

            var addedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Entity);

            foreach (var entry in addedEntities)
            {
                if (entry.Entity is Entity entity)
                {
                    // 設定 Creator 欄位值為 _userContext.UserId
                    entity.CreatorId = _userContext.UserId ?? 0;
                }
            }
            //取出修改的entity並更新修改人與修改時間
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is Entity);
            foreach (var entry in modifiedEntities)
            {
                if (entry.Entity is not Entity entity) continue;
                entity.UpdaterId = _userContext.UserId ?? 0;
                entity.UpdatedDateTime = DateTime.UtcNow;
                entry.Property("Updater").IsModified = true;
                entry.Property("UpdatedDateTime").IsModified = true;
            }

            RecordChangesCreate(addedEntities);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RecordChangesModify();

            var addedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Entity)
                .ToList();

            foreach (var entry in addedEntities)
            {
                if (entry.Entity is Entity entity)
                {
                    // 設定 Creator 欄位值為 _userContext.UserId
                    entity.CreatorId = _userContext.UserId ?? 0;
                }
            }
            //取出修改的entity並更新修改人與修改時間
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is Entity);

            foreach (var entry in modifiedEntities)
            {
                if (entry.Entity is not Entity entity) continue;
                entity.UpdaterId = _userContext.UserId ?? 0;
                entity.UpdatedDateTime = DateTime.UtcNow;
                entry.Property("Updater").IsModified = true;
                entry.Property("UpdatedDateTime").IsModified = true;
            }
            RecordChangesCreate(addedEntities);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void RecordChangesModify()
        {
            var changeLogs = new List<DataChangeLog>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not Entity)
                {
                    continue;
                }

                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var tableName = entry.Metadata.GetTableName() ?? throw new Exception("資料表定義讀取失敗，可能是沒有設定主鍵");

                    var primaryKey = entry.Metadata.FindPrimaryKey() ?? throw new Exception("資料表定義讀取失敗，可能是沒有設定主鍵");

                    var keyName = primaryKey.Properties.Select(p => p.Name).First();
                    var keyValue = entry.CurrentValues[keyName]?.ToString();
                    int? key = null;
                    if (keyValue != null && entry.State != EntityState.Added)
                    {
                        key = int.Parse(keyValue);
                    }

                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    var changeLog = new DataChangeLog
                    {
                        TableName = tableName,
                        TablePKValue = key,
                        HandleType = entry.State switch
                        {
                            EntityState.Added => DataChangeLog.HandleTypes.Create,
                            EntityState.Modified => DataChangeLog.HandleTypes.Update,
                            EntityState.Deleted => DataChangeLog.HandleTypes.Delete,
                            _ => DataChangeLog.HandleTypes.None
                        },
                        HandleDateTime = DateTime.UtcNow,
                        HandleBefore = entry.State == EntityState.Added ? null : JsonConvert.SerializeObject(CreateEntityFromPropertyValues(entry, entry.OriginalValues), settings),
                        HandleAfter = entry.State == EntityState.Deleted ? null : JsonConvert.SerializeObject(CreateEntityFromPropertyValues(entry, entry.CurrentValues), settings),
                        HandleUser = _userContext.UserId ?? 0
                    };
                    changeLogs.Add(changeLog);
                }
            }

            if (changeLogs.Any())
            {
                DataChangeLogs.AddRange(changeLogs);
            }
        }

        private void RecordChangesCreate(IEnumerable<EntityEntry> createEntityEntries)
        {
            var changeLogs = new List<DataChangeLog>();
            foreach (var entry in createEntityEntries)
            {
                if (entry.Entity is not Entity entity)
                {
                    continue;
                }



                var tableName = entry.Metadata.GetTableName() ?? throw new Exception("資料表定義讀取失敗，可能是沒有設定主鍵");

                var primaryKey = entry.Metadata.FindPrimaryKey() ?? throw new Exception("資料表定義讀取失敗，可能是沒有設定主鍵");

                var keyName = primaryKey.Properties.Select(p => p.Name).Single();
                var keyValue = entry.CurrentValues[keyName]?.ToString();
                int? key = null;
                if (keyValue != null && entry.State != EntityState.Added)
                {
                    key = int.Parse(keyValue);
                }


                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var changeLog = new DataChangeLog
                {
                    TableName = tableName,
                    TablePKValue = key,
                    HandleType = DataChangeLog.HandleTypes.Create,
                    HandleDateTime = DateTime.UtcNow,
                    HandleBefore = null,
                    HandleAfter = JsonConvert.SerializeObject(CreateEntityFromPropertyValues(entry, entry.CurrentValues), settings),
                    HandleUser = _userContext.UserId ?? 0
                };
                changeLogs.Add(changeLog);
            }

            if (changeLogs.Any())
            {
                DataChangeLogs.AddRange(changeLogs);
            }
        }


        private object CreateEntityFromPropertyValues(EntityEntry entry, PropertyValues values)
        {
            var entityType = entry.Metadata.ClrType;

            var entity =
                Activator.CreateInstance(entityType);

            foreach (IProperty prop in values.Properties)
            {
                var propInfo =
                    entityType.GetProperty(prop.Name);

                if (propInfo != null &&
                   propInfo.CanWrite)
                {
                    propInfo.SetValue(
                        entity,
                        values[prop]);
                }
            }

            return entity;
        }
    }
}
