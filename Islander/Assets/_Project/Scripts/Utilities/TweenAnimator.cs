using System.Collections;
using UnityEngine;

namespace Gisha.Islander.Utilities
{
    public class TweenAnimator : MonoBehaviour
    {
        private static TweenAnimator Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public static void Scale(Transform target, float to, float time, bool isCyclic = false) =>
            Instance.StartCoroutine(Instance.ScaleCoroutine(target, to, time, isCyclic));

        private IEnumerator ScaleCoroutine(Transform target, float to, float time, bool isCyclic)
        {
            float startScale = target.transform.localScale.x;
            float cyclicMultiplier = isCyclic ? 2f : 1f;
            bool upScale = to > startScale;

            target.localScale = Vector3.one * startScale;
            while (upScale && target != null)
            {
                target.transform.localScale += Vector3.one * (to / time * cyclicMultiplier * Time.deltaTime);

                if (target.transform.localScale.x > to)
                {
                    target.localScale = Vector3.one * to;

                    if (isCyclic)
                        StartCoroutine(ScaleCoroutine(target, startScale, time, false));
                    yield break;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }

            while (!upScale && target != null)
            {
                target.transform.localScale -= Vector3.one * (to / time * cyclicMultiplier * Time.deltaTime);

                if (target.transform.localScale.x < to)
                {
                    target.localScale = Vector3.one * to;

                    if (isCyclic)
                        StartCoroutine(ScaleCoroutine(target, startScale, time, false));
                    yield break;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}