using UnityEngine;

namespace _Main.Scripts.Blocks
{
    public class MyBlock : MonoBehaviour
    {
        [SerializeField] private BlockData data;
        
        [HideInInspector] public int xId;
        [HideInInspector]public int yId;

        public void Initialize(Vector3 p_spawnPos, Vector2 p_gridId)
        {
            transform.position = p_spawnPos;

            xId = (int)p_gridId.x;
            yId = (int)p_gridId.y;
        }
    }
}