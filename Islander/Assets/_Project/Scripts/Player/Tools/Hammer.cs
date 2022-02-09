using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Hammer : Tool
    {
        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner)
        {
            Debug.Log("Build some stuff!");
        }
    }
}