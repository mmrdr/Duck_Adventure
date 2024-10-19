using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMover : MonoBehaviour
{
    private Rigidbody rb;
    private float rockMoveSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(Vector3.back * rockMoveSpeed * Time.deltaTime, ForceMode.Impulse); 
        if (rb.position.y < 0)
        {
            Destroy(rb.gameObject);
        }
    }
}
