using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private InGameMenuUI gameMenuUI;
    [SerializeField] float moveSpeed = 7f;
    private bool isWalking;
    private const string CANNON_BALL = "Cannon Ball";

    public event EventHandler OnCannonBallHit;

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CANNON_BALL))
        {
            OnCannonBallHit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandlePlayerMovement()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        isWalking = moveDirection != Vector3.zero;
        float playerHeight = 1f;
        float playerRadius = 0.15f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        if (!canMove)
        {
            Vector3 moveX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveX, moveDistance);
            if (canMove)
            {
                moveDirection = moveX;
            }
            else
            {
                Vector3 moveZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveZ, moveDistance);
                if (canMove)
                {
                    moveDirection = moveZ;
                }
                else
                {
                    Debug.Log("��������");
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() => isWalking;
}
