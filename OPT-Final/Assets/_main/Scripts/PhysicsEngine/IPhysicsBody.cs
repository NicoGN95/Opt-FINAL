using System;
using UnityEngine;

namespace _main.Scripts.PhysicsEngine
{
    public interface IPhysicsBody
    {
        //Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        float VelocityAng { get; set; }
        //float Angle { get; set; }
        float AccelerationAng { get; set; }
        
        void AddPosition(Vector2 p_value);
        void AddAngle(float p_value);
        void SetPosition(Vector2 p_newValue);
        void SetAngle(float p_newValue);
        void SetEnable(bool p_newValue);
        void SetTrigger(bool p_newValue);
        void SetStatic(bool p_newValue);
        void SetKinematic(bool p_newValue);
        void SetGravity(bool p_newValue);
        bool GetEnable();
        bool GetTrigger();
        bool GetStatic();
        bool GetKinematic();
        bool GetGravity();
        Vector2 GetSizeCollider();
        Vector2 GetPosition();
        float GetRadius();
        TypeCollider GetTypeCollider();
        GameObject GetGameObject();
        float GetMass();
        
        public void AddForce(Vector2 p_force, ForceMode2D p_forceMode2D = ForceMode2D.Force);
        public void AddForceOnPoint(Vector2 p_force, Vector2 p_point);
        public void AddTorque(float p_torque);

        
        void OnTriggerEventHandler(GameObject p_gameObject);
        void OnColliderEventHandler(GameObject p_gameObject);
    }

    [Serializable]
    public enum TypeCollider
    {
        Circle,
        Box
    }
}