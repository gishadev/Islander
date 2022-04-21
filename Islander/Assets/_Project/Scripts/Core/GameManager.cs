using Gisha.Islander.Environment;
using UnityEngine;

namespace Gisha.Islander.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private Totem[] totems;

        public Totem[] Totems => totems;

        private void Awake()
        {
            Instance = this;
        }

        [ContextMenu("Find All Totems")]
        public void FindAllTotems()
        {
            totems = FindObjectsOfType<Totem>();
        }
    }
}