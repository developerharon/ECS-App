using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsApp
{

    public class ClockPageMasterMenuItem
    {
        public ClockPageMasterMenuItem()
        {
            TargetType = typeof(ClockPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}