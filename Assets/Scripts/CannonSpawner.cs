using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject shootPoint;
    private float radius = 9.5f;
    private int cannonAmount = 1;
    private Vector3 center = new Vector3(1.24f, 0.75f, -0.2f);

    private void Update()
    {
        foreach (GameObject cannon in GameObject.FindGameObjectsWithTag("Cannon"))
        {
            cannon.transform.LookAt(shootPoint.transform.position);
        }
    }

    private void Start()
    {
        StartCoroutine(CannonSpawn());
    }

    IEnumerator CannonSpawn()
    {
        while(true)
        {
            Spawn(cannonAmount);
            yield return new WaitForSeconds(2f);
            DestroyCannons();
            ++cannonAmount;
        }
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
        foreach (GameObject cannon in GameObject.FindGameObjectsWithTag("Cannon"))
        {
            Destroy(cannon);
        }
    }
}
