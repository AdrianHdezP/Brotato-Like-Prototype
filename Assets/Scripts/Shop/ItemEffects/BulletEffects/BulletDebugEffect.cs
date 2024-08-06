using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDebugEffect : MonoBehaviour
{
    Bullet bullet;

    private void Awake()
    {
        bullet = GetComponent<Bullet>();    
    }

    private void Update()
    {
        if (bullet.impacted) { DebugBulletEffect(bullet.lastImpactPos, bullet.lastHitName); }
    }

    void DebugBulletEffect(Vector3 position, string name)
    {
        Debug.Log("bullet impacted in: " + position + " And hit: " + name);
    }
}
