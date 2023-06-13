using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebShop.Data;
using WebShop.DTOs.ShopDTOs;
using WebShop.DTOs.UserDTOs;
using WebShop.Models.ShopEntities;
using WebShop.Models.UserEntities;
using WebShop.Utilities;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ShopDb _dbHandle;
        public UserController(ShopDb dbHandle) { _dbHandle = dbHandle; }



        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult RegisterUser(RegistrationDTO registrationDTO)
        {
            User user = new User();

            var existEmail = _dbHandle.Users.FirstOrDefault(x => x.Email == registrationDTO.Email);

            if (existEmail != null)
            {
                return Unauthorized("Email already registered!");
            }
            else if (registrationDTO.Password != registrationDTO.ConfirmPassword)
            {
                return Unauthorized("Passwords are not matching!");
            }
            else
            {
                user.Email = registrationDTO.Email;
                user.FirstName = registrationDTO.FirstName;
                user.LastName = registrationDTO.LastName;

                (user.PasswordSalt, user.PasswordHash) = new UserControllerHelper().SaltHashCreator(registrationDTO.Password);

                var existUserAddress = _dbHandle.Addresses.FirstOrDefault(x =>
                    x.Country == registrationDTO.Address.Country &&
                    x.Region == registrationDTO.Address.Region &&
                    x.City == registrationDTO.Address.City &&
                    x.StreetAddress == registrationDTO.Address.StreetAddress);

                if (existUserAddress != null)
                {
                    user.Address = existUserAddress;
                }
                else
                {
                    user.Address = registrationDTO.Address;
                }
                _dbHandle.Users.Add(user);
                _dbHandle.SaveChanges();

                return Ok("Registration Successfull!");
            }
        }




        [HttpPost]
        [Route("LoginUser")]
        public ActionResult LoginUser(LoginDTO loginDTO)
        {
            var existEmail = _dbHandle.Users.FirstOrDefault(x => x.Email == loginDTO.Email);

            if (existEmail == null)
            {
                return Unauthorized("Email or Passwords are not matching!");
            }
            else
            {
                (string loginPwHashString, string existEmailPwHashstring) = new UserControllerHelper().LoginByteToString(loginDTO, existEmail);

                if (existEmailPwHashstring != loginPwHashString)
                {
                    return Unauthorized("Email or Passwords are not matching!");
                }
                else
                {
                    return Ok("Login Successfull!");
                }
            }
        }





        [HttpPost]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(LoginDTO loginDTO)
        {

            var existEmail = _dbHandle.Users.FirstOrDefault(x => x.Email == loginDTO.Email);

            if (existEmail == null)
            {
                return Unauthorized("Email or Passwords are not matching!");
            }
            else
            {
                (string loginPwHashString, string existEmailPwHashstring) = new UserControllerHelper().LoginByteToString(loginDTO, existEmail);

                if (existEmailPwHashstring != loginPwHashString)
                {
                    return Unauthorized("Email or Passwords are not matching!");
                }
                else
                {
                    _dbHandle.Users.Remove(existEmail);
                    _dbHandle.SaveChanges();
                    return Ok("User deletion Successfull!");
                }
            }
        }




        [HttpPost]
        [Route("ChangePasswordUser")]
        public ActionResult ChangePasswordUser(LoginDTO loginDTO, string password)
        {

            var existEmail = _dbHandle.Users.FirstOrDefault(x => x.Email == loginDTO.Email);

            if (existEmail == null)
            {
                return Unauthorized("Email or Passwords are not matching!");
            }
            else
            {
                (string loginPwHashString, string existEmailPwHashstring) = new UserControllerHelper().LoginByteToString(loginDTO, existEmail);

                if (existEmailPwHashstring != loginPwHashString)
                {
                    return Unauthorized("Email or Passwords are not matching!");
                }
                else
                {
                    (existEmail.PasswordSalt, existEmail.PasswordHash) = new UserControllerHelper().SaltHashCreator(loginDTO.Password);
                    _dbHandle.SaveChanges();
                    return Ok("Password change Successfull!");
                }
            }
        }





        [HttpPost]
        [Route("SeedUsers")]
        public ActionResult SeedUsers()
        {
            var FakeAddress = new Faker<Models.UserEntities.Address>()
                .RuleFor(s => s.Country, f => f.Address.Country())
                .RuleFor(s => s.Region, f => f.Address.County())
                .RuleFor(s => s.City, f => f.Address.City());

            var adresses = FakeAddress.Generate(30);

            Random random = new Random();

            var newUser = new Faker<User>()
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
                .RuleFor(s => s.Address, f => adresses[random.Next(adresses.Count)]);

            var users = newUser.Generate(100);

            _dbHandle.AddRange(users);

            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Users);
        }





    }
}

