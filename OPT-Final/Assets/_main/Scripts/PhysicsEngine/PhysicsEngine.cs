using System;
using System.Collections.Generic;
using _main.Scripts.Extensions;
using _main.Scripts.Services;
using _main.Scripts.StaticClass;
using UnityEngine;

namespace _main.Scripts.PhysicsEngine
{
    public static class PhysicsEngine
    {
        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        private static List<PhysicsBody> m_bodies;
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            EventService.AddListener(EventsDefinitions.FIXED_UPDATE_OBJECT_EVENT_ID, FixedUpdateHandler);
        }

        private static void FixedUpdateHandler()
        {
            if (m_bodies == default)
                return;
            
            //Chequeo De collisiones
            if (m_bodies.Count > 1)
            {
                for (var l_i = 0; l_i < m_bodies.Count - 1; l_i++)
                {
                    var l_bodyA = m_bodies[l_i];
                    if (!l_bodyA.GetEnable())
                        continue;

                    for (var l_j = l_i + 1; l_j < m_bodies.Count; l_j++)
                    {
                        var l_bodyB = m_bodies[l_j];

                        if (!l_bodyB.GetEnable() || (l_bodyA.GetStatic() && l_bodyB.GetStatic()))
                            continue;

                        if (!CheckCollision(l_bodyA, l_bodyB)) 
                            continue;
                    
                        if (!l_bodyA.GetTrigger() && !l_bodyB.GetTrigger())
                        {
                            l_bodyA.OnColliderEventHandler(l_bodyB.GetGameObject());
                            l_bodyB.OnColliderEventHandler(l_bodyA.GetGameObject());
                            ResolveCollision(l_bodyA, l_bodyB);
                        }
                        else
                        {
                            l_bodyA.OnTriggerEventHandler(l_bodyB.GetGameObject());
                            l_bodyB.OnTriggerEventHandler(l_bodyA.GetGameObject());
                        }
                    }
                }
            }

            // Calcular las fisicas.
            for (var l_i = 0; l_i < m_bodies.Count; l_i++)
            {
                var l_body = m_bodies[l_i];
                
                if (l_body.GetStatic() || !l_body.GetEnable())
                    continue;

                var l_deltaTime = Time.deltaTime;
                
                if (l_body.GetGravity() && !l_body.GetKinematic())
                    l_body.AddForce(Physics2D.gravity * l_deltaTime);
                
                l_body.Velocity += l_body.Acceleration * l_deltaTime;

                l_body.AddPosition(l_body.Velocity * l_deltaTime + l_body.Acceleration * (l_deltaTime * l_deltaTime * 0.5f));

                l_body.Acceleration = Vector2.zero;
  
                /*l_body.VelocityAng += l_body.AccelerationAng * l_deltaTime;

                l_body.AddAngle(l_body.VelocityAng * l_deltaTime);

                l_body.VelocityAng = 0;*/
            }
        }
        
        private static void ResolveCollision(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            var l_bodyATypeCollider = p_bodyA.GetTypeCollider();
            var l_bodyBTypeCollider = p_bodyB.GetTypeCollider();

            switch (l_bodyATypeCollider)
            {
                case TypeCollider.Circle when l_bodyBTypeCollider == TypeCollider.Circle:
                    CircleWithCircleResolveCollision(p_bodyA, p_bodyB);
                    break;
                case TypeCollider.Box when l_bodyBTypeCollider == TypeCollider.Box:
                    BoxWithBoxResolveCollision(p_bodyA, p_bodyB);
                    break;
                default:
                    CircleWithBoxResolveCollision(p_bodyA, p_bodyB);
                    break;
            }
        }
        
        private static void BoxWithBoxResolveCollision(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            var l_dir = (p_bodyA.GetPosition() - p_bodyB.GetPosition()).normalized;
            
            if (!p_bodyA.GetStatic())
                p_bodyA.Velocity = Vector2.Reflect(p_bodyA.Velocity, l_dir);
            
            if (!p_bodyB.GetStatic())
                p_bodyB.Velocity = Vector2.Reflect(p_bodyB.Velocity, -l_dir);
        }

        private static void CircleWithCircleResolveCollision(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            var l_dir = (p_bodyA.GetPosition() - p_bodyB.GetPosition()).normalized;
            var l_parallel = new Vector2(l_dir.x, -l_dir.y);

            var l_u1 = p_bodyA.Velocity.Climb(l_dir);
            var l_u2 = p_bodyB.Velocity.Climb(l_dir);
            
            var l_velPar1 = p_bodyA.Velocity.Climb(l_parallel);
            var l_velPar2 = p_bodyB.Velocity.Climb(l_parallel);

            var l_mass1 = p_bodyA.GetMass();
            var l_mass2 = p_bodyB.GetMass();

            var l_sumMass = (l_mass1 + l_mass2);

            var l_v1 = (l_mass1 - l_mass2) / l_sumMass * l_u1 + 2 * l_mass2 / l_sumMass * l_u2;
            var l_v2 = (l_mass2 - l_mass1) / l_sumMass * l_u2 + 2 * l_mass1 / l_sumMass * l_u1;

            if (!p_bodyA.GetStatic())
                p_bodyA.Velocity = l_dir * l_v1 + l_parallel * l_velPar1;
            
            if (!p_bodyB.GetStatic())
                p_bodyB.Velocity = l_dir * l_v2 + l_parallel * l_velPar2;
        }
        
        private static void CircleWithBoxResolveCollision(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            PhysicsBody l_circleBody;
            PhysicsBody l_boxBody;
            if (p_bodyA.GetTypeCollider() == TypeCollider.Circle)
            {
                l_circleBody = p_bodyA;
                l_boxBody = p_bodyB;
            }
            else
            {
                l_circleBody = p_bodyB;
                l_boxBody = p_bodyA;
            }

            var l_pointInBox = l_circleBody.GetPosition();
            var l_boxPosition = l_boxBody.GetPosition();
            var l_sizeBox = l_boxBody.GetSizeCollider() / 2;
            
            if (l_pointInBox.x < l_boxPosition.x - l_sizeBox.x)
            {
                l_pointInBox.x = l_boxPosition.x - l_sizeBox.x;
            }
            else if (l_pointInBox.x > l_boxPosition.x + l_sizeBox.x)
            {
                l_pointInBox.x = l_boxPosition.x + l_sizeBox.x;
            }
            
            if (l_pointInBox.y < l_boxPosition.y - l_sizeBox.y)
            {
                l_pointInBox.y = l_boxPosition.y - l_sizeBox.y;
            }
            else if (l_pointInBox.y > l_boxPosition.y + l_sizeBox.y)
            {
                l_pointInBox.y = l_boxPosition.y + l_sizeBox.y;
            }
            
            var l_dir = (l_circleBody.GetPosition() - l_pointInBox).normalized;

            if (!l_boxBody.GetStatic())
                l_boxBody.AddForce(l_circleBody.Velocity);
            
            if (!l_circleBody.GetStatic())
            {
                //l_circleBody.SetPosition(l_pointInBox + (l_circleBody.GetPosition() - l_boxPosition).normalized * l_circleBody.GetRadius());
                l_circleBody.Velocity = Vector2.Reflect(l_circleBody.Velocity, l_dir);
            }
        }

        private static bool CheckCollision(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            var l_typeCollisionA = p_bodyA.GetTypeCollider();
            var l_typeCollisionB = p_bodyB.GetTypeCollider();

            switch (l_typeCollisionA)
            {
                case TypeCollider.Box:
                    switch (l_typeCollisionB)
                    {
                        case TypeCollider.Box:
                            return CollisionUtilities.BoxCollider(p_bodyA.GetPosition(), p_bodyA.GetSizeCollider(),
                                p_bodyB.GetPosition(), p_bodyB.GetSizeCollider());
                            
                        case TypeCollider.Circle:
                        {
                            return CollisionUtilities.IsBoxWithCircleColliding(p_bodyA.GetPosition(), p_bodyA.GetSizeCollider(),
                                p_bodyB.GetPosition(), p_bodyB.GetRadius());
                        }
                        default:
                            Debug.LogError("Invalid TypeCollider");
                            return false;
                    }
                case TypeCollider.Circle:
                    switch (l_typeCollisionB)
                    {
                        case TypeCollider.Circle:
                            return CollisionUtilities.CircleCollider(p_bodyA.GetPosition(), p_bodyA.GetRadius(), p_bodyB.GetPosition(),
                                p_bodyB.GetRadius());
                        case TypeCollider.Box:
                            return CollisionUtilities.IsBoxWithCircleColliding(p_bodyB.GetPosition(), p_bodyB.GetSizeCollider(),
                                p_bodyA.GetPosition(), p_bodyA.GetRadius());
                        default:
                            Debug.LogError("Invalid TypeCollider");
                            return false;
                    }
                default:
                    Debug.LogError("Invalid TypeCollider");
                    return false;
            }
        }

        public static void AddPhysicsBody(PhysicsBody p_newBody)
        {
            m_bodies ??= new List<PhysicsBody>();
            
            if (!m_bodies.Contains(p_newBody))
                m_bodies.Add(p_newBody);
            
            Debug.Log("Add body");
        }
        
        public static void RemovePhysicsBody(PhysicsBody p_removeBody)
        {
            if (m_bodies.Contains(p_removeBody))
                m_bodies.Remove(p_removeBody);
            
            if (m_bodies.Count <= 0)
                m_bodies = default;
        }
    }
}
