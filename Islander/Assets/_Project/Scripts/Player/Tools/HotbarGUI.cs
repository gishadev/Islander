using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.Player.Tools
{
    public class HotbarGUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemGUIPrefab;

        private Color _defSlotColor = new Color(0.8f, 0.8f, 0.8f);
        private Transform[] _slots;

        public void ResetGUI()
        {
            _slots = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                _slots[i] = transform.GetChild(i);

                if (_slots[i].childCount > 0)
                    Destroy(_slots[i].GetChild(0).gameObject);
            }
        }


        public void AddToolGUI(GameObject toolPrefab, int index)
        {
            var guiElement = Instantiate(itemGUIPrefab, _slots[index]);
            guiElement.GetComponent<TMP_Text>().text = toolPrefab.name;
        }

        public void ToolEquipGUI(int index)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                var img = _slots[i].GetComponent<Image>();

                if (i == index)
                {
                    img.color = Color.yellow;
                    continue;
                }

                img.color = _defSlotColor;
            }
        }
    }
}