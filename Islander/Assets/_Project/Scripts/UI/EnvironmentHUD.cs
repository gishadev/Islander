using System;
using Gisha.Islander.Environment;
using Gisha.Islander.Player;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class EnvironmentHUD : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TMP_Text objectNameText;
        [SerializeField] private Transform healthBar;

        private GameObject _lastRaycastTarget;
        private MineableResource _lastMineable;
        
        private void OnEnable()
        {
            GUIRaycaster.HUDShowed += Show;
        }

        private void OnDisable()
        {
            GUIRaycaster.HUDShowed -= Show;
        }

        private void Show(bool status, RaycastHit hitInfo, Vector3 direction)
        {
            infoPanel.SetActive(status);
            if (!status)
                return;
            
            Vector3 hudPosition = new Vector3(hitInfo.transform.position.x, hitInfo.point.y,
                hitInfo.transform.position.z);
            
            transform.position = hudPosition;
            transform.rotation = Quaternion.LookRotation(direction);
            
            if (_lastRaycastTarget == null || _lastRaycastTarget != hitInfo.collider.gameObject)
            {
                _lastRaycastTarget = hitInfo.collider.gameObject;
                _lastMineable = hitInfo.collider.GetComponentInParent<MineableResource>();
            }

            if (_lastMineable != null)
            {
                UpdateHUD(_lastMineable.ResourceType.ToString(),
                    _lastMineable.HealthPercentage);
            }
        }

        private void UpdateHUD(string objName, float healthPercentage)
        {
            objectNameText.text = objName;
            healthBar.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
    }
}