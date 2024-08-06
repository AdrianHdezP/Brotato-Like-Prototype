using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDebugManager : MonoBehaviour
{
    private static DamageDebugManager instance;
    public static DamageDebugManager Instance
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

    [SerializeField] DamageTextAnimator textMeshPrefab;
    [SerializeField] Gradient damageColorGradient;
    [SerializeField] float maxDamageGradient;
    Canvas canvas;
    private void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
    }
    public void InstatiateDamageVisual(Vector3 position, float amount)
    {
        DamageTextAnimator instance = Instantiate(textMeshPrefab, canvas.transform);
        TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

        instance.transform.position = Camera.main.WorldToScreenPoint(position);
        text.text = amount.ToString("0");
        //instance.endColor = damageColorGradient.Evaluate(0.5f);
        instance.endColor = damageColorGradient.Evaluate(amount / maxDamageGradient);
    }

}
