using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Components

    private Player player;
    private Rigidbody2D rb;

    #endregion

    [Header("Enemy Type Data")]
    [SerializeField] private EnemyTypeSO enemyTypeData;

    [Header("Config")]
    [SerializeField] private Transform visuals;
    [SerializeField] private GameObject deathAnimation;

    private bool facingRight = true;

    #region Inherited Variables

    private int moneyToAdd;
    private int health;
    private float speed;
    private int damage;

    #endregion

    private void Awake()
    {
        SetupEnemy();
    }

    private void SetupEnemy()
    {
        moneyToAdd = enemyTypeData.MoneyToAdd;
        health = enemyTypeData.Health;
        speed = enemyTypeData.Speed;
        damage = enemyTypeData.Damage;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
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

    private void PassiveAttack()
    {
        PlayerManager.Instance.playerStats.Health -= damage;
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
        PlayerManager.Instance.playerStats.Money += moneyToAdd;
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

