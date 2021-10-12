namespace EncounterGraphix
{
    public interface ICombatFrame : IFrame
    {
        public CreatureInfoBox OpponentInfoBox { get; }
        public CreatureInfoBox PlayerInfoBox { get; }
    }
}