using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Item SO", order = 1)]
public class ShopItemSO : ScriptableObject
{
    [Header("ID")]
    public int ID;
    public string newId;

    [Header("Item Setup")]
    public int itemCost;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;

    public UnityEvent buyEvent;

    [HideInInspector] public bool canBuy = true;

    private void OnEnable()
    {
        newId = System.Guid.NewGuid().ToString();
    }

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

    #region Basic Items

    public void SmallLifePotion() => PlayerManager.Instance.playerStats.ModifyMaxHealth(1);

    public void CombatSyringes() => PlayerManager.Instance.playerStats.ModifyMaxHealth(1);

    public void BloodTransfer() => PlayerManager.Instance.playerStats.ModifyMaxHealth(2.5f);

    #endregion

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
            ShopManager.Instance.LoadItems(true, ID);
        }

        canBuy = true;
    }

}
