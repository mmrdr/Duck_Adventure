using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject portalSidesVisual;
    [Range(0, 255)] private int activeRed = 80;
    [Range(0, 255)] private int activeGreen = 255;
    [Range(0, 255)] private int activeBlue = 255;

    [Range(0, 255)] private int defaultRed = 193;
    [Range(0, 255)] private int defaultGreen = 85;
    [Range(0, 255)] private int defaultBlue = 255;

    private ParticleSystem portalParticle;

    private void Start()
    {
        player.OnPortalInteract += Player_OnPortalInteract;
        portalParticle = portalSidesVisual.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!player.IsOnPortal())
        {
            SetParticleColor(defaultRed, defaultGreen, defaultBlue);
            Debug.Log("Теперь обычный цвет");
        }
    }

    private void Player_OnPortalInteract(object sender, System.EventArgs e)
    {
        Debug.Log("Меняем цвет");
        SetParticleColor(activeRed, activeGreen, activeBlue);
    }

    private void SetParticleColor(int r, int g, int b)
    {
        if (portalParticle != null)
        {
            var mainModule = portalParticle.main;
            mainModule.startColor = new Color(r / 255f, g / 255f, b / 255f);
        }
    }
}
