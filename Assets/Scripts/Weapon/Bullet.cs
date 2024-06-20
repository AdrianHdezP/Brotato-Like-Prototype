using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStats playerStats;

    private float defaultScale = 0.15f;
    private float scale;
    private float target= 0.225f;

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
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
            enemy.Damage(playerStats.damage);

        if (collision != null)
            Destroy(gameObject);
    }

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

}
