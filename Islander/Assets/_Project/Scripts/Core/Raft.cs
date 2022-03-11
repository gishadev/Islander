using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Islander.Core
{
    public class Raft : Floater
    {
        [SerializeField] private int level;

        public int Level => level;

        private List<Transform> _playersOnRaft = new List<Transform>();

        private void OnDisable()
        {
            foreach (var playerTrans in _playersOnRaft)
            {
                if (playerTrans != null)
                    playerTrans.SetParent(null);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playersOnRaft.Add(other.transform);
                other.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playersOnRaft.Remove(other.transform);
                other.transform.SetParent(null);
                other.transform.rotation = Quaternion.Euler(0, other.transform.rotation.eulerAngles.y, 0);
            }
        }
    }
}