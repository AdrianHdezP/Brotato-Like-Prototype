using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance
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

    public Player player { get; private set; }
    public PlayerStats playerStats { get; private set; }
    public Weapon playerWeapon { get; private set; }

    private void Awake()
    {
        instance = this;

        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        playerWeapon = GetComponentInChildren<Weapon>();
    }

}
