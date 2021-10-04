namespace EncounterGraphix
{
    public interface ICombatFrame : IFrame
    {
        public CreatureInfoElement DefenderInfo { get; }
        public CreatureInfoElement AttackerInfo { get; }
    }
}