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
        AimToMousePosition();
        Shoot();
        Flip();
    }

    private void AimToMousePosition()
    {
        float angle = Mathf.Atan2(playerManager.player.lookDirection.y, playerManager.player.lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Flip()
    {
        SpriteRenderer muzzleSR = muzzle.GetComponent<SpriteRenderer>();

        Vector3 defaultMuzzlePosition = muzzle.transform.localPosition;
        Vector3 defaultBulletSpawnPos = bulletSpawnPos.localPosition;

        if (playerManager.player.isFacingRight)
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