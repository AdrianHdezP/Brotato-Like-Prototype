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
    private WaveManager waveManager;

    [Header("Canvas Config")]
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private TextMeshProUGUI moneyTMP;
    [SerializeField] private Button nextRoundButton;

    [Header("Stats Config")]
    [SerializeField] private TextMeshProUGUI maxHealthTMP;
    [SerializeField] private TextMeshProUGUI speedTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI fireRateTMP;

    [Header("Items Config")]
    [SerializeField] private ShopItemTemplate[] shopItems;
    [SerializeField] private ShopItemSO[] shopItemSO;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerStats = PlayerManager.Instance.playerStats;
        waveManager = WaveManager.Instance;

        SetupButton();
        LoadItems();
    }

    private void Update()
    {
        SetupCanvas();
        UpdateStats();
        UpdateMoneyTMP();
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

    private void UpdateStats()
    {
        maxHealthTMP.text = "Max Health: " + playerStats.maxHealth;
        speedTMP.text = "Speed: " + playerStats.speed;
        damageTMP.text = "Damage: " + playerStats.damage;
        fireRateTMP.text = "Fire Rate: " + playerStats.fireRate;
    }

    private void UpdateMoneyTMP() => moneyTMP.text = playerStats.Money + "$";

    public void LoadItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            int randomItem = Random.Range(0, shopItemSO.Length);

            shopItems[i].shopItemSO = shopItemSO[randomItem];
        }
    }

}
