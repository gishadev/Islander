using System;
using UnityEngine;

namespace Gisha.Islander
{
    public class FPSLimiter : MonoBehaviour
    {
        [SerializeField] private int maxFps = 60;

        private void Start()
        {
            Application.targetFrameRate = maxFps;
        }
    }
}