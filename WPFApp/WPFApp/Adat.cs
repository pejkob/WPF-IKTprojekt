using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp
{
    public class Adat
    {
        string szemId;
        string aruAtvevo;

        public string SzemId { get { return szemId; } set { szemId = value; } }
        public string AruAtvevo { get {  return aruAtvevo; } set {  aruAtvevo = value; } }

        public Adat(string szemId, string aruAtvevo)
        {
            SzemId = szemId;
            AruAtvevo = aruAtvevo;
        }
    }
}
