namespace server_side.Models.DTOs
{
    public class DrawingCreateDto
    {
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string DrawingName { get; set; }
        public string ImageDataUrl { get; set; }
    }
}
