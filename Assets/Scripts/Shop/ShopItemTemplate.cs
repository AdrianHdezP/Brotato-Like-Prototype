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
        //Resets can buy to true
        shopItemSO.canBuy = true;

        #region Setup

        discountMultiplier = 0;
        price = shopItemSO.itemCost;
        itemCostTMP.text = price.ToString("0$");
        itemNameTMP.text = shopItemSO.itemName;
        //itemImage.sprite = shopItemSO.itemImage;
        itemDescriptionTMP.text = shopItemSO.itemDescription;

        #endregion

    }

    public void AssignPurchaseEvent() => purchaseButton.onClick.AddListener(() => shopItemSO.Buy(GetDiscountedPrice()));

    private int GetDiscountedPrice() => (int)(price - price * discountMultiplier);

}
