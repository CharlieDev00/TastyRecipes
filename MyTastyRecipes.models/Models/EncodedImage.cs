﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.models.Models
{
    public class EncodedImage
    {
        public string EncodedImageFile { get; set; }
        public string FileExtension { get; set; }
        public int DeleteId { get; set; }
        public string DeleteImageFile { get; set; }
    }
}
