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

   
    #endregion

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
        PlayerManager.Instance.SubstractMoney(itemCost);
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
        PlayerManager.Instance.SubstractMoney(itemCost);
    }

    #endregion

    #region Items

    public void MaxHealth()
    {
        PlayerManager.Instance.playerStats.maxHealth += 5;
        PlayerManager.Instance.playerStats.Health += 5;
        PlayerManager.Instance.SubstractMoney(itemCost);
    }

    public void Speed()
    {
        PlayerManager.Instance.playerStats.speed += 1;
        PlayerManager.Instance.SubstractMoney(itemCost);
    }

    public void Damage()
    {
        PlayerManager.Instance.playerStats.damage += 5;
        PlayerManager.Instance.SubstractMoney(itemCost);
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
