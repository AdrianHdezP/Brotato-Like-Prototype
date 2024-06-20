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
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;
    [SerializeField] private Button purchaseButton;

    private void Start()
    {
        AssignPurchaseEvent();
    }

    private void Update()
    {
        AssignItemData();
    }

    private void AssignItemData()
    {
        if (shopItemSO != null)
        {
            itemCostTMP.text = shopItemSO.itemCost.ToString();
            //itemImage.sprite = shopItemSO.itemImage;
            itemNameTMP.text = shopItemSO.itemName;
            itemDescriptionTMP.text = shopItemSO.itemDescription;
        }
        else
        {
            ResetPurchaseEvent();
        }
    }

    private void AssignPurchaseEvent() => purchaseButton.onClick.AddListener(() => shopItemSO.Buy());

    private void ResetPurchaseEvent() => purchaseButton.onClick.RemoveAllListeners();

}
