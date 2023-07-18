using System;
using UnityEngine;

namespace _main.Scripts.PhysicsEngine
{
    public static class CollisionUtilities
    {
        public static bool BoxCollider(PhysicsBody p_bodyA, PhysicsBody p_bodyB)
        {
            var l_posA = p_bodyA.GetPosition();
            var l_posB = p_bodyB.GetPosition();
            var l_sizeA = p_bodyA.GetSizeCollider() / 2;
            var l_sizeB = p_bodyA.GetSizeCollider() / 2;

            return l_posA.x + l_sizeA.x > l_posB.x - l_sizeB.x &&
                   l_posA.y + l_sizeA.y > l_posB.y - l_sizeB.y &&
                   l_posA.x - l_sizeA.x < l_posB.x + l_sizeB.x &&
                   l_posA.y - l_sizeA.y < l_posB.y + l_sizeB.y;
        }

        public static bool CircleCollider(Vector2 p_position1, float p_radio1, Vector2 p_position2, float p_radio2)
        {
            var l_distance = Math.Sqrt((p_position1.x * p_position2.x) + (p_position1.y * p_position2.y));

            return l_distance < p_radio1 + p_radio2;
        }

        public static bool IsBoxWithCircleColliding(Vector2 p_boxPosition, Vector2 p_boxSize, Vector2 p_circlePosition,
            float p_circleRadius)
        {
            var l_position = p_circlePosition;
            var l_boxSizeHalfExtend = p_boxSize / 2;
            if (l_position.x < p_boxPosition.x - l_boxSizeHalfExtend.x)
            {
                l_position.x = p_boxPosition.x - l_boxSizeHalfExtend.x;
            }
            else if (l_position.x > p_boxPosition.x + l_boxSizeHalfExtend.x)
            {
                l_position.x = p_boxPosition.x + l_boxSizeHalfExtend.x;
            }
            
            if (l_position.y < p_boxPosition.y - l_boxSizeHalfExtend.y)
            {
                l_position.y = p_boxPosition.y - l_boxSizeHalfExtend.y;
            }
            else if (l_position.y > p_boxPosition.y + l_boxSizeHalfExtend.y)
            {
                l_position.y = p_boxPosition.y + l_boxSizeHalfExtend.y;
            }

            var l_vectorDistance = p_circlePosition - l_position;

            var l_distance = Math.Sqrt(l_vectorDistance.x * l_vectorDistance.x + l_vectorDistance.y * l_vectorDistance.y);

            return l_distance <= p_circleRadius;
        }
    }
}