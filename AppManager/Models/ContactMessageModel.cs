using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace AppManager.Models
{
    [Area("Admin")]
    [Authorize(Roles = "admin, staff")]
    public class ContactMessageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
