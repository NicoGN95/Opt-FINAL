using _main.Scripts.PhysicsEngine;
using _main.Scripts.Services;
using _main.Scripts.StaticClass;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _main.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput inputManager;
        [SerializeField] private float speed;
        [SerializeField] private float leftScreenBorder;
        [SerializeField] private float rightScreenBorder;

        private static IEventService EventService => ServiceLocator.Get<IEventService>();
        private float m_MovementInput;
        private PhysicsBody m_myBody;
    
        private void Start()
        {
            m_myBody = GetComponent<PhysicsBody>();
            inputManager.actions["Movement"].performed += OnMovementPerformed;
            inputManager.actions["Movement"].canceled += OnMovementCanceled;
            EventService.AddListener(EventsDefinitions.FIXED_UPDATE_OBJECT_EVENT_ID, MyFixedUpdate);
        }
        private void OnMovementPerformed(InputAction.CallbackContext p_obj)
        {
            m_MovementInput = p_obj.ReadValue<float>();
        }
        private void OnMovementCanceled(InputAction.CallbackContext p_obj)
        {
            m_MovementInput = 0f;
        }
        private void MyFixedUpdate()
        {
            if ((transform.position.x < leftScreenBorder))
            {
                m_myBody.Velocity = Vector2.zero;
                transform.position =new Vector3(leftScreenBorder, transform.position.y);
                return;
            }

            if (transform.position.x > rightScreenBorder)
            {
                m_myBody.Velocity = Vector2.zero;
                transform.position =new Vector3(rightScreenBorder, transform.position.y);
                return;
            }
            m_myBody.Velocity = Vector2.right * m_MovementInput * speed;
        }

        
        
    }
}
