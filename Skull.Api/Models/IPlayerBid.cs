namespace Skull.Api.Models
{
    public interface IPlayerBid
    {
        int? Bid { get; set; }
        int PlayerId { get; set; }
    }
}