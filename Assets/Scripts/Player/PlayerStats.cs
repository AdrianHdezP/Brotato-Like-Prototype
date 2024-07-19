using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int maxHealth; // Vida maxima - 1
    public float lifeRecovery; // Porcentaje de recuperacion de vida en la runa - 0
    [Range(10, 75)] public float harvesting; // Distancia de recoleccion - 1
    [Range(0, 100)] public float luck; // Porcentaje de conseguir mas monedas al matar enemigos - 1

    [Header("Movement Config")]
    [Range(50, 100)] public float speed; // Velocidad - 1

    [Header("Defense Config")]
    [Range(0, 50)] public float evasion; // Porcentaje de esquivar un ataque - 1
    [Range(0, 75)] public float armor; // Porcentaje de reduccion de da�o - 1

    [Header("Damage Config")]
    public int damage; // Cantidad de da�o - 1
    public float percentageOfCriticalDamage; // Porcentaje de efectuar da�o critico - 0
    public int criticalDamage; // Cantidad de da�o critico  - 0
    public int magicDamage; // Cantidad de da�o con magias  - 0
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

    public void ModifyMaxHealth(int maxHealth)
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

    public void ModifyCriticalDamage(int criticalDamage) => this.criticalDamage += criticalDamage;

    public void ModifyMagicDamage(int magicDamage) => this.magicDamage += magicDamage;

    public void ModifyMagicRecovery(float magicRecovery) => this.magicRecovery += magicRecovery;

    #endregion

}
