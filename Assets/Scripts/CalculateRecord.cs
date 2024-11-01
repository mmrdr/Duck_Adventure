using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateRecord : MonoBehaviour
{
    [SerializeField] private GameObject highScoreObject;
    private static TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScoreText = highScoreObject.GetComponent<TextMeshProUGUI>();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScore == 0)
        {
            Debug.Log("-");
        }
        highScoreText.text = "Record: " + highScore + " cannons";
    }

    public static void UpdateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        { 
            highScoreText.text = "Record: " + score + " cannons"; 
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
