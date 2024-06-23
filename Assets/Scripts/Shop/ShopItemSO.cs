using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuyAction
{
    pistol,
    rifle,
    maxHealth,
    speed,
    damage,
    fireRate,
}

[CreateAssetMenu(fileName = "Shop Item", menuName = "Item SO", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public int itemCost;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    [Space(25)]
    public BuyAction buyAction;

    private ItemsEventManager itemEventManager;
    private Weapon weapon;

    private bool HasMoney()
    {
        if (PlayerManager.Instance.playerStats.Money < itemCost)
        {
            itemEventManager.WarningAdvice(itemEventManager.noMoneyWarning);
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SubstractMoney() => PlayerManager.Instance.playerStats.Money -= itemCost;

    public void Buy()
    {
        if (!HasMoney())
            return;

        itemEventManager = ItemsEventManager.Instance;
        weapon = PlayerManager.Instance.player.GetComponentInChildren<Weapon>();

        switch (buyAction)
        {
            case BuyAction.pistol:
                if (weapon.weaponType == WeaponType.pistol)
                    break;

                weapon.ChangeWeapon(WeaponType.pistol);
                SubstractMoney();

                break;

            case BuyAction.rifle:
                if (weapon.weaponType == WeaponType.rifle)
                    break;

                weapon.ChangeWeapon(WeaponType.rifle);
                SubstractMoney();

                break;

            case BuyAction.maxHealth:
                itemEventManager.MaxHealth();
                SubstractMoney();
                break;

            case BuyAction.speed:
                itemEventManager.Speed();
                SubstractMoney();
                break;

            case BuyAction.damage:
                itemEventManager.Damage();
                SubstractMoney();
                break;

            case BuyAction.fireRate:
                itemEventManager.FireRate();
                SubstractMoney();
                break;
        }
    }

}
