using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Item SO", order = 1)]
public class ShopItemSO : ScriptableObject
{
    [Header("ID")]
    public int ID;

    [Header("Item Setup")]
    public int itemCost;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;

    public UnityEvent buyEvent;

    #region Money

    private bool HasMoney()
    {
        if (PlayerManager.Instance.playerStats.Money < itemCost)
            return false;
        else
            return true;
    }

    private void SubstractMoney() => PlayerManager.Instance.playerStats.Money -= itemCost;

    #endregion

    #region Weapons

    private Weapon weapon;

    public void Pistol()
    {
        weapon = PlayerManager.Instance.player.GetComponentInChildren<Weapon>();

        if (weapon.weaponType == WeaponType.pistol)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(1);
            return;
        }

        PlayerManager.Instance.player.ChangeWeapons(0);
        SubstractMoney();
    }

    public void Shootgun()
    {
        weapon = PlayerManager.Instance.player.GetComponentInChildren<Weapon>();

        if (weapon.weaponType == WeaponType.pistol)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(2);
            return;
        }

        PlayerManager.Instance.player.ChangeWeapons(1);
        SubstractMoney();
    }

    public void Rifle()
    {
        weapon = PlayerManager.Instance.player.GetComponentInChildren<Weapon>();

        if (weapon.weaponType == WeaponType.rifle)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(3);
            return;
        }

        PlayerManager.Instance.player.ChangeWeapons(2);
        SubstractMoney();
    }

    #endregion

    #region Items

    public void MaxHealth()
    {
        PlayerManager.Instance.playerStats.maxHealth += 5;
        PlayerManager.Instance.playerStats.Health += 5;
        SubstractMoney();
    }

    public void Speed()
    {
        PlayerManager.Instance.playerStats.speed += 1;
        SubstractMoney();
    }

    public void Damage()
    {
        PlayerManager.Instance.playerStats.damage += 5;
        SubstractMoney();
    }

    #endregion

    public void Buy()
    {
        if (!HasMoney())
        {
            FindObjectOfType<HUDManager>().SetupWarnings(0);
            return;
        }

        buyEvent.Invoke();
    }

}
