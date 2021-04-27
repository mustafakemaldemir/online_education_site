using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace online_education_site.Helpers
{
    public static class ClaimHelper
    {
        public static User GetUser(System.Security.Claims.Claim claim)
        {
            if (claim == null) 
            {
                throw new Exception("Claim bulunamadı!");
            }
            
            string userName = "";
            var claimModel = JsonSerializer.Deserialize<ClaimModel>(claim.Value);
            
            userName = claimModel.UserName;

            var _veritabani = new online_educationContext();
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == userName);

            if (user == null) 
            {
                throw new Exception("Kullanıcı bulunamadı!");
            }
            return user;
        }        
    }
}
