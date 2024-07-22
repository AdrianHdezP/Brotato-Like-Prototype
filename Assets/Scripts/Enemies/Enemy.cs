using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    #region Components

    private PlayerManager playerManager;
    private Player player;
    private PlayerStats playerStats;
    private Rigidbody2D rb;
    private NavMeshAgent agent2D;

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
    public float moveSpeed;
    public int dropMoney;
    public float health;
    public float speed;
    public int damage;

    private Vector3 moveDirection;
    private bool facingRight = true;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent2D = GetComponentInChildren<NavMeshAgent>();

        agent2D.updateRotation = false;
        agent2D.updateUpAxis = false;
    }

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        player = playerManager.player;
        playerStats = playerManager.playerStats;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        FlipController();
    }

    private void FixedUpdate()
    {
        AddForces();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null && !player.playerStats.isInvencible && !player.playerStats.INVENCIBILITY_CHEAT)
            PassiveAttack();
    }

    #region Movement

    private void MoveTowardsPlayer()
    {
        //Vector2 direction = player.transform.position - transform.position;
        //rb.velocity = direction.normalized * speed;

        if (player != null) 
            agent2D.destination = player.transform.position;

        agent2D.transform.localPosition = Vector3.zero;
        moveDirection = agent2D.desiredVelocity;
    }

    private void AddForces()
    {
        moveDirection = new Vector3(Mathf.Clamp(moveDirection.x, -1f, 1f), Mathf.Clamp(moveDirection.y, -1f, 1f), 0);
        rb.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime * rb.mass * 100);
    }

    #endregion

    #region Flip

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

    #endregion

    private void InstantiateMoney()
    {
        float randomLuck = Random.Range(0f, 1f);

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

    private void PassiveAttack() => playerManager.RecieveDamage(damage);

    public void RecieveDamage()
    {
        float damage = playerStats.damage;
        float randomPercentageOfCriticDamage = Random.Range(0f, 1f);

        if (randomPercentageOfCriticDamage <= playerStats.percentageOfCriticalDamage)
            damage *= playerStats.criticalDamageMult;

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

