using System;
using _main.Scripts.Extensions;
using UnityEngine;

namespace _main.Scripts.PhysicsEngine
{
    public class PhysicsBody : MonoBehaviour
    {
        [SerializeField] private bool isEnable = true;
        [SerializeField] private TypeCollider typeCollider;
        [SerializeField] private Vector2 sizeCollider;
        [SerializeField] private float radius;
        [SerializeField] private float mass;
        [SerializeField] private float inertia;
        [SerializeField] private bool isStatic;
        [SerializeField] private bool isKinematic;
        [SerializeField] private bool isTrigger;
        [SerializeField] private bool isGravity;

#if UNITY_EDITOR
        [Header("Only Editor")] 
        [SerializeField] private Vector2 startVelocity;
        [SerializeField] private float startVelocityAng;
        private void FixedUpdate()
        {
            AddForce(startVelocity);
            AddTorque(startVelocityAng);
        }
#endif

        public event Action<GameObject> OnCollisionEvent;
        public event Action<GameObject> OnTriggerEvent;

        private void Awake()
        {
            PhysicsEngine.AddPhysicsBody(this);
        }

        private void OnDestroy()
        {
            PhysicsEngine.RemovePhysicsBody(this);
        }
        
        public void AddForce(Vector2 p_force)
        {
            Acceleration += p_force / mass;
        }

        public void AddImpulse(Vector2 p_force)
        {
            Velocity += p_force / mass;
        }
        public void AddForceOnPoint(Vector2 p_force, Vector2 p_point)
        {
            AddForce(p_force);

            var l_diff = (Vector2)transform.position - p_point;
            var l_torqueX = p_force.x * l_diff.y;
            var l_torqueY = -p_force.y * l_diff.x;

            AddTorque(l_torqueX + l_torqueY);
        }
 
        public void AddTorque(float p_torque)
        {
            AccelerationAng += p_torque / (mass * inertia);
        }

        public Vector2 Velocity { get; set; } = Vector2.zero;
        public Vector2 Acceleration { get; set; }
        public float VelocityAng { get; set; }
        public float AccelerationAng { get; set; }

        public void SetPosition(Vector2 p_newValue) => transform.position = p_newValue;
        public void AddPosition(Vector2 p_value) => transform.position += new Vector3(p_value.x, p_value.y);
        public void SetAngle(float p_newValue)
        {
            var l_transform = transform;
            var l_eulerAngles = l_transform.eulerAngles;
            l_transform.rotation = Quaternion.Euler(new Vector3(l_eulerAngles.x, l_eulerAngles.y, p_newValue));
        }
        
        public void AddAngle(float p_newValue)
        {
            var l_eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(l_eulerAngles.x, l_eulerAngles.y, l_eulerAngles.z + p_newValue));
        }

        public void SetEnable(bool p_newValue) => isEnable = p_newValue;
        public void SetTrigger(bool p_newValue) => isTrigger = p_newValue;
        public void SetStatic(bool p_newValue) => isStatic = p_newValue;
        public void SetKinematic(bool p_newValue) => isKinematic = p_newValue;
        public void SetGravity(bool p_newValue) => isGravity = p_newValue;
        
        public bool GetEnable() => isEnable;
        public bool GetTrigger() => isTrigger;
        public bool GetStatic() => isStatic;
        public bool GetKinematic() => isKinematic;
        public bool GetGravity() => isGravity;
        
        public Vector2 GetSizeCollider() => sizeCollider;
        public Vector2 GetPosition() => transform.position;

        public float GetRadius() => radius;
        public float GetMass() => mass;
        
        public TypeCollider GetTypeCollider() => typeCollider;

        public GameObject GetGameObject() => gameObject;

        public void OnTriggerEventHandler(GameObject p_gameObject)
        {
            OnTriggerEvent?.Invoke(p_gameObject);
        }

        public void OnColliderEventHandler(GameObject p_gameObject)
        {
            OnCollisionEvent?.Invoke(p_gameObject);
        }

#if UNITY_EDITOR

        [SerializeField] private PhysicsBody boxBody;
        
        private void OnDrawGizmosSelected()
        {
            switch (typeCollider)
            {
                case TypeCollider.Circle:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case TypeCollider.Box:
                    Gizmos.DrawWireCube(transform.position, sizeCollider);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
#endif
    }
}