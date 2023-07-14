using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button creditsBackButton;
    [SerializeField] private Button controlsBackButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject creditsPanel;    
    [SerializeField] private GameObject controlsPanel;


    private void Awake()
    {
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        
        playButton.onClick.AddListener(OnPlayButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        controlsButton.onClick.AddListener(OnControlsButtonClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);
        controlsBackButton.onClick.AddListener(OnControlsBackButtonClicked);
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
    private void OnControlsButtonClicked()
    {
        controlsPanel.SetActive(true);
    }
    private void OnCreditsBackButtonClicked()
    {
        creditsPanel.SetActive(false);
    }
    private void OnControlsBackButtonClicked()
    {
        controlsPanel.SetActive(false);
    }
    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
