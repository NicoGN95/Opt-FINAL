using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float leftScreenBorder;
    [SerializeField] private float rightScreenBorder;


    private float m_movementInput;
    
    
    private void Start()
    {
        inputManager.actions["Movement"].performed += OnMovementPerformed;
        inputManager.actions["Movement"].canceled += OnMovementCanceled;
        
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext p_obj)
    {
        m_movementInput = p_obj.ReadValue<float>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext p_obj)
    {
        m_movementInput = 0f;
    }

    private void Update()
    {
        if (transform.position.x > leftScreenBorder && m_movementInput < 0)
        {
            transform.position += Vector3.right * (m_movementInput * (speed * Time.deltaTime));
        }
        if (transform.position.x < rightScreenBorder && m_movementInput > 0)
        {
            transform.position += Vector3.right * (m_movementInput * (speed * Time.deltaTime));
        }
    }
}
