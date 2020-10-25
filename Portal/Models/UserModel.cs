using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Portal.Models
{
    public class User
    {
        [Key]
        public Int64 Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
