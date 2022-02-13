using UnityEngine;

namespace Gisha.Islander.Core
{
    public class Raft : Floater
    {
        [SerializeField] private int level;

        public int Level => level;
    }
}