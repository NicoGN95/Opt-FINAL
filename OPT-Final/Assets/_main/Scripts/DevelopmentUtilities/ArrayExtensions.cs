using UnityEngine;

namespace _main.Scripts.DevelopmentUtilities
{
    public static class ArrayExtensions
    {
        public static T GetRandomElement<T>(this T[] baseArray)
        {
            return baseArray[Random.Range(0, baseArray.Length)];
        }
    }
}