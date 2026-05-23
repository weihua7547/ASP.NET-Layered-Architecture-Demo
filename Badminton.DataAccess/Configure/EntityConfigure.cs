using Badminton.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Configure
{
    public abstract class EntityConfigure<T>: IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");
            builder.HasQueryFilter(x => x.DeleteKey == new Guid()); ;

            builder.Property(x => x.Id)
                .HasComment("序號")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatorId)
                .HasComment("建立人員")
                .IsRequired()
                .HasDefaultValue(0)
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Property(x => x.CreatedDateTime)
                .HasComment("創建時間")
                .IsRequired()
                .HasPrecision(3)
                .HasDefaultValueSql("GETUTCDATE()")
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Property(x => x.UpdaterId)
                .HasComment("修改人員")
                .IsRequired(false)
                .HasDefaultValue(0)
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);

            builder.Property(x => x.UpdatedDateTime)
                .HasComment("修改時間")
                .IsRequired(false)
                .HasPrecision(3)
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);

            builder.Property(x => x.DeleteKey)
                .HasComment("刪除鍵值")
                .ValueGeneratedNever()
                .HasDefaultValue(new Guid());

            ConfigureHelper(builder);
        }

        protected abstract void ConfigureHelper(EntityTypeBuilder<T> builder);
    }
}
