using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private WaveManager waveManager;

    [Header("Canvas")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI moneyTMP;
    [SerializeField] private TextMeshProUGUI roundsTMP;

    [Header("Canvas Warnings")]
    public GameObject[] warnings;
    // 0 - No money
    // 1 - Already have pistol
    // 2 - Already have shootgun
    // 3 - Already have rifle

    private float warningTimer;

    private void Start()
    {
        playerStats = PlayerManager.Instance.playerStats;
        waveManager = WaveManager.Instance;

        #region Setup Variables

        healthSlider.maxValue = playerStats.Health;

        #endregion
    }

    private void Update()
    {
        UpdateHealthSlider();
        UpdateHealthTMP();
        UpdateMoneyTMP();
        UpdateRoundInCanvas();
        ResetWarnings();
    }

    private void UpdateHealthSlider() => healthSlider.value = playerStats.Health;

    private void UpdateHealthTMP() => healthTMP.text = playerStats.Health + " / " + playerStats.maxHealth;

    private void UpdateMoneyTMP() => moneyTMP.text = playerStats.Money + "$";

    private void UpdateRoundInCanvas() => roundsTMP.text = "Rounds: " + waveManager.round;

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
