namespace EncounterGraphix
{
    public interface ICombatFrame : IFrame
    {
        public CreatureInfoBox Defender { get; }
        public CreatureInfoBox Attacker { get; }
    }
}