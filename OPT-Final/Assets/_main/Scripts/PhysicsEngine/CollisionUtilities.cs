using System;
using UnityEngine;

namespace _main.Scripts.PhysicsEngine
{
    public static class CollisionUtilities
    {
        public static bool BoxCollider(Vector2 p_position1, Vector2 p_size1, Vector2 p_position2, Vector2 p_size2)
        {
            var l_distance = new Vector2(p_position1.x - p_position2.x, p_position1.y - p_position2.y);

            var l_sumSize = new Vector2((p_size1.x / 2 + p_size2.x / 2), (p_size1.y / 2 + p_size2.y / 2));

            Debug.Log($"{l_distance.x <= l_sumSize.x && l_distance.y <= l_sumSize.y}");
            return l_distance.x <= l_sumSize.x && l_distance.y <= l_sumSize.y;
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
            if (p_circlePosition.x < p_boxPosition.x)
            {
                l_position.x = p_boxPosition.x;
            }
            else if (p_circlePosition.x > p_boxPosition.x + p_boxSize.x)
            {
                l_position.x = p_boxPosition.x + p_boxSize.x;
            }
        
            if (p_circlePosition.y < p_boxPosition.y)
            {
                l_position.y = p_boxPosition.y;
            }
            else if (p_circlePosition.y > p_boxPosition.y + p_boxSize.y)
            {
                l_position.y = p_boxPosition.y + p_boxSize.y;
            }

            var l_vectorDistance = p_circlePosition - l_position;

            var l_distance = Math.Sqrt(l_vectorDistance.x * l_vectorDistance.x + l_vectorDistance.y * l_vectorDistance.y);

            return l_distance <= p_circleRadius;
        }
    }
}