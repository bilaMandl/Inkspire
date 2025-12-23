using System.ComponentModel.DataAnnotations;
using server_side.Models;

namespace server_side.Entities
{
    public class Challenge
    {
        public int Id { get; set; }

        public int ShapeId { get; set; }
        public Shape Shape { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public int ComplexityLevel { get; set; }
        public User Creator { get; set; }
    }

}
