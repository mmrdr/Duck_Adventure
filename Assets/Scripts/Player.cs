using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;
    private const string ROCK = "Rock";

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ROCK))
        {
            Debug.Log("Столкновение с камнем, конец игры");
            DefeatHandle();
        }
    }

    private void HandlePlayerMovement()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 1f;
        float playerRadius = 0.25f;
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
                    Debug.Log("Застряли");
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

    private void DefeatHandle()
    {
        Time.timeScale = 0f;
        StartCoroutine(RestartLevel(2f));
    }

    private IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
