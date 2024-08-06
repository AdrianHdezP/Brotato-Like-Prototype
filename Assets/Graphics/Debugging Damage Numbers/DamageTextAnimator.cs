using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextAnimator : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1;
    [SerializeField] float disolveSpeed = 1;
    [SerializeField] float EndHeight;

     public Color endColor;
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();       
        StartCoroutine(AnimateText());
    }
    IEnumerator AnimateText()
    {
        float t = 0;
        Color color = endColor;

        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + Vector3.up * EndHeight;

        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed;

            color.a = t;
            textMeshProUGUI.color = color;
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 1f);

            yield return null;
        }

        t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * disolveSpeed;

            color.a = t;
            textMeshProUGUI.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
