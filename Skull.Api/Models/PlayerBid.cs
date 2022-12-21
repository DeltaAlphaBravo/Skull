namespace Skull.Api.Models
{
    public class PlayerBid : IPlayerBid
    {
        public int PlayerId { get; set; }
        public int? Bid { get; set; }
    }
}
