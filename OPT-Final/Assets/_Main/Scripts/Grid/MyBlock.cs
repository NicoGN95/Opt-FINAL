using System;
using _main.Scripts.Managers;
using _main.Scripts.PhysicsEngine;
using UnityEngine;

namespace _main.Scripts.Grid
{
    public class MyBlock : MonoBehaviour
    {
        [SerializeField] private BlockData data;
        [HideInInspector] public int xId;
        [HideInInspector]public int yId;

        private PhysicsBody m_myBody;
        private void Awake()
        {
            m_myBody = GetComponent<PhysicsBody>();
            m_myBody.OnCollisionEvent += MyOnCollisionEvent;
        }

        private void MyOnCollisionEvent(GameObject p_obj)
        {
            if (p_obj.CompareTag("Ball"))
            {
                var l_neighs = GameManager.Instance.blockGrid.GetNeighbours(this);

                foreach (var l_neigh in l_neighs)
                {
                    l_neigh.SetCollisions(true);
                }
                Destroy(gameObject);
            }
        }

        public void Initialize(Vector3 p_spawnPos, Vector2 p_gridId, bool p_isActive)
        {
            transform.position = p_spawnPos;

            xId = (int)p_gridId.x;
            yId = (int)p_gridId.y;
            SetCollisions(p_isActive);
        }

        public void SetCollisions(bool p_b)
        {
            m_myBody.SetEnable(p_b);
        }
    }
}