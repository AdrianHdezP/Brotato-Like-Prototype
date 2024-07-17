using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    #region Components

    public PlayerManager playerManager {  get; private set; }
    public SpriteRenderer sr {  get; private set; }

    #endregion

    #region Variables

    [Header("Weapon Config")]
    public float fireRate;

    [Header("Weapon Visuals")]
    public GameObject muzzle;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    #endregion

    #region Get Private Set Variables

    public Vector3 direction {  get; private set; }
    public float bulletSpeed { get; private set; } = 100;

    #endregion

    #region Private Variables

    private float lastShootTime = 0;

    #endregion

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        sr = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        CalculateDirection();
        AimToMousePosition();
        Shoot();
        Flip();
    }

    private void CalculateDirection()
    {
        if (PlayerManager.Instance.IsKeyboardAndMouseActive())
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
        }
        else if (PlayerManager.Instance.IsGamepadActive())
        {
            direction = PlayerManager.Instance.player.aimInput.normalized;
        }  
    }

    private void AimToMousePosition()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Flip()
    {
        SpriteRenderer muzzleSR = muzzle.GetComponent<SpriteRenderer>();

        Vector3 defaultMuzzlePosition = muzzle.transform.localPosition;
        Vector3 defaultBulletSpawnPos = bulletSpawnPos.localPosition;

        if (PlayerManager.Instance.player.isFacingRight)
        {
            sr.flipY = false;
            muzzleSR.flipY = false;

            muzzle.transform.localPosition = new Vector3(defaultMuzzlePosition.x, 1.2f, defaultMuzzlePosition.z);
            bulletSpawnPos.localPosition = new Vector3(defaultBulletSpawnPos.x, 1.2f, defaultBulletSpawnPos.z);
        }
        else
        {
            sr.flipY = true;
            muzzleSR.flipY = true;

            muzzle.transform.localPosition = new Vector3(defaultMuzzlePosition.x, -1.2f, defaultMuzzlePosition.z);
            bulletSpawnPos.localPosition = new Vector3(defaultBulletSpawnPos.x, -1.2f, defaultBulletSpawnPos.z);
        }
    }

    public bool CanShoot()
    {
        if (!WaveManager.Instance.isInRound)
            return false;

        if (Time.time > lastShootTime + 1 / fireRate)
        {
            lastShootTime = Time.time;
            return true;
        }

        return false;

    }

    public virtual void Shoot() { }

    public IEnumerator MuzzleFlash()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        muzzle.SetActive(false);
    }

}