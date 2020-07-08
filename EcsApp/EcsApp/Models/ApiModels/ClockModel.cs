using System;
using Xamarin.Essentials;

namespace EcsApp.Models
{
    public class ClockModel
    {
        public string Email { get; set; }
        public DateTime ClockTime { get; set; }
        public Location ClockLocation { get; set; }
    }
}
