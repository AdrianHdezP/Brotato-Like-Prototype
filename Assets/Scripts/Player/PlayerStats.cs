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
    public float lifeRecovery; // Porcentaje de recuperacion de vida en la runa
    public float harvesting; // Radio del iman de recoleccion
    public float luck; // Porcentaje de conseguir mas monedas al matar enemigos

    [Header("Movement Config")]
    public float speed; // Velocidad

    [Header("Defense Config")]
    public float evasion; // Porcentaje de esquivar un ataque
    public float armor; // Porcentaje de reduccion de daño

    [Header("Damage Config")]
    public int damage; // Cantidad de daño 
    public float percentageOfCriticalDamage; // Porcentaje de efectuar daño critico
    public int criticalDamage; // Cantidad de daño critico 
    public int magicDamage; // Cantidad de daño con magias 
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

    #region Modify Methods

    public void ModifyMaxHealth(int maxHealth) => this.maxHealth += maxHealth;
    public void ModifyLifeRecovery(float lifeRecovery) => this.lifeRecovery += lifeRecovery;
    public void ModifyHarvesting(float harvesting) => this.harvesting += harvesting;
    public void ModifyLuck(float luck) => this.luck += luck;
    public void ModifySpeed(float speed) => this.speed += speed;
    public void ModifyEvasion(float evasion) => this.evasion += evasion;
    public void ModifyArmor(float armor) => this.armor += armor;
    public void ModifyDamage(int damage) => this.damage += damage;
    public void ModifyPercentageOfCriticalDamage(float percentageOfCriticalDamage) => this.percentageOfCriticalDamage += percentageOfCriticalDamage;
    public void ModifyCriticalDamage(int criticalDamage) => this.criticalDamage += criticalDamage;
    public void ModifyMagicDamage(int magicDamage) => this.magicDamage += magicDamage;
    public void ModifyMagicRecovery(float magicRecovery) => this.magicRecovery += magicRecovery;

    #endregion

}
