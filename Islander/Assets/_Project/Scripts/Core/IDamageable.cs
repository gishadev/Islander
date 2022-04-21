using Gisha.Islander.Player;

namespace Gisha.Islander.Core
{
    public interface IDamageable
    {
        public void GetDamage(PlayerController owner, float damage);
    }
}