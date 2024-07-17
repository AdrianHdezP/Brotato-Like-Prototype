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
        if (!waveManager.isInRound)
        {
            shopCanvas.SetActive(true);
            playerStats.canMove = false;
        }
        else
        {
            shopCanvas.SetActive(false);
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

    private void UpdateMoneyTMP() => moneyTMP.text = playerStats.Money + "$";

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

            CheckForDuplicateItems(i);
        }
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

    public void RefreshShop()
    {
        LoadItems();
        PlayerManager.Instance.SubstractMoney(shopRefreshCost);
        shopRefreshCost = (int)(shopRefreshCost * shopCostMultiplier);
        refreshTMP.text = "REFRESH (" + shopRefreshCost + "$)";
    }
    public void ResetShopRefreshCost()
    {
        shopRefreshCost = baseShopRefreshCost;
    }

    #endregion

}
