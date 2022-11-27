namespace Skull.Api
{
    internal class TableFactory : ITableFactory
    {
        private readonly string[] _adjectives;
        private readonly string[] _occupations;

        public TableFactory(IEnumerable<string> adjectives, IEnumerable<string> occupations)
        {
            _adjectives = adjectives.ToArray();
            _occupations = occupations.ToArray();
        }

        public Table Create()
        {
            var adjective = _adjectives[Random.Shared.Next(0, _adjectives.Length)];
            var occupation = _occupations[Random.Shared.Next(0, _occupations.Length)];
            return new Table($"{adjective} {occupation}");
        }
    }
}
