using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    public int money;
    public float force;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerStats = playerManager.playerStats;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = playerManager.transform.position - transform.position;

        if (Vector2.Distance(transform.position, playerManager.transform.position) < playerStats.harvesting)
            rb.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            playerStats.Money += money;
            Destroy(gameObject);
        }
    }
}
