using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Item SO", order = 1)]
public class ShopItemSO : ScriptableObject
{
    [HideInInspector] public string Id;

    [Header("Item Setup")]
    public int itemCost;
    public Sprite itemImage;
    public string itemName;
    [TextArea] public string itemDescription;

    public UnityEvent buyEvent;

    private void OnEnable()
    {
        Id = Guid.NewGuid().ToString();
    }

    #region Weapons

    public void Pistol()
    {
        WeaponPistol pistol = PlayerManager.Instance.playerWeapon.GetComponent<WeaponPistol>(); 

        if (pistol != null)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(1);
            return;
        }

        PlayerManager.Instance.player.ChangeWeapons(0);
    }

    public void Rifle()
    {
        WeaponRifle rifle = PlayerManager.Instance.playerWeapon.GetComponent<WeaponRifle>();

        if (rifle != null)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(3);
            return;
        }

        PlayerManager.Instance.player.ChangeWeapons(1);
    }

    #endregion

    #region Items

    public void ModifyMaxHealth(float value) => PlayerManager.Instance.playerStats.ModifyMaxHealth(value);

    public void ModifyHealth(float value) => PlayerManager.Instance.playerStats.Health -= value;

    public void ModifyLifeRecovery(float value) => PlayerManager.Instance.playerStats.ModifyLifeRecovery(value);

    public void ModifyHarvesting(float value) => PlayerManager.Instance.playerStats.ModifyHarvesting(value);

    public void ModifyLuck(float percentage) => PlayerManager.Instance.playerStats.ModifyLuck(percentage);

    public void ModifySpeed(float value) => PlayerManager.Instance.playerStats.ModifySpeed(value);

    public void ModifyEvasion(float percentage) => PlayerManager.Instance.playerStats.ModifyEvasion(percentage);

    public void ModifyArmor(float percentage) => PlayerManager.Instance.playerStats.ModifyArmor(percentage);

    public void ModifyDamage(float value) => PlayerManager.Instance.playerStats.ModifyDamage(value);

    public void ModifyPercentageOfCriticalDamage(float percentage) => PlayerManager.Instance.playerStats.ModifyPercentageOfCriticalDamage(percentage);

    public void ModifyCriticalDamageMult(float percentage) => PlayerManager.Instance.playerStats.ModifyCriticalDamageMult(percentage);

    public void ModifyMagicDamage(float value) => PlayerManager.Instance.playerStats.ModifyMagicDamage(value);

    public void ModifyMagicRecovery(float percentage) => PlayerManager.Instance.playerStats.ModifyMagicRecovery(percentage);

    #endregion

    #region Effects
    public void ActivateDebugBullets()
    {
        EffectManager.Instance.DebuggingEffect = true;
    }

    #endregion

}
