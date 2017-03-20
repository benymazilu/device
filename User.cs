using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeviceManagement.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Location { get; set; }

       // public List<Device> Devices { get; set; }
    }
}