using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Developer Cheats

    [Header("Developer Cheats")]
    public bool INVENCIBILITY_CHEAT;
    public bool INFINITE_MONEY;

    #endregion

    #region Variables

    [Header("General Config")]
    public int maxHealth;

    [Header("Movement Config")]
    public float speed;

    [Header("Damage Config")]
    public int damage;
    public float fireRate;

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
            if (INFINITE_MONEY)
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

        #region Setup Properties

        Money = 0;
        Health = maxHealth;

        #endregion

    }

}
