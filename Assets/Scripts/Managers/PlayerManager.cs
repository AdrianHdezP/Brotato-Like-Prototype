using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public PlayerInput playerInput {  get; private set; }
    public Player player { get; private set; }
    public PlayerStats playerStats { get; private set; }
    public Weapon playerWeapon { get; private set; }

    private void Awake()
    {
        instance = this;

        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        playerWeapon = GetComponentInChildren<Weapon>();
    }

    #region Money Methods

    public bool HasMoney(int itemCost)
    {
        if (playerStats.Money < itemCost)
        {
            FindObjectOfType<HUDManager>().SetupWarnings(0);
            return false;
        }
        else
            return true;
    }

    public void SubstractMoney(int amount) => playerStats.Money -= amount;

    #endregion

    #region Active Controller

    public bool IsKeyboardAndMouseActive()
    {
        if (playerInput.currentControlScheme == "Keyboard")
            return true;

        return false;
    }

    public bool IsGamepadActive()
    {
        if (playerInput.currentControlScheme == "Gamepad")
            return true;

        return false;   
    }

    #endregion


    public void RecieveDamage(int damage)
    {
        float finalDamage = damage;

        float randomEvasion = Random.Range(0f, 1f);

        if (randomEvasion <= playerStats.evasion)
            return;

        finalDamage = finalDamage * (1 - playerStats.armor);

        playerStats.Health -= (int)(finalDamage);
        player.SetInvencibility();
    }

}
