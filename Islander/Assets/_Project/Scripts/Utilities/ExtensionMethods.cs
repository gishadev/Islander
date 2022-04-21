using UnityEngine;

namespace Gisha.Islander.Utilities
{
    public static class ExtensionMethods
    {
        public static bool TryGetComponentInChildren<T>(this Component self, out T component) where T : Component
        {
            component = self.GetComponentInChildren<T>();
            return component != null;
        }

        public static bool TryGetComponentInChildren<T>(this Component self, out T component, bool includeInactive)
            where T : Component
        {
            component = self.GetComponentInChildren<T>(includeInactive);
            return component != null;
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T t))
                return t;

            return gameObject.AddComponent<T>();
        }
    }
}