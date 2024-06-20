using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private WaveManager waveManager;
    private PlayerStats playerStats;

    [Header("Canvas")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI moneyTMP;
    [SerializeField] private TextMeshProUGUI roundsTMP;

    private void Start()
    {
        waveManager = WaveManager.Instance;
        playerStats = PlayerManager.Instance.playerStats;

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
    }

    private void UpdateHealthSlider() => healthSlider.value = playerStats.Health;

    private void UpdateHealthTMP() => healthTMP.text = playerStats.Health + " / " + playerStats.maxHealth;

    private void UpdateMoneyTMP() => moneyTMP.text = playerStats.Money + "$";

    private void UpdateRoundInCanvas() => roundsTMP.text = "Rounds: " + waveManager.round;

}
