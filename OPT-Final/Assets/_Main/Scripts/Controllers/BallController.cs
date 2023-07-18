using _main.Scripts.PhysicsEngine;
using UnityEngine;

namespace _main.Scripts.Controllers
{
    public class BallController : MonoBehaviour
    {

        private PhysicsBody m_myBody;
        private void Awake()
        {
            m_myBody = GetComponent<PhysicsBody>();
        }

        public void Initialize(Vector3 p_initPos,Vector3 p_initDirVec)
        {
            
        }


        public Vector3 GetCurrDir() => m_myBody.Velocity;
    }
}