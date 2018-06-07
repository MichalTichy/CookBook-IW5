﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.DAL.Entities;

namespace CookBook.BL.Models
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [Range(typeof(TimeSpan), "00:01", "23:59")]
        public TimeSpan Duration { get; set; }
        public virtual ICollection<IngredientAmountEntity> Ingredients { get; set; }
    }
}
