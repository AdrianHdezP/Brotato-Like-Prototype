using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEventManager : MonoBehaviour
{
    private static ItemsEventManager instance;

    public static ItemsEventManager Instance
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

    private PlayerStats playerStats;

    [Header("Canvas Warnings")]
    public GameObject noMoneyWarning;
    public GameObject alreadyPistolWarning;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerStats = PlayerManager.Instance.playerStats;
    }

    public IEnumerator WarningAdvice(GameObject warning)
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(1);
        warning.SetActive(false);
    }

    public void MaxHealth() => playerStats.maxHealth += 5;

    public void Speed() => playerStats.speed += 1;

    public void Damage() => playerStats.damage += 5;

    public void FireRate() => playerStats.fireRate += (playerStats.fireRate * 10) / 100;

}
