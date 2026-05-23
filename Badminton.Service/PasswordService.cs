using Badminton.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model
{
    public class PasswordService : IPasswordService
    {
        private readonly ICryptographyService _cryptographyService;
        public PasswordService(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        public string EncryptionPassword(string password, string? salt)
        {
            if (!string.IsNullOrEmpty(salt))
            {
                password += salt;
            }
            return _cryptographyService.ComputeSha256Hash(password);
        }
    }
}
