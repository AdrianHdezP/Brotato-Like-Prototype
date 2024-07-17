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

    public void SubstractMoney(int amount) => PlayerManager.Instance.playerStats.Money -= amount;

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

}
