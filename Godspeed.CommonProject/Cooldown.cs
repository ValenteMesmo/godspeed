namespace Godspeed
{
    public class Cooldown
    {
        private readonly int cooldownDurationInUpdatesCount;
        private int cooldown;

        public Cooldown(int cooldownDurationInUpdatesCount)
            => this.cooldownDurationInUpdatesCount = cooldownDurationInUpdatesCount;

        public void Update() { if (cooldown > 0) cooldown--; }

        public bool IsOnCooldown() => cooldown > 0;
        public bool NotOnCooldown() => cooldown == 0;
        public void SetOnCooldown() => cooldown = cooldownDurationInUpdatesCount;
    }
}
