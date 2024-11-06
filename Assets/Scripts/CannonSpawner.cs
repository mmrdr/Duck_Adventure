using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject playerScoreObject;
    [SerializeField] private DefeatMenuUI defeatUI;
    private TextMeshProUGUI playerScoreText;
    private float radius = 9.5f;
    private int cannonAmount = 0;
    private Vector3 center = new Vector3(1.24f, 0.75f, -0.2f);

    public event EventHandler OnPlayerWinEvent;

    private void Update()
    {
        foreach (GameObject cannon in GameObject.FindGameObjectsWithTag("Cannon"))
        {
            cannon.transform.LookAt(shootPoint.transform.position);
        }
    }

    private void Awake()
    {
        defeatUI.OnDefeatHandler += CannonSpawner_OnDefeatHandler;
        playerScoreObject.SetActive(true);
        playerScoreText = playerScoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void CannonSpawner_OnDefeatHandler(object sender, System.EventArgs e)
    {
        playerScoreObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(CannonSpawn());
    }

    IEnumerator CannonSpawn()
    {
        while(cannonAmount <= 7)
        {
            UpdateScore();
            Spawn(cannonAmount);
            yield return new WaitForSeconds(2f);
            DestroyCannons();      
            ++cannonAmount;
        }
        OnPlayerWinEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 spawnPosition = new Vector3(
                center.x + Mathf.Cos(angle) * radius,
                center.y,
                center.z + Mathf.Sin(angle) * radius
            );
            GameObject cannonObject = Instantiate(cannon, spawnPosition, Quaternion.identity);
            cannonObject.transform.LookAt(shootPoint.transform.position);
        }
    }

    private void DestroyCannons()
    {
        if (cannonAmount <= 7)
        {
            foreach (GameObject cannon in GameObject.FindGameObjectsWithTag("Cannon"))
            {
                Destroy(cannon);
            }
        }
    }

    private void UpdateScore()
    {
        playerScoreText.text = "Wave: " + cannonAmount + " cannons";
        CalculateRecord.UpdateHighScore(cannonAmount);
    }

    public int CannonCount() => cannonAmount;
}
