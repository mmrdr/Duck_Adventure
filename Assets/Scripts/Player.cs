using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private InGameMenuUI gameMenuUI;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] private LayerMask portalLayerMask;
    private Vector3 lastInteractDirection;
    private bool isWalking;
    private bool isInteractedWithPortal;
    private bool isOnPortal;
    private const string CANNON_BALL = "Cannon Ball";
    private Portal portal;

    public event EventHandler OnCannonBallHit;
    public event EventHandler OnPortalInteract;

    private void Start()
    {
        gameInput.OnInteractPerformed += GameInput_OnInteractPerformed;
    }

    private void GameInput_OnInteractPerformed(object sender, EventArgs e)
    {
        if (portal != null)
        {
            Debug.Log("Нажал на портал");
            isInteractedWithPortal = true;
        }
    }

    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerPortalCollision();
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

    private void HandlePlayerPortalCollision()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        float interactDistance = 2f;
        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, portalLayerMask))
        {
            isOnPortal = true;
            OnPortalInteract?.Invoke(this, EventArgs.Empty);

            if (hit.transform.TryGetComponent(out Portal portal)) 
            {
                if (this.portal != portal)
                {
                    SetPortal(portal);
                }
            }
            else
            {
                SetPortal(null);
            }
        }
        else
        {
            isOnPortal = false;
            SetPortal(null);
        }
    }

    private void SetPortal(Portal portal)
    {
        this.portal = portal;
    }

    public bool IsWalking() => isWalking;

    public bool IsOnPortal() => isOnPortal;

    public bool IsInteractedWithPortal() => isInteractedWithPortal;
}
