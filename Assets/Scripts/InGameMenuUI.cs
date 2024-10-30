using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private DefeatMenuUI defeatMenuUI;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    private bool isPaused = false;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
        });

        quitButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            isPaused = false;
            SceneManager.LoadScene(0);
        });
    }

    private void Update()
    {
        if (defeatMenuUI.IsDefeat())
        {
            // Cant open menu;
        }
        else if (gameInput.IsEscapePressed())
        {
            if (isPaused)
            {
                ResumeGame();
            } 
            else
            {
                PauseGame();
            }
        }
    }

    private void ResumeGame()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void PauseGame()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public bool IsPaused() => isPaused;
}
