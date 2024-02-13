using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiseCompanion.Models
{
    public class Medication
    {
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Time { get; set; }

        public string DosageAndTime => $"{Dosage}, Time: {Time}";
    }
}
