using Gisha.Islander.Character;
using Gisha.Islander.Utilities;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Tree : MonoBehaviour, IMineable
    {
        private int _health = 5;

        public void Mine()
        {
            float part = transform.localScale.x / 10f;
            TweenAnimator.Scale(transform, transform.localScale.x - part, 0.5f, true);
            _health--;
            if (_health <= 0)
                Gather();
        }

        private void Gather()
        {
            InventoryManager.Instance.ChangeWoodCount(5);
            Destroy(gameObject);
        }
    }
}