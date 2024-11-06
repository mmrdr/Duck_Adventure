using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string IS_INTERACTED_WITH_PORTAL = "IsInteractedWithPortal";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_INTERACTED_WITH_PORTAL, player.IsInteractedWithPortal());
    }
}
