using UnityEngine;

namespace Gisha.Islander.Utilities
{
    public static class ExtensionMethods
    {
        public static bool TryGetComponentInChildren<T>(this Component c, out T retVal) where T : Component {
            foreach(Transform t in c.transform) {
                if(t.TryGetComponent<T>(out retVal)) return true;
            }
            retVal = null;
            return false;
        }
    }
}