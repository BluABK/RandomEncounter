namespace RandomEncounter
{
    public interface IMoveOutcome
    {
        public Creature Attacker { get; }
        public Creature Defender { get; }
        public Move MoveUsed { get; }
        public bool Hit { get; }
        public bool Crit { get; }
        public int Damage { get; }
        public bool TargetsDefender { get; }
    }
}