﻿using UnityEngine;

namespace _Main.Scripts.Blocks
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "main/Datas/BlockData", order = 0)]
    public class BlockData : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public float PowerUpChances { get; private set; }
    }
}