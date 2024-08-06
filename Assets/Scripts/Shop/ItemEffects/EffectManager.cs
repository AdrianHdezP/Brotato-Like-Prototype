using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    private static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("No instance");
            }

            return instance;
        }
    }


    public bool DebuggingEffect;
    public int bulletPenetrations = 1;


    private void Awake()
    {
         instance = this;
    }


    public void ApplyEffectsToBullet(GameObject proyectile)
    {
        if (DebuggingEffect) proyectile.AddComponent<BulletDebugEffect>();

        proyectile.GetComponent<Bullet>().impacts = bulletPenetrations;
    }

}
