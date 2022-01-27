using System;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private Transform[] spawnpoints;

        public Transform[] Spawnpoints => spawnpoints;

        private void Awake()
        {
            Instance = this;
        }
    }
}