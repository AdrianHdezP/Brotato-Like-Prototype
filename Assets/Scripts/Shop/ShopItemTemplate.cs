using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemTemplate : MonoBehaviour
{

    [Header("ItemType")]
    [SerializeField] ShopItemSO shopItemSO;
    public ShopItemSO ShopItemSO
    {
        get 
        {
            return shopItemSO;
        }
        set
        { 
            shopItemSO = value;
            AssignItemData();
        }
    }

    [Header("Animator")]
    public Animator animator;

    [Header("Canvas Config")]
    [SerializeField] private TextMeshProUGUI itemCostTMP;
    [SerializeField] private TextMeshProUGUI itemDiscountTMP;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Image lockImage;

    [HideInInspector] public int price;
    [HideInInspector] public float discountMultiplier;

    public bool isLocked { get; private set; }


    private void Start()
    {       
        if (shopItemSO != null) ShopItemSO = shopItemSO;
    }

    private void OnEnable()
    {
        //animator.SetFloat("Speed", ShopManager.Instance.shopRefreshSpeed);
        //animator.SetTrigger("Enter");
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

    void AssignItemData()
    {

        if (ShopItemSO != null)
        {

            // Debug.Log("DATA ASSIGNED TO ITEM " + name);

            discountMultiplier = 0;
            price = ShopItemSO.itemCost;
            itemCostTMP.text = price.ToString("0$");
            itemNameTMP.text = ShopItemSO.itemName;
            //itemImage.sprite = shopItemSO.itemImage;
            itemDescriptionTMP.text = ShopItemSO.itemDescription;
        }
        else
        {
            //Debug.Log("DATA REMOVED FROM ITEM " + name);

            discountMultiplier = 0;
            price = 99999999;
            itemCostTMP.text = price.ToString("0$");
            itemNameTMP.text = "NULL OBJECT";
            //itemImage.sprite = shopItemSO.itemImage;
            itemDescriptionTMP.text = "NULL DESCRIPTION";
        }

    }

 //   public void AssignPurchaseEvent() => purchaseButton.onClick.AddListener(() => ShopItemSO.Buy(GetDiscountedPrice()));

    private int GetDiscountedPrice() => (int)(price - price * discountMultiplier);

    public void ToggleLockState()
    {
        if (isLocked) 
            UnlockItem();
        else 
            LockItem();
    }

    public void LockItem()
    {
        isLocked = true;
        lockImage.color = Color.grey;
        lockImage.GetComponentInChildren<TextMeshProUGUI>().text = "UNLOCK";
        ShopManager.Instance.SetRerollButtonText();
    }

    public void UnlockItem()
    {
        isLocked = false;
        lockImage.color = Color.white;
        lockImage.GetComponentInChildren<TextMeshProUGUI>().text = "LOCK";
        ShopManager.Instance.SetRerollButtonText();
    }

    public void Buy()
    {
        int totalCost = GetDiscountedPrice();

        if (!PlayerManager.Instance.HasMoney(totalCost) || shopItemSO == null)
        {
            return;
        }

        shopItemSO.buyEvent.Invoke();

        PlayerManager.Instance.SubstractMoney(totalCost);
        ShopManager.Instance.LoadItems(true, shopItemSO.Id);
    }

}
