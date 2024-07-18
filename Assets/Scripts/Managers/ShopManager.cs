using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;

    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("No instance");
            }

            return instance;
        }
    }

    private Player player;
    private PlayerStats playerStats;
    private Weapon weapon;
    private WaveManager waveManager;

    [Header("Canvas Config")]
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private TextMeshProUGUI moneyTMP;
    [SerializeField] private TextMeshProUGUI refreshTMP;
    [SerializeField] private Button nextRoundButton;

    [Header("Stats Config")]
    [SerializeField] private TextMeshProUGUI maxHealthTMP;
    [SerializeField] private TextMeshProUGUI speedTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI fireRateTMP;

    [Header("Items Config")]
    private List<int> itemsID;
    private List<int> loadItemsID;
    [SerializeField] private ShopItemTemplate[] shopItems;
    [SerializeField] private ShopItemSO[] shopItemSO;

    [Header("Shop config")]
    public int shopRefreshCost;
    public float shopCostMultiplier = 1.3f;
    int baseShopRefreshCost;

    [Header("GenerateOdds")]

    [Header("MIN ODDS")]
    [SerializeField] float i_BasicOdds = 80f;
    [SerializeField] float i_CommunOdds = 20f;
    [SerializeField] float i_RareOdds = 0;
    [SerializeField] float i_VeryRareOdds = 0;
    [SerializeField] float i_LegendaryOdds = 0;

    [Header("MAX ODDS")]
    [SerializeField] float f_BasicOdds = 0f;
    [SerializeField] float f_CommunOdds = 15f;
    [SerializeField] float f_RareOdds = 30f;
    [SerializeField] float f_VeryRareOdds = 40f;
    [SerializeField] float f_LegendaryOdds = 15f;


    [Header("MIN GENERATE ROUND")]
    [SerializeField] int minRound_Basic = 0;
    [SerializeField] int minRound_Commun = 0;
    [SerializeField] int minRound_Rare = 5;
    [SerializeField] int minRound_VeryRare = 10;
    [SerializeField] int minRound_Legendary = 15;

    [Header("CURRENT ODDS")]
    [SerializeField] float c_BasicOdds = 0f;
    [SerializeField] float c_CommunOdds = 15f;
    [SerializeField] float c_RareOdds = 30f;
    [SerializeField] float c_VeryRareOdds = 40f;
    [SerializeField] float c_LegendaryOdds = 15f;

    private void Awake()
    {
        instance = this;
        baseShopRefreshCost = shopRefreshCost;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerStats = PlayerManager.Instance.playerStats;
        weapon = player.GetComponentInChildren<Weapon>();
        waveManager = WaveManager.Instance;

        CheckForDuplicateId();
        SetupButton();
        LoadItems();
    }

    private void OnEnable()
    {
        refreshTMP.text = "REFRESH (" + shopRefreshCost + "$)";
    }

    private void Update()
    {
        SetupCanvas();
        UpdateStats();
        UpdateMoneyTMP();

        CalculateItemOdd();
    }

    #region Setup

    private void CheckForDuplicateId()
    {
        itemsID = new List<int>();
        itemsID.Clear();

        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (itemsID.Contains(shopItemSO[i].ID))
                Debug.LogError("Item ID duplicated");
            else
                itemsID.Add(shopItemSO[i].ID);
        }
    }

    private void SetupCanvas()
    {
        if (!waveManager.isInRound && !shopCanvas.activeSelf)
        {
            ResetShopRefreshCost();
            shopCanvas.SetActive(true);
            playerStats.canMove = false;
        }
        else if (waveManager.isInRound && shopCanvas.activeSelf)
        {
            shopCanvas.SetActive(false);

            foreach(ShopItemTemplate item in shopItems)
            {
                item.ResetPurchaseEvent();
            }

            playerStats.canMove = true;
        }
    }

    private void SetupButton() => nextRoundButton.onClick.AddListener(() => waveManager.NextRound());

    #endregion

    #region Update Methods

    private void UpdateStats()
    {
        maxHealthTMP.text = "Max Health: " + playerStats.maxHealth;

        speedTMP.text = "Speed: " + playerStats.speed;

        damageTMP.text = "Damage: " + playerStats.damage;
    }

    public void UpdateMoneyTMP() => moneyTMP.text = playerStats.Money + "$";

    #endregion

    #region LoadItems

    public void LoadItems()
    {
        loadItemsID = new List<int>();
        loadItemsID.Clear();

        for (int i = 0; i < shopItems.Length; i++)
        {
            int randomItem = Random.Range(0, shopItemSO.Length);

            shopItems[i].shopItemSO = shopItemSO[randomItem];
            loadItemsID.Add(shopItems[i].shopItemSO.ID);
            shopItems[i].AssignItemData();

            //CheckForDuplicateItems(i);
        }

        CheckForDiscounts();
    }
    void CalculateItemOdd()
    {
        float value = Mathf.Clamp01((float)WaveManager.Instance.round / WaveManager.Instance.maxRound);

        //if (WaveManager.Instance.round >= minRound_Basic) c_BasicOdds = Mathf.Lerp(i_BasicOdds, f_BasicOdds, value);
        //else c_BasicOdds = 0;
        //if (WaveManager.Instance.round >= minRound_Commun) c_CommunOdds = Mathf.Lerp(i_CommunOdds, f_CommunOdds, value);
        //else c_CommunOdds = 0;
        //if (WaveManager.Instance.round >= minRound_Rare) c_RareOdds = Mathf.Lerp(i_RareOdds, f_RareOdds, value);
        //else c_RareOdds = 0;
        //if (WaveManager.Instance.round >= minRound_VeryRare) c_VeryRareOdds = Mathf.Lerp(i_VeryRareOdds, f_VeryRareOdds, value);
        //else c_VeryRareOdds = 0;
        //if (WaveManager.Instance.round >= minRound_Legendary) c_LegendaryOdds = Mathf.Lerp(i_LegendaryOdds, f_LegendaryOdds, value);
        //else c_LegendaryOdds = 0;

        c_BasicOdds = Mathf.Lerp(i_BasicOdds, f_BasicOdds, value);
        
        c_CommunOdds = Mathf.Lerp(i_CommunOdds, f_CommunOdds, value);
       
        c_RareOdds = Mathf.Lerp(i_RareOdds, f_RareOdds, value);
        
        c_VeryRareOdds = Mathf.Lerp(i_VeryRareOdds, f_VeryRareOdds, value);
       
        c_LegendaryOdds = Mathf.Lerp(i_LegendaryOdds, f_LegendaryOdds, value);      
    }


    public void CheckForDuplicateItems(int i)
    {
        if (loadItemsID.Contains(shopItems[i].shopItemSO.ID))
        {
            int randomItem = Random.Range(0, shopItemSO.Length);

            shopItems[i].shopItemSO = shopItemSO[randomItem];
            CheckForDuplicateItems(i);
        }
        else
        {
            loadItemsID.Add(shopItems[i].shopItemSO.ID);
        }
    }
    public void CheckForDiscounts()
    {
        for (int i = 0; i < loadItemsID.Count; i++)
        {
            shopItems[i].discountMultiplier = 0;

            for (int j = 0; j < loadItemsID.Count; j++)
            {
                if (i != j && loadItemsID[i] == loadItemsID[j])
                {
                    shopItems[i].discountMultiplier += 0.33f;
                }
            }
        }
    }

    public void RefreshShop()
    {
        if (!PlayerManager.Instance.HasMoney(shopRefreshCost)) return;

        LoadItems();
        PlayerManager.Instance.SubstractMoney(shopRefreshCost);
        shopRefreshCost = (int)(shopRefreshCost * shopCostMultiplier);
        refreshTMP.text = "REFRESH (" + shopRefreshCost + "$)";
    }
    public void ResetShopRefreshCost()
    {
        shopRefreshCost = baseShopRefreshCost;
        refreshTMP.text = "REFRESH (" + shopRefreshCost + "$)";
    }



    #endregion
}
