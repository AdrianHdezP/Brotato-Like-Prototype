using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    #region Components

    public PlayerManager playerManager {  get; private set; }
    public PlayerInput playerInput { get; private set; }
    public PlayerStats playerStats { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    #endregion

    #region Variables

    [SerializeField] private GameObject[] weapons;

    private float invencibilityTimer;

    #endregion

    #region Get Private Set Variables

    public Vector2 moveInput {  get; private set; }
    public Vector2 aimInput { get; private set; }
    public Vector2 lookDirection { get; private set; }

    public bool isFacingRight { get; private set; } = true;

    #endregion


    [Header("DEBUGGING")]
    [SerializeField] string aimData;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerStats = playerManager.playerStats;
        playerInput = playerManager.playerInput;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        ChangeWeapons(0);
    }

    private void Update()
    {
        CalculateDirection();
        InputChecks();
        Animations();
        FlipController();
        SetInvencibilityToDefalut();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Inputs

    private void InputChecks()
    {
        moveInput = playerInput.actions["move"].ReadValue<Vector2>();
        aimInput = playerInput.actions["aim"].ReadValue<Vector2>();
    }

    #endregion

    #region Move

    private void Move()
    {
        if (!playerStats.canMove)
        {
            DontMove();
            return;
        }


        Vector2 moveDirection = new Vector2(moveInput.x, moveInput.y);

        if (Vector3.Dot(moveDirection.normalized, lookDirection.normalized) > 0.6f) rb.AddForce(moveDirection.normalized * playerStats.speed * rb.mass * Time.fixedDeltaTime * 100);
        else rb.AddForce(moveDirection.normalized * playerStats.speed * 0.6f * rb.mass * Time.fixedDeltaTime * 100);

        aimData = "AIM: " + lookDirection.normalized + "// MOVE: " + moveDirection.normalized;

       // rb.AddForce(new Vector2(moveInput.x, moveInput.y) * playerStats.speed * rb.mass);
    }

    private void DontMove() => rb.velocity = Vector2.zero;

    #endregion

    #region Look

    private void CalculateDirection()
    {
        if (PlayerManager.Instance.IsKeyboardAndMouseActive() && Application.isFocused)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookDirection.Normalize();
        }
        else if (PlayerManager.Instance.IsGamepadActive())
        {
            lookDirection = PlayerManager.Instance.player.aimInput.normalized;
        }
    }


    #endregion

    #region Animations

    private void Animations()
    {
        if (moveInput == Vector2.zero)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", true);
        }
    }

    #endregion

    #region Flip

    private void FlipController()
    {
        if (playerManager.IsKeyboardAndMouseActive() && Application.isFocused)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= transform.position.x && isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && !isFacingRight)
            {
                Flip();
                isFacingRight = true;
            }
        }

        if (playerManager.IsGamepadActive())
        {
            if (aimInput.x < 0 && isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
            else if (aimInput.x >= 0 && !isFacingRight)
            {
                Flip();
                isFacingRight = true;
            }
        }    
    }

    private void Flip() => transform.Rotate(0, 180, 0);

    #endregion

    #region Invencibility

    public void SetInvencibility()
    {
        playerStats.isInvencible = true;
        invencibilityTimer = 1;
    }

    private void SetInvencibilityToDefalut()
    {
        if (playerStats.isInvencible)
        {
            invencibilityTimer -= Time.deltaTime;

            if (invencibilityTimer < 0)
                playerStats.isInvencible = false;
        }
    }

    #endregion

    public void ChangeWeapons(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[index].SetActive(true);
    }

    public void GenerateBullet(GameObject proyectilePrefab, Vector3 spawnPoint, float speed)
    {
        GameObject bullet = Instantiate(proyectilePrefab, spawnPoint, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = playerManager.player.lookDirection.normalized * speed;
        EffectManager.Instance.ApplyEffectsToBullet(bullet);
    }

}
