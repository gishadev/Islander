using Gisha.Islander.Player;

namespace Gisha.Islander.Environment
{
    public interface IMineable
    {
        public void Mine(PlayerController owner, float damage, float pickaxeEfficiency, float axeEfficiency);
    }
}