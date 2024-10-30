using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject defeatMenuUI;
    [SerializeField] private Player player;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    private bool isDefeat = false;

    private void Awake()
    {
        player.OnCannonBallHit += DefeatMenuUI_OnCannonBallHit;

        retryButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        quitButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
    }

    private void DefeatMenuUI_OnCannonBallHit(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;
        defeatMenuUI.SetActive(true);
        isDefeat = true;
    }

    public bool IsDefeat() => isDefeat;
}
