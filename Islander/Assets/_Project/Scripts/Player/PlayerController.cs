using System;
using Gisha.Islander.Core;
using Gisha.Islander.Player.Tools;
using Gisha.Islander.UI;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class PlayerController : MonoBehaviourPun, IDamageable
    {
        [Header("Player Settings")] [SerializeField]
        private float maxHealth = 100;

        [Header("Other")] [SerializeField] private float swimmingDamagePerSecond = 10;

        public Action Destroyed;
        
        public FPSCamera FPSCamera => _fpsCamera;
        public InventoryManager InventoryManager => _inventoryManager;

        private float _health;

        private ToolController _toolController;
        private InventoryManager _inventoryManager;
        private FPSMover _fpsMover;
        private FPSCamera _fpsCamera;

        private void Awake()
        {
            _inventoryManager = GetComponent<InventoryManager>();
            _toolController = GetComponent<ToolController>();
            _fpsMover = GetComponent<FPSMover>();
            _fpsCamera = GetComponent<FPSCamera>();
        }

        private void Start()
        {
            _health = maxHealth;
            UIManager.Instance.UpdateHealthBar(_health, maxHealth);
            UIManager.Instance.UpdateResourcesCount();

            if (photonView.IsMine)
                AddTool("Rock");
        }

        private void Update()
        {
            if (!photonView.IsMine)
                return;

            if (_fpsMover.IsSwimming)
                GetDamage(swimmingDamagePerSecond * Time.deltaTime);
        }

        public void GetDamage(float dmg)
        {
            photonView.RPC("RPC_GetDamage", RpcTarget.All, dmg);
        }

        [PunRPC]
        private void RPC_GetDamage(float dmg)
        {
            _health -= dmg;

            if (_health < 0)
            {
                _health = 0;

                Destroyed?.Invoke();
            }

            if (photonView.IsMine)
                UIManager.Instance.UpdateHealthBar(_health, maxHealth);
        }

        public void AddTool(string toolName)
        {
            photonView.RPC("RPC_AddTool", RpcTarget.AllBuffered, toolName);
        }

        [PunRPC]
        private void RPC_AddTool(string toolName)
        {
            _toolController.AddTool(toolName);
        }

        public void SpawnRaft(GameObject prefab, Vector3 position)
        {
            if (photonView.IsMine)
                PhotonNetwork.Instantiate($"Rafts/{prefab.name}", position + Vector3.up, Quaternion.identity);
        }
    }
}