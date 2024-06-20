using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerStatsSO playerStatsSO;

    #region Developer Cheats

    [HideInInspector] public bool INVENCIBILITY_CHEAT;
    [HideInInspector] public bool INFINITE_MONEY;

    #endregion

    #region Variables

    [Header("General Config")]
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int damage;

    [Header("Movement Config")]
    [HideInInspector] public float speed;

    [Header("Damage Config")]
    [HideInInspector] public float fireRate;

    #endregion

    #region HideInInspector Variables

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isInvencible = false;

    #endregion

    #region Properties

    #region Money

    private int money = 0;
    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            if (playerStatsSO.INFINITE_MONEY)
            {
                money = 9999;
                return;
            }

            money = value;
        }
    }

    #endregion

    #region Health

    private int health = 0;
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    #endregion

    #endregion

    private void Start()
    {

        #region Setup Stats

        maxHealth = playerStatsSO.maxHealth;
        damage = playerStatsSO.damage;
        speed = playerStatsSO.speed;
        fireRate = playerStatsSO.fireRate;

        #endregion

        #region Setup Variables

        Money = 0;
        Health = maxHealth;

        #endregion

    }

}
