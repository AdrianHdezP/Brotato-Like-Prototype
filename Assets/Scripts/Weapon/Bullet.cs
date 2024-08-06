using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStats playerStats;

    public int impacts = 1;
    private float defaultScale = 0.15f;
    private float scale;
    private float target= 0.225f;

    public bool impacted {  get; private set; } 
    public Vector3 lastImpactPos {  get; private set; } 
    public string lastHitName {  get; private set; } 

    private void Start()
    {
        playerStats = PlayerManager.Instance.playerStats;

        scale = defaultScale;
    }

    private void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);

        ScaleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(impacts > 0)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                enemy.RecieveDamage();
            }

            if (collision != null)
            {
                impacted = true;
                lastImpactPos = collision.transform.position;
                lastHitName = collision.gameObject.name;

                if (collision.gameObject.layer == 3) impacts = 0;
                else impacts --;
            }
        }       
    }

    private void LateUpdate()
    {
        if (impacted )
        {
            impacted = false;
            if (impacts <= 0) Destroy(gameObject);
        }
    }


    #region Scale Movement

    private void ScaleMovement()
    {
        if (scale == defaultScale)
            StartCoroutine(LerpScale(defaultScale, target, 0.5f));
        else if (scale == target)
            StartCoroutine(LerpScale(target, defaultScale, 0.5f));
    }
    private IEnumerator LerpScale(float start, float target, float lerpDuration)
    {
        float timeElpased = 0;

        while (timeElpased < lerpDuration)
        {
            scale = Mathf.Lerp(start, target, timeElpased / lerpDuration);
            timeElpased += Time.deltaTime;
            yield return null;
        }

        scale = target;
    }

    #endregion

}
