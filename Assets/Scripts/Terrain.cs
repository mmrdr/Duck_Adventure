using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private const string CANNON_BALL = "Cannon Ball";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CANNON_BALL))
        {
            Destroy(collision.gameObject);
        }
    }
}
