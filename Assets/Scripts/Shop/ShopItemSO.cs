using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuyAction
{
    pistol,
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

    private bool HasMoney()
    {
        if (PlayerManager.Instance.playerStats.Money < itemCost)
        {
            itemEventManager.WarningAdvice(itemEventManager.noMoneyWarning);
            return false;
        }
        else
        {
            PlayerManager.Instance.playerStats.Money -= itemCost;
            return true;
        }
    }

    public void Buy()
    {
        if (!HasMoney())
            return;

        itemEventManager = ItemsEventManager.Instance;

        switch (buyAction)
        {
            case BuyAction.pistol:
                Debug.Log("Pistol was purchase");
                break;

            case BuyAction.maxHealth:
                itemEventManager.MaxHealth();
                break;

            case BuyAction.speed:
                itemEventManager.Speed();
                break;

            case BuyAction.damage:
                itemEventManager.Damage();
                break;

            case BuyAction.fireRate:
                itemEventManager.FireRate();
                break;
        }
    }

}
