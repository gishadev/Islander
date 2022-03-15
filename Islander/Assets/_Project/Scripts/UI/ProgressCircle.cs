using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.UI
{
    public class ProgressCircle : MonoBehaviour
    {
        public static ProgressCircle Instance { get; private set; }

        private Image _circle;

        private void Awake()
        {
            Instance = this;

            _circle = GetComponent<Image>();
        }

        public void SetProgress(float value)
        {
            _circle.fillAmount = value;
        }
    }
}