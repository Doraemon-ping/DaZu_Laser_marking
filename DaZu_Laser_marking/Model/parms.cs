using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    public class parms
    {
        private string key;
        private string value;

        public string Key { get => key; set => key = value; }
        public string Value { get => value; set => this.value = value; }

        public parms(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
