using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource menuAudio;

    private void Awake()
    {
        menuAudio.Play();
        playButton.onClick.AddListener(() =>
        {
            menuAudio.Stop();
            SceneManager.LoadScene(1);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
