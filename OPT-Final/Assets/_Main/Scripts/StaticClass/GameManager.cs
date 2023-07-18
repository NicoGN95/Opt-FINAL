using System;
using System.Collections.Generic;
using _main.Scripts.Controllers;
using _main.Scripts.Pools;
using UnityEngine;

namespace _main.Scripts.StaticClass
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BallController ballPrefab;
        public GameManager Instance;



        private List<BallController> m_ballsOnScreen = new List<BallController>();
        private PoolGeneric<BallController> m_ballPool;
        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            m_ballPool = new PoolGeneric<BallController>(ballPrefab);
        }


        public void MultiBall()
        {
            var l_prevBalls = m_ballsOnScreen;

            for (int i = 0; i < l_prevBalls.Count; i++)
            {
                var l_currBall = l_prevBalls[i];
                var l_currDir = l_currBall.GetCurrDir();
                //Iniitialize ball toward a similar direction
                var l_ballOne = m_ballPool.GetOrCreate();
                l_ballOne.Initialize(l_currBall.transform.position, Quaternion.Euler(0, 0, 25f) * l_currDir);
                
                var l_ballTwo = m_ballPool.GetOrCreate();
                l_ballTwo.Initialize(l_currBall.transform.position, Quaternion.Euler(0, 0, -25f) * l_currDir);
                
                m_ballsOnScreen.Add(l_ballOne);
                m_ballsOnScreen.Add(l_ballTwo);
            }
        }

        public void InvokeBalls()
        {
            
        }

        public void AddBallCount(int p_i)
        {
            
        }
    }
}