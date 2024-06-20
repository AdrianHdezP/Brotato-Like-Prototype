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
    [SerializeField] private GameObject weapon;
    private Material myMaterial;
    private Material myWeaponMaterial;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerStats = PlayerManager.Instance.playerStats;
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();

        myMaterial = playerSpriteRenderer.material;
        myWeaponMaterial = weaponSpriteRenderer.material;
    }

    private void Update()
    {
        InvencibilityFX();
    }

    private void InvencibilityFX()
    {
        if (playerStats.isInvencible == true)
        {
            playerSpriteRenderer.material = flashFX;
            weaponSpriteRenderer.material = flashFX;
        }
        else
        {
            playerSpriteRenderer.material = myMaterial;
            weaponSpriteRenderer.material = myWeaponMaterial;
        }
    }

}
