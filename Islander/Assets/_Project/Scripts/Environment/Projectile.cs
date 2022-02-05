using System;
using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Projectile : MonoBehaviour
    {
        public Transform Owner { get; set; }

        private PhotonView _pv;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
        }

        private void Start()
        {
            Invoke("Destroy", 60f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player") && !Owner.IsChildOf(other.transform))
                other.collider.GetComponent<PlayerController>().GetDamage(30);

            Destroy();
        }

        private void Destroy()
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}