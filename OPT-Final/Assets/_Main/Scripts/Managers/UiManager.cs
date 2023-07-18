using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _main.Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ballText;
        public static UiManager Instance;
        private int m_ballsLeft;

        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }
            
            
            m_ballsLeft = 3;
            ballText.text = m_ballsLeft.ToString();
        }

        public void AddLifePoints(int p_change)
        {
            m_ballsLeft += p_change;
            ballText.text = m_ballsLeft.ToString();

        }
    }
}