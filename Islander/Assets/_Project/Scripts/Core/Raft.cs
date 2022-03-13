using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Core
{
    public class Raft : Floater, IDamageable
    {
        [SerializeField] private int level;
        [SerializeField] private float health = 100f;

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

        public void GetDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                for (var i = 0; i < _playersOnRaft.Count; i++)
                {
                    if (_playersOnRaft[i] != null)
                        UnRootPlayer(_playersOnRaft[i]);
                }

                PhotonNetwork.Destroy(gameObject);
            }
        }

        private void UnRootPlayer(Transform player)
        {
            _playersOnRaft.Remove(player);
            player.SetParent(null);
            player.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0);
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
                UnRootPlayer(other.transform);
            }
        }
    }
}