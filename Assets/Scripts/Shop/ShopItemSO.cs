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

    [HideInInspector] public bool canBuy = true;

    #region Weapons

    public void Pistol()
    {
        WeaponPistol pistol = PlayerManager.Instance.playerWeapon.GetComponent<WeaponPistol>(); 

        if (pistol != null)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(1);
            canBuy = false;
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

    public void MaxHealth()
    {
        PlayerManager.Instance.playerStats.maxHealth += 5;
        PlayerManager.Instance.playerStats.Health += 5;
    }

    public void Speed()
    {
        PlayerManager.Instance.playerStats.speed += 1;
    }

    public void Damage()
    {
        PlayerManager.Instance.playerStats.damage += 5;
    }

    #endregion

    public void Buy(int cost)
    {
        if (!canBuy) 
            return;

        if (!PlayerManager.Instance.HasMoney(cost))
        {       
            return;
        }

        buyEvent.Invoke();

        if (canBuy)
        {
            PlayerManager.Instance.SubstractMoney(cost);
            ShopManager.Instance.LoadItems();
        }

        canBuy = true;
    }

}
