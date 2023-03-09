using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Entites
{
    public class EPCReadTemp 
    {
        //public byte PacketParam { get; set; }
        public int Id { get; set; }
        public int Sort { get; set; }
        public string Epc { get; set; }
        //public int phase_begin { get; set; }
        //public int phase_end { get; set; }
        public int Rssi { get; set; }
        public byte Ant { get; set; }
        public DateTime EpcDate { get; set; }

    }
    
}
