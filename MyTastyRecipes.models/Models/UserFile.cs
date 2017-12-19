using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.models.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public byte[] ByteArray { get; set; }
        public string UserFileName { get; set; }
        public string Extension { get; set; }
        public string SaveLocation { get; set; }
    }
}
