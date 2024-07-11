using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Components

    [HideInInspector] public PlayerStats playerStats;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    #endregion

    #region States

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    #endregion

    #region Get Private Set Variables

    public float xInput { get; private set; }
    public float yInput { get; private set; }

    public bool isFacingRight { get; private set; } = true;

    #endregion

    #region Variables

    [SerializeField] private GameObject[] weapons;

    private float invencibilityTimer;

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        stateMachine.Initialize(idleState);

        ChangeWeapons(0);
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        InputCheck();
        FlipController();
        SetInvencibilityToDefalut();
    }

    #region Inputs

    private void InputCheck()
    {
        if (!playerStats.canMove)
        {
            SetZeroVelocity();
            return;
        }

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    #endregion

    #region Velocity

    public void SetVelocity(float x, float y) => rb.velocity = new Vector2(x, y).normalized * playerStats.speed;

    public void SetZeroVelocity() => rb.velocity = Vector2.zero;

    #endregion

    #region Flip

    private void FlipController()
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
