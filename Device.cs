using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeviceManagement.Models
{
    public class Device
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Type { get; set; }

        public string OperatingSystem { get; set; }

        public decimal OsVersion { get; set; }

        public string Processor { get; set; }

        public int Ram { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

    }
}