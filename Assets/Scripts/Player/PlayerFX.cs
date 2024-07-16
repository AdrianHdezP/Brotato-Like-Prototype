using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    private Player player;
    private PlayerStats playerStats;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    [SerializeField] private Material flashFX;
    [SerializeField] private Material weaponDefaultMaterial;
    private Material myMaterial;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerStats = PlayerManager.Instance.playerStats;
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

        myMaterial = playerSpriteRenderer.material;
    }

    private void Update()
    {
        InvencibilityFX();
    }

    private void InvencibilityFX()
    {
        weaponSpriteRenderer = PlayerManager.Instance.playerWeapon.gameObject.GetComponent<SpriteRenderer>();

        if (playerStats.isInvencible == true)
        {
            playerSpriteRenderer.material = flashFX;
            weaponSpriteRenderer.material = flashFX;
        }
        else
        {
            playerSpriteRenderer.material = myMaterial;
            weaponSpriteRenderer.material = weaponDefaultMaterial;
        }
    }

}
