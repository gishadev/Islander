using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.Player.Tools
{
    public class HotbarGUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemGUIPrefab;

        private Color _defSlotColor;
        private Transform[] _slots;

        private void Start()
        {
            _slots = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
                _slots[i] = transform.GetChild(i);

            _defSlotColor = _slots[0].GetComponent<Image>().color;
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
                if (i == index)
                {
                    _slots[index].GetComponent<Image>().color = Color.yellow;
                    continue;
                }

                _slots[index].GetComponent<Image>().color = _defSlotColor;
            }
        }
    }
}