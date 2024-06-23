using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum WeaponType
{
    pistol,
    rifle,
}

public class Weapon : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Weapon Config")]
    public WeaponType weaponType;
    [SerializeField] private Sprite[] weaponSprite;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;

    private Vector3 direction;
    private float lastShootTime = 0;
    private float bulletSpeed = 100;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        CalculateDirection();
        AimToMousePosition();
        Shoot();
        Flip();
    }

    public void ChangeWeapon(WeaponType weaponToChange)
    {
        switch (weaponToChange)
        {
            case WeaponType.pistol:
                weaponType = WeaponType.pistol;
                sr.sprite = weaponSprite[0];
                break;

            case WeaponType.rifle:
                weaponType = WeaponType.rifle;
                sr.sprite = weaponSprite[2];
                break;
        }
    }

    private void CalculateDirection()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
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

    private bool CanShoot()
    {
        if (!WaveManager.Instance.isInRound)
            return false;

        if (weaponType == WeaponType.pistol)
        {
            if (Time.time > lastShootTime + 1 / PlayerManager.Instance.playerStats.fireRate)
            {
                lastShootTime = Time.time;
                return true;
            }
        }

        if (weaponType == WeaponType.rifle)
        {
            if (Time.time > lastShootTime + 1 / (PlayerManager.Instance.playerStats.fireRate + 1.5f))
            {
                lastShootTime = Time.time;
                return true;
            }
        }

        return false;

    }

    private void Shoot()
    {
        if (CanShoot())
        {
            if (weaponType == WeaponType.pistol || weaponType == WeaponType.rifle)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
                StartCoroutine(MuzzleFlash());
            }   
        }
    }

    private IEnumerator MuzzleFlash()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        muzzle.SetActive(false);
    }

}