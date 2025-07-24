using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    public class Eresponse
    {
        private string status;

        public Eresponse(string status)
        {
            this.Status = status;
        }

        public string Status { get => status; set => status = value; }
    }
}
