using System.Collections.Generic;
using UnityEngine;

namespace _main.Scripts.DevelopmentUtilities
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> baseList)
        {
            return baseList[Random.Range(0, baseList.Count)];
        }
    }
}