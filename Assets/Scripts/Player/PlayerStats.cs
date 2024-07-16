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
    public int maxHealth; // Vida maxima 
    public float lifeRecovery; // ?
    public float harvesting; // Radio del iman de recoleccion
    public float luck; // Porcentaje de conseguir mas monedas al matar enemigos

    [Header("Movement Config")]
    public float speed;

    [Header("Defense Config")]
    public float evasion; // Porcentaje de esquivar un ataque
    public float armor; // Porcentaje de reduccion de da�o

    [Header("Damage Config")]
    public int damage; // Cantidad de da�o 
    public float percentageOfCriticalDamage; // Porcentaje de efectuar da�o critico
    public int criticalDamage; // Cantidad de da�o critico 
    public int magicDamage; // Cantidad de da�o con magias 
    public float magicRecovery; // Cooldown para magia (Debe de reducir todas)

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

            if (health <= 0)
            {
                // Muerte
                // Fin de partida
            }
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
