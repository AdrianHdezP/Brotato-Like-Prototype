using System.Collections;
using System.Collections.Generic;
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

    public bool isFacingRight { get; private set; } = true;

    #endregion


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

        rb.AddForce(new Vector2(moveInput.x, moveInput.y) * playerStats.speed * rb.mass);
    }

    private void DontMove() => rb.velocity = Vector2.zero;

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

}
