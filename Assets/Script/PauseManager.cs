using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeButton.onClick.AddListener(ResumeGame);
    }

    private void PauseGame()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
