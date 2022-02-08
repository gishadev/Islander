using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        public abstract void PrimaryUse(Vector3 origin, Vector3 direction);
    }
}