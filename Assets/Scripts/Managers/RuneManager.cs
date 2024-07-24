using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    private static RuneManager instance;

    public static RuneManager Instance
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

    public Vector2 areaSize;
    public GameObject rune;

    private float timer = 0;
    private GameObject actualRune;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer = 1;
    }

    private void Update()
    {
        timer -= Time.deltaTime;    

        if (timer <= 0)
            SetupRune();
    }

    public void SetupRune()
    {
        Destroy(actualRune);
        actualRune = Instantiate(rune, RandomPointInArea(), Quaternion.identity);
        timer = 2.5f;
    }

    private Vector3 RandomPointInArea()
    {
        float randomX = Random.Range(areaSize.x, -areaSize.x) / 2;
        float randomY = Random.Range(areaSize.y, -areaSize.y) / 2;

        return transform.position + new Vector3(randomX, randomY, 0);
    }

    private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0));

}
