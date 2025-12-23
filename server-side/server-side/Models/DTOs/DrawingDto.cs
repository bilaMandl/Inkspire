using server_side.Entities;
using System.ComponentModel.DataAnnotations;

namespace server_side.Models.DTOs
{
    public class DrawingDto
    {
        public int Id { get; set; }

        public string DrawingName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }

        public int? Rating { get; set; }
    }
}
