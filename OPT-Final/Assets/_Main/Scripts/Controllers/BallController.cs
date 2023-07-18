using _main.Scripts.Managers;
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
            m_myBody.OnTriggerEvent += MyOnTriggerEvent;
        }

        

        public void Initialize(Vector3 p_initPos,Vector3 p_initDirVec)
        {
            transform.position = p_initPos;
            m_myBody.Velocity = p_initDirVec;
        }


        public Vector3 GetCurrDirVec() => m_myBody.Velocity;
        private void MyOnTriggerEvent(GameObject p_obj)
        {
            GameManager.Instance.LostBall(this);
        }
    }
}