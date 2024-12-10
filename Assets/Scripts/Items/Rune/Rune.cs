using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
     public float runeScale;
     public float runeMaxTimer;
     public float runeMinTimer;

    private void Start()
    {
        transform.localScale = new Vector3(runeScale, runeScale, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            RuneEffect();
        }
    }

    public virtual void RuneEffect() { }
}
