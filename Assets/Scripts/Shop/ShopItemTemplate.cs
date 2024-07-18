using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemTemplate : MonoBehaviour
{
    [Header("Item")]
    public ShopItemSO shopItemSO;

    [Header("Canvas Config")]
    [SerializeField] private TextMeshProUGUI itemCostTMP;
    [SerializeField] private TextMeshProUGUI itemDiscountTMP;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;
    [SerializeField] private Button purchaseButton;

    [HideInInspector] public int price;
    [HideInInspector] public float discountMultiplier;

    private void Start()
    {
        AssignPurchaseEvent();
    }
    private void Update()
    {
        if (discountMultiplier > 0)
        {
            itemDiscountTMP.gameObject.SetActive(true);
            itemDiscountTMP.text = (GetDiscountedPrice()).ToString("0$");
        }
        else
        {
            itemDiscountTMP.gameObject.SetActive(false);
        }
    }

    public void AssignItemData()
    {
        //RESETS TO TRUE POR SI ACASO
        shopItemSO.canBuy = true;

        //itemImage.sprite = shopItemSO.itemImage;
        discountMultiplier = 0;
        price = shopItemSO.itemCost;
        itemCostTMP.text = price.ToString("0$");
        itemNameTMP.text = shopItemSO.itemName;
        itemDescriptionTMP.text = shopItemSO.itemDescription;
    }

    private void AssignPurchaseEvent()
    {
        purchaseButton.onClick.AddListener(() => shopItemSO.Buy(GetDiscountedPrice()));
    }

    public void ResetPurchaseEvent() => purchaseButton.onClick.RemoveAllListeners();

    int GetDiscountedPrice()
    {
        return (int)(price - price * discountMultiplier);
    }
}
