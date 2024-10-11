using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ClientRegisteros.Models
{
    public class RegisterModel
    {
        
        public int ID_Register { get; set; }
        public string Nom_Company { get; set; }
        public string Nom_Contact { get; set; }
        public string Email { get; set; }
        public string Num_Tel { get; set; }
    }
}
