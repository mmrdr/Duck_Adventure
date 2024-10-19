using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rock;

    private void Start()
    {
        StartCoroutine(SpawnRock());
    }

    IEnumerator SpawnRock()
    {
        while(true)
        {
            Instantiate(rock, getRandomVector(), Quaternion.identity);
            float randomIntervalWait = Random.Range(1f, 2f);
            yield return new WaitForSeconds(randomIntervalWait);
        }
    }

    private Vector3 getRandomVector()
    {
        Vector3 vector = new Vector3();
        vector.x = Random.Range(-5f, 5f);
        vector.y = 0.5f;
        vector.z = 4f;
        return vector;
    }
}
