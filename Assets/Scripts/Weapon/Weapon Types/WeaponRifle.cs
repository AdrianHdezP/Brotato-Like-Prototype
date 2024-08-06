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
            //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
            //bullet.GetComponent<Rigidbody2D>().velocity = playerManager.player.lookDirection.normalized * bulletSpeed;
            //

            playerManager.player.GenerateBullet(bulletPrefab, bulletSpawnPos.position, bulletSpeed);
            StartCoroutine(MuzzleFlash());
        }
    }
}
