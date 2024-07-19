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
    private WaveManager waveManager;

    [Header("Canvas Config")]
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private TextMeshProUGUI refreshTMP;
    [SerializeField] private Button nextRoundButton;

    [Header("Stats Config")]
    [SerializeField] private TextMeshProUGUI maxHealthTMP;
    [SerializeField] private TextMeshProUGUI speedTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI fireRateTMP;

    [Header("Items Config")]
    private List<int> loadItemsID;
    [SerializeField] private ShopItemTemplate[] shopItems;
    [SerializeField] private ShopItemSO[] basicItemsSO;
    [SerializeField] private ShopItemSO[] communItemsSO;
    [SerializeField] private ShopItemSO[] rareItemsSO;
    [SerializeField] private ShopItemSO[] veryRareItemsSO;
    [SerializeField] private ShopItemSO[] legendaryItemsSO;

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
        waveManager = WaveManager.Instance;

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
        CalculateItemOdd();
    }

    #region Setup

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
                //item.ResetPurchaseEvent();
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

    #endregion

    #region Shop Mehods

    public ShopItemSO GetItemOfType()
    {

        float random = Random.Range(0f, 100f);

        if (random <= c_LegendaryOdds)
        {
            int randomItem = Random.Range(0, legendaryItemsSO.Length);
            return legendaryItemsSO[randomItem];
        }
        else if (random <= c_LegendaryOdds + c_VeryRareOdds)
        {
            int randomItem = Random.Range(0, veryRareItemsSO.Length);
            return veryRareItemsSO[randomItem];
        }
        else if (random <= c_LegendaryOdds + c_VeryRareOdds + c_RareOdds)
        {
            int randomItem = Random.Range(0, rareItemsSO.Length);
            return rareItemsSO[randomItem];
        }
        else if (random <= c_LegendaryOdds + c_VeryRareOdds + c_RareOdds + c_CommunOdds)
        {
            int randomItem = Random.Range(0, communItemsSO.Length);
            return communItemsSO[randomItem];
        }
        else
        {
            int randomItem = Random.Range(0, basicItemsSO.Length);
            return basicItemsSO[randomItem];
        }
    }

    public void LoadItems()
    {
        loadItemsID = new List<int>();
        loadItemsID.Clear();

        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopItemSO current = GetItemOfType();

            shopItems[i].shopItemSO = current;

            loadItemsID.Add(shopItems[i].shopItemSO.ID);
            shopItems[i].AssignItemData();

            shopItems[i].AssignPurchaseEvent();
        }

        CheckForDiscounts();
    }

    private void CalculateItemOdd()
    {
        //float value = Mathf.Clamp01((float) WaveManager.Instance.round + minRound_Basic / WaveManager.Instance.maxRound);

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

        float _BasicOdds = Mathf.Lerp(i_BasicOdds, f_BasicOdds, Mathf.Clamp01((float)(WaveManager.Instance.round - minRound_Basic) / (WaveManager.Instance.maxRound - minRound_Basic)));

        float _CommunOdds = Mathf.Lerp(i_CommunOdds, f_CommunOdds, Mathf.Clamp01((float)(WaveManager.Instance.round - minRound_Commun) / (WaveManager.Instance.maxRound - minRound_Commun)));

        float _RareOdds = Mathf.Lerp(i_RareOdds, f_RareOdds, Mathf.Clamp01((float)(WaveManager.Instance.round - minRound_Rare) / (WaveManager.Instance.maxRound - minRound_Rare)));

        float _VeryRareOdds = Mathf.Lerp(i_VeryRareOdds, f_VeryRareOdds, Mathf.Clamp01((float)(WaveManager.Instance.round - minRound_VeryRare) / (WaveManager.Instance.maxRound - minRound_VeryRare)));

        float _LegendaryOdds = Mathf.Lerp(i_LegendaryOdds, f_LegendaryOdds, Mathf.Clamp01((float)(WaveManager.Instance.round - minRound_Legendary) / (WaveManager.Instance.maxRound - minRound_Legendary)));

        float total = _BasicOdds + _CommunOdds + _RareOdds + _VeryRareOdds + _LegendaryOdds;


        _BasicOdds /= total;
        _CommunOdds /= total;
        _RareOdds /= total;
        _VeryRareOdds /= total;
        _LegendaryOdds /= total;

        c_BasicOdds = _BasicOdds * 100;
        c_CommunOdds = _CommunOdds * 100;
        c_RareOdds = _RareOdds * 100;
        c_VeryRareOdds = _VeryRareOdds * 100;
        c_LegendaryOdds = _LegendaryOdds * 100;
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
        if (!PlayerManager.Instance.HasMoney(shopRefreshCost)) 
            return;

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
