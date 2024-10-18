using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;

    private void Update()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }
}
