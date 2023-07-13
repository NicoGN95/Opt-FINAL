using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject creditsPanel;

    private void Awake()
    {
        creditsPanel.SetActive(false);
        
        playButton.onClick.AddListener(OnPlayButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    private void OnCreditsButtonClicked()
    {
        creditsPanel.SetActive(true);
    }
    private void OnBackButtonClicked()
    {
        creditsPanel.SetActive(false);
    }
    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
