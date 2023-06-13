using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using WebShop.DTOs.UserDTOs;
using WebShop.Models.UserEntities;

namespace WebShop.Utilities {
    public class UserControllerHelper {

        public (string loginPwdHashString, string existEmailPwHashString) LoginByteToString(LoginDTO loginDTO, User existEmail) {
            var hmac = new HMACSHA512();
            hmac.Key = existEmail.PasswordSalt;
            byte[] loginPwHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            (string loginPwHashString, string existEmailPwHashString) = (Encoding.UTF8.GetString(loginPwHash), Encoding.UTF8.GetString(existEmail.PasswordHash));
        
            return (loginPwHashString, existEmailPwHashString);
        }

        public (byte[] PwSalt, byte[] PwHash) SaltHashCreator(string pwd) {
            var hmac = new HMACSHA512();
            (byte[] PwSalt, byte[] PwHash) = (hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd)));

            return (PwSalt, PwHash);
        }
    }
}
