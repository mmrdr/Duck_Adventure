using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private CannonSpawner cannonSpawner;

    private void Awake()
    {
        cannonSpawner.OnPlayerWinEvent += PlayerWin_OnPlayerWinEvent;
    }

    private void PlayerWin_OnPlayerWinEvent(object sender, System.EventArgs e)
    {
        portal.SetActive(true);
    }
}
