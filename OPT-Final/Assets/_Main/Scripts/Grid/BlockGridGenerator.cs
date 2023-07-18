using System;
using System.Collections.Generic;
using _main.Scripts.Managers;
using UnityEngine;

namespace _main.Scripts.Grid
{
    public class BlockGridGenerator : MonoBehaviour
    {
        [SerializeField] private List<MyBlock> blocksPrefabs;
        [SerializeField] private Vector2 blockSize;
        [SerializeField] private Vector2 gridworldSize;

        private MyBlock[,] m_grid;
        private int m_gridSizeX;
        private int m_gridSizeY;


        private void Awake()
        {
            GameManager.Instance.SetGrid(this);
            Initialize();
        }

        [ContextMenu("GenerateGrid")]
        private void Initialize()
        {
            m_gridSizeX = Mathf.RoundToInt(gridworldSize.x/blockSize.x);
            m_gridSizeY = Mathf.RoundToInt(gridworldSize.y/blockSize.y);
            CreateGrid();
            
        }

        
        void CreateGrid()
        {
            var l_grid = new MyBlock[m_gridSizeX, m_gridSizeY];
            Vector3 l_worldBottomLeft = transform.position - Vector3.right * gridworldSize.x / 2 -
                                        Vector3.up * gridworldSize.y / 2;

            for (int l_x = 0; l_x < m_gridSizeX; l_x++)
            {
                for (int l_y = 0; l_y < m_gridSizeY; l_y++)
                {
                    
                    //Fijamos la verdadera posicion en el mundo del nodo
                    Vector3 l_worldPoint = l_worldBottomLeft + Vector3.right * (l_x * blockSize.x + blockSize.x /2 ) + Vector3.up * (l_y * blockSize.y + blockSize.y / 2);

                    var l_blockId = l_y % blocksPrefabs.Count;
                    
                    var l_block = Instantiate(blocksPrefabs[l_blockId]);
                    //------------
                    l_block.Initialize(l_worldPoint, new Vector2(l_x, l_y), l_x == 0);
                    
                    l_grid[l_x, l_y] = l_block;
                }
            }

            m_grid = l_grid;
        }
        
        
        public IEnumerable<MyBlock> GetNeighbours(MyBlock p_node)
        {
            var l_neighbours = new List<MyBlock>();

            if (p_node.xId-1 >-1)
                l_neighbours.Add(m_grid[(p_node.xId - 1), p_node.yId]);

            if (p_node.xId + 1 <= m_gridSizeX-1)
                l_neighbours.Add(m_grid[(p_node.xId + 1), p_node.yId]);
            
            if (p_node.yId - 1 > -1)
                l_neighbours.Add(m_grid[p_node.xId, (p_node.yId-1)]);

            if (p_node.yId + 1 <= m_gridSizeY-1)
                l_neighbours.Add(m_grid[p_node.xId, (p_node.yId + 1)]);

            return l_neighbours;
        }
        
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(gridworldSize.x, gridworldSize.y));
        }
#endif
    }
}