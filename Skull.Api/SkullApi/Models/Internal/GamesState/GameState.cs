namespace SkullApi.Models.Internal.GamesState
{
    public sealed class GameState : IGameState
    {
        public int PlayerWithOnus { get; private set; }

        public string Name { get; private init; }

        public IList<IPlayer> Players { get; private init; }

        public Phase Phase { get; private set; }

        public Stack<IBid> Bids { get; private init; }

        public GameState(string name, IList<IPlayer> players)
        {
            Name = name;
            Players = players;
            Bids = new Stack<IBid>();
            Phase = Phase.Placement;
            PlayerWithOnus = Random.Shared.Next(players.Count - 1);
        }

        public int NextPlayer()
        {
            PlayerWithOnus++;
            if (PlayerWithOnus == Players.Count) PlayerWithOnus = 0;
            return PlayerWithOnus;
        }

        public Phase NextPhase()
        {
            if (Phase == Phase.Reveal) Phase = Phase.Complete;
            if (Phase == Phase.Challenge) Phase = Phase.Reveal;
            if (Phase == Phase.Placement) Phase = Phase.Challenge;
            return Phase;
        }
    }
}
