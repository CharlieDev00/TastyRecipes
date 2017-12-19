using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.models.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ImageUrl { get; set; }
        public int Number { get; set; }
        public string Time { get; set; }
        public int Yields { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
