using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.models.ViewModel
{
    public class RecipeImageBase
    {
        public string Name { get; set; }
        public int FileId { get; set; }
        public string SystemFileName { get; set; }
    }
}
