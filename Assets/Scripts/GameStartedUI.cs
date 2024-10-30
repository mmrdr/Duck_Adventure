using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartedUI : MonoBehaviour
{
    [SerializeField] private GameObject countDownObject;
    private TextMeshProUGUI countDownText;

    private void Awake()
    {
        countDownObject.SetActive(true);
        countDownText = countDownObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        Time.timeScale = 0f;
        for (int i = 3; i > 0; --i)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countDownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        countDownObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
