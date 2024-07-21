using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private HUDManager hudManager;

    #region Developer Cheats

    [Header("Developer Cheats")]
    public bool INVENCIBILITY_CHEAT;
    public bool INFINITE_MONEY;

    #endregion

    #region Variables

    [Header("General Config")]
    public float maxHealth; // Vida maxima - 1
    public float lifeRecovery; // Porcentaje de recuperacion de vida en la runa - 0 
    [Range(10, 75)] public float harvesting; // Distancia de recoleccion - 1
    [Range(0, 1f)] public float luck; // Porcentaje de conseguir mas monedas al matar enemigos - 0.5

    [Header("Movement Config")]
    [Range(50, 100)] public float speed; // Velocidad - 1

    [Header("Defense Config")]
    [Range(0, 0.50f)] public float evasion; // Porcentaje de esquivar un ataque - 1
    [Range(0, 0.75f)] public float armor; // Porcentaje de reduccion de daño - 1

    [Header("Damage Config")]
    public int damage; // Cantidad de daño - 1
    [Range(0, 1f)] public float percentageOfCriticalDamage; // Porcentaje de efectuar daño critico - 1
    [Range(1.25f, 3f)] public int criticalDamageMult; // Cantidad de daño critico  - 1
    public int magicDamage; // Cantidad de daño con magias  - 0
    public float magicRecovery; // Cooldown para magia (Debe de reducir todas) - 0

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

            UpdateMoneyTMP();
        }
    }

    #endregion

    #region Health

    private float health = 0;
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;

            UpdateHealthSlider();
            UpdateHealthTMP();

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
        hudManager = HUDManager.Instance;

        #region Setup Properties

        Money = 0;
        Health = maxHealth;

        #endregion

        AssignNewMaxHealthInSlider();
        UpdateHealthSlider();
        UpdateHealthTMP();
        UpdateMoneyTMP();
    }

    #region Health Methods

    private void AssignNewMaxHealthInSlider() => hudManager.healthSlider.maxValue = maxHealth;

    private void UpdateHealthSlider() => hudManager.healthSlider.value = health;

    private void UpdateHealthTMP() => hudManager.healthTMP.text = health + " / " + maxHealth;

    #endregion

    #region Money Methods

    private void UpdateMoneyTMP()
    {
        hudManager.moneyTMP.text = money + "$";
        hudManager.shopMoneyTMP.text = money + "$";
    }

    #endregion

    #region Modify Methods

    public void ModifyMaxHealth(float maxHealth)
    {
        this.maxHealth += maxHealth;
        AssignNewMaxHealthInSlider();
    }

    public void ModifyLifeRecovery(float lifeRecovery) => this.lifeRecovery += lifeRecovery;

    public void ModifyHarvesting(float harvesting) => this.harvesting += harvesting;
    
    public void ModifyLuck(float luck) => this.luck += luck;

    public void ModifySpeed(float speed) => this.speed += speed;

    public void ModifyEvasion(float evasion) => this.evasion += evasion;

    public void ModifyArmor(float armor) => this.armor += armor;

    public void ModifyDamage(int damage) => this.damage += damage;

    public void ModifyPercentageOfCriticalDamage(float percentageOfCriticalDamage) => this.percentageOfCriticalDamage += percentageOfCriticalDamage;

    public void ModifyCriticalDamage(int criticalDamage) => this.criticalDamageMult += criticalDamage;

    public void ModifyMagicDamage(int magicDamage) => this.magicDamage += magicDamage;

    public void ModifyMagicRecovery(float magicRecovery) => this.magicRecovery += magicRecovery;

    #endregion

}
