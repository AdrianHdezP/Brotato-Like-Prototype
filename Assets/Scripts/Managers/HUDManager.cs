using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance;

    public static HUDManager Instance
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

    #region Componets

    private PlayerManager playerManager;
    private PlayerStats playerStats;
    private WaveManager waveManager;

    #endregion

    [Header("Canvas")]
    public Slider healthSlider;
    public TextMeshProUGUI healthTMP;
    public TextMeshProUGUI moneyTMP;
    public TextMeshProUGUI shopMoneyTMP;
    public TextMeshProUGUI roundsTMP;

    [Header("Canvas Warnings")]
    public GameObject[] warnings;
    // 0 - No money
    // 1 - Already have pistol
    // 2 - Already have shootgun
    // 3 - Already have rifle

    private float warningTimer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerStats = playerManager.playerStats;
        waveManager = WaveManager.Instance;
    }

    private void Update()
    {
        ResetWarnings();
    }

    #region Warnings

    public void SetupWarnings(int index)
    {
        warnings[index].SetActive(true);
        warningTimer = 1;
    }

    private void ResetWarnings()
    {
        warningTimer -= Time.deltaTime;

        if (warningTimer <= 0)
        {
            for (int i = 0; i < warnings.Length; i++)
            {
                warnings[i].SetActive(false);
            }
        }
    }

    #endregion

}
