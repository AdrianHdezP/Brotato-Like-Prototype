using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        if (CanShoot())
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
            StartCoroutine(MuzzleFlash());
        }
    }
}
