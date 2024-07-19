using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        if (CanShoot())
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = playerManager.player.lookDirection.normalized * bulletSpeed;
            StartCoroutine(MuzzleFlash());
        }
    }
}
