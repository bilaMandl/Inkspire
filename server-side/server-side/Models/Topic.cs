using server_side.Entities;

namespace server_side.Models
{
    public class Topic
    {        
            public int Id { get; set; }
            public string Name { get; set; }   
            public ICollection<Challenge> Challenges { get; set; }

    }
}
