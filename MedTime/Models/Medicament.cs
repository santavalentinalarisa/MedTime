using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MedTime.Models
{
   public class Medicament
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nume { get; set; }
       
        public string Doza { get; set; }
        public TimeSpan Ora { get; set; }
        
        public DateTime DataStart { get; set; }
        public DateTime DataFinal { get; set; }

       //public DateTime MinimumDate { get; set; }
    }
}
