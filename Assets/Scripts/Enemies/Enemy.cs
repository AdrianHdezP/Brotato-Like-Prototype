using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Components

    private PlayerManager playerManager;
    private Player player;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    #endregion

    #region Variables

    [Header("Config")]
    [SerializeField] private Transform visuals;
    [SerializeField] private float forceMultiplier;
    [SerializeField] private GameObject oneDollarMoneyPrefab;
    [SerializeField] private GameObject fiveDollarMoneyPrefab;
    [SerializeField] private GameObject tenDollarMoneyPrefab;
    [SerializeField] private GameObject deathAnimationPrefab;

    [Header("Setup")]
    public int dropMoney;
    public int health;
    public float speed;
    public int damage;

    private bool facingRight = true;

    #endregion

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        player = playerManager.player;
        playerStats = playerManager.playerStats;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FlipController();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null && !player.playerStats.isInvencible && !player.playerStats.INVENCIBILITY_CHEAT)
            PassiveAttack();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * speed;
    }

    private void FlipController()
    {
        if (player.transform.position.x <= transform.position.x && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else if (player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
            facingRight = true;
        }
    }

    private void Flip() => visuals.Rotate(0, 180, 0);

    private void InstantiateMoney()
    {
        float randomLuck = Random.Range(0f, 100f);

        if (randomLuck <= playerStats.luck)
        {
            int randomExtraMoney = Random.Range(1, 6);
            dropMoney += randomExtraMoney;
        }

        while (dropMoney > 0)
        {
            GameObject money;

            if (dropMoney >= 10)
            {
                money = Instantiate(tenDollarMoneyPrefab, transform.position, Quaternion.identity);
                dropMoney -= 10;
            }
            else if (dropMoney >= 5)
            {
                money = Instantiate(fiveDollarMoneyPrefab, transform.position, Quaternion.identity);
                dropMoney -= 5;
            }
            else
            {
                money = Instantiate(oneDollarMoneyPrefab, transform.position, Quaternion.identity);
                dropMoney -= 1;
            }

            float RandomX = Random.Range(-1f, 1f);
            float RandomY = Random.Range(-1f, 1f);

            money.GetComponent<Rigidbody2D>().AddForce(new Vector2(RandomX, RandomY) * forceMultiplier, ForceMode2D.Impulse);
        }  
    }

    private void PassiveAttack()
    {
        float randomEvasion = Random.Range(0f, 100f);

        if (randomEvasion <= playerStats.evasion)
            return;

        float finalDamage = (damage / 100f) * (100f - playerStats.armor);

        PlayerManager.Instance.playerStats.Health -= (int)(finalDamage);
        player.SetInvencibility();
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        WaveManager.Instance.enemiesInRound--;
        InstantiateMoney();
        Instantiate(deathAnimationPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

