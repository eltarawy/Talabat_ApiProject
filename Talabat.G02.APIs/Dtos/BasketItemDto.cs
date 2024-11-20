﻿using System.ComponentModel.DataAnnotations;

namespace TalabatG02.APIs.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Quantity Must Be One Item AtLeast ")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
