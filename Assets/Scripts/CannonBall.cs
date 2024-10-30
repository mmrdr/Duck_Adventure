using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private const string CANNON_BALL = "Cannon Ball";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CANNON_BALL))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        StartCoroutine(DestroyAfterThreeSeconds());
    }

    private IEnumerator DestroyAfterThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
