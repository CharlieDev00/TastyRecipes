using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.models.Models
{
    public class RecipeIngredients
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Measurements { get; set; }
        public string Ingredient { get; set; }
        public int RecipeId { get; set; }
    }
}
