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
        private Vector2 m_movementInput;
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
            m_movementInput = p_obj.ReadValue<Vector2>();
        }
        private void OnMovementCanceled(InputAction.CallbackContext p_obj)
        {
            m_movementInput = Vector2.zero;
        }
        private void MyFixedUpdate()
        {
            var l_moveDir = m_movementInput;
            
            if (transform.position.x > leftScreenBorder && m_movementInput.x < 0)
            {
                l_moveDir = Vector3.right * (m_movementInput * speed);
            }
            if (transform.position.x < rightScreenBorder && m_movementInput.x > 0)
            {
                l_moveDir= Vector3.right * (m_movementInput * (speed * Time.deltaTime));
            }
            
            m_myBody.AddForce(l_moveDir);
        }

        
        
    }
}
