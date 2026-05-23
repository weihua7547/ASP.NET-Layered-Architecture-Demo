using Badminton.Model;
using Badminton.Model.Abstract;
using Badminton.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess
{
    public static class SeedExtension
    {
        public static void SetDefaultUserSeed(this ModelBuilder builder, IPasswordService passwordService, IConfiguration configuration)
        {
            string salt = configuration["Salt:Pwd"] ?? "";
            string pwd = passwordService.EncryptionPassword("admin", salt);
            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                Account = "admin",
                Password = pwd,
                Phone = "0987654321",
                Email = "11363122@gm.nfu.edu.tw",
                Name = "系統管理員",
            });

        }
        public static void SetDefaultSystemManagerSeed(this ModelBuilder builder)
        {
            builder.Entity<SystemManager>().HasData(new SystemManager
            {
                Id = 1,
                Code = "CSIE-11363122"

            });
        }
    }
}
