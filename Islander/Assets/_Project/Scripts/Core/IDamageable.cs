using Gisha.Islander.Player;

namespace Gisha.Islander.Core
{
    public interface IDamageable
    {
        public void GetDamage(IDamager damager, PlayerController owner);
    }

    public interface IDamager
    {
        float Damage { get; }
    }
}