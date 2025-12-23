using server_side.Entities;

namespace server_side.Models
{
    public class Shape
    {

        public int Id { get; set; }
        public string Code { get; set; }   
        public ICollection<Challenge> Challenges { get; set; }
    }
}
