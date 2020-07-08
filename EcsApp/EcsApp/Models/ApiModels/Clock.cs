using System;

namespace EcsApp.Models.ApiModels
{
    public class Clock
    {
        public DateTime In { get; set; }
        public bool InWhileOnPremises { get; set; }
        public DateTime Out { get; set; }
        public bool OutWhileOnPremises { get; set; }
        public bool IsActive { get; set; }
    }
}