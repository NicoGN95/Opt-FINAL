using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using _main.Scripts.Controllers;
using _main.Scripts.Grid;
using _main.Scripts.Pools;
using UnityEngine;

namespace _main.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private BallController ballPrefab;
        [SerializeField] private string levelScene;
        [SerializeField] private string mainMenuScene;
        [SerializeField] private string gameOverScene;

        public BlockGridGenerator blockGrid;

        private List<BallController> m_ballsOnScreen = new List<BallController>();
        private PoolGeneric<BallController> m_ballPool;
        private PlayerController m_localPlayer;
        private int m_lifePoints;
        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            m_lifePoints = 3;
            m_ballPool = new PoolGeneric<BallController>(ballPrefab);
        }
        public void LostBall(BallController p_ballController)
        {
            p_ballController.gameObject.SetActive(false);
            m_lifePoints -= 1;
            UiManager.Instance.AddLifePoints(-1);
            m_ballsOnScreen.Remove(p_ballController);
            ReturnBallToPool(p_ballController);


            if (m_ballsOnScreen.Count <= 0)
            {
                var l_ball = GetBallFromPool();
                l_ball.Initialize(new Vector3(0,8),Vector3.down*3);
                m_ballsOnScreen.Add(l_ball);
            }
        }
        
        public BallController GetBallFromPool() => m_ballPool.GetOrCreate();

        public void ReturnBallToPool(BallController p_ball) => m_ballPool.AddPool(p_ball);

        public void SetLocalPlayer(PlayerController p_newLocalPlayer) => m_localPlayer = p_newLocalPlayer;
        public void SetGrid(BlockGridGenerator p_grid) => blockGrid = p_grid;
        
        
    #region PowerUps

        public void MultiBall()
        {
            var l_prevBalls = m_ballsOnScreen;

            for (int i = 0; i < l_prevBalls.Count; i++)
            {
                var l_currBall = l_prevBalls[i];
                var l_currDir = l_currBall.GetCurrDirVec();
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
            m_lifePoints += p_i;
            UiManager.Instance.AddLifePoints(p_i);
        }

    #endregion
        
    }
}