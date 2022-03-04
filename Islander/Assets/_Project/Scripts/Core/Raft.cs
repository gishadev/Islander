using System;
using UnityEngine;

namespace Gisha.Islander.Core
{
    public class Raft : Floater
    {
        [SerializeField] private int level;

        public int Level => level;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                other.transform.SetParent(transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                other.transform.SetParent(null);
        }
    }
}