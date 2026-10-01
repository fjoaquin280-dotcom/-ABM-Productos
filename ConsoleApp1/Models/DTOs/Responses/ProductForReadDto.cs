using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models.DTOs.Responses
{
    public class ProductForReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
