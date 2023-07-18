using _main.Scripts.Managers;
using _main.Scripts.PhysicsEngine;
using _main.Scripts.Services;
using _main.Scripts.StaticClass;
using UnityEngine;

namespace _main.Scripts.Controllers
{
    public class BallController : MonoBehaviour
    {
        private PhysicsBody m_myBody;
        private static IEventService EventService => ServiceLocator.Get<IEventService>();
        private void Start()
        {
            m_myBody = GetComponent<PhysicsBody>();
            EventService.AddListener(EventsDefinitions.FIXED_UPDATE_OBJECT_EVENT_ID, MyFixedUpdate);
            m_myBody.OnTriggerEvent += MyOnTriggerEvent;
            m_myBody.OnCollisionEvent += MyOnCollisionEvent;
            Initialize(transform.position, Vector3.down*2);
        }

        private void MyOnCollisionEvent(GameObject p_obj)
        {
            if (p_obj.CompareTag("Player"))
            {
                var l_prevVec = m_myBody.Velocity.magnitude;
                m_myBody.Velocity = Vector2.zero;
                var l_dir = (p_obj.transform.position - transform.position).normalized;
                
                m_myBody.AddImpulse(-l_dir * l_prevVec * 1.05f);
            }
        }

        private void MyFixedUpdate()
        {
            
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