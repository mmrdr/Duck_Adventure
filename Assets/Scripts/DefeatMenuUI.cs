using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatMenuUI : MonoBehaviour
{
    [SerializeField] CannonSpawner spawner;
    [SerializeField] private GameObject defeatMenuUI;
    [SerializeField] private GameObject currentPlayerScoreObject;
    [SerializeField] private Player player;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    private TextMeshProUGUI currentPlayerScoreText;

    public event EventHandler OnDefeatHandler;

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
        currentPlayerScoreText = currentPlayerScoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void DefeatMenuUI_OnCannonBallHit(object sender, EventArgs e)
    {
        OnDefeatHandler?.Invoke(this, EventArgs.Empty);
        currentPlayerScoreText.text = "Result: " + spawner.CannonCount() + " cannons";
        Time.timeScale = 0f;
        defeatMenuUI.SetActive(true);
        isDefeat = true;
    }

    public bool IsDefeat() => isDefeat;
}
