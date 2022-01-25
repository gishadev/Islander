using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Tree : MonoBehaviour, IMineable
    {
        private int _health = 5;
        
        public void Mine()
        {
            _health--;
            if (_health <= 0)
                Destroy(gameObject);
        }
    }
}