using System;
using System.Collections;
using UnityEngine;

namespace _main.Scripts.Extensions
{
    public static class MonobehaviourExtensions
    {
        public static void ActionAfterFrame(this MonoBehaviour monoBehaviour, Action action)
        {
            monoBehaviour.StartCoroutine(IEActionAfterFrame(action));
        }

        private static IEnumerator IEActionAfterFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}