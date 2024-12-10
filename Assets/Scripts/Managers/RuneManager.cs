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
    public GameObject[] runePool; 
    [SerializeField] private float runeInstantiateTimer;

    private float timerForNextRune = 0;
    private float runeTimeoutTimer = 0;
    private float runeMaxTimer = 0;
    private float runeMinTimer = 0;
    private GameObject actualRune;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerForNextRune = 1;
    }

    private void Update()
    {
        timerForNextRune -= Time.deltaTime;
        runeTimeoutTimer -= Time.deltaTime;

        if (WaveManager.Instance.isInRound)
        {
            RuneTimeout();
            NextRune();
        }
        else
        {
            ResetRune();
        }

        Debug.Log(timerForNextRune);
    }

    #region Rune

    private void NextRune()
    {
        if (timerForNextRune <= 0 && runeTimeoutTimer <= 0)
            SetupRune();
    }

    public void SetupRune()
    {
        timerForNextRune = Random.Range(0, runeInstantiateTimer);

        InstantiateRune();

        Rune runeScript = actualRune.GetComponent<Rune>();
        runeMaxTimer = runeScript.runeMaxTimer;
        runeMinTimer = runeScript.runeMinTimer;
        runeTimeoutTimer = Random.Range(runeMinTimer, runeMaxTimer);
    }

    private void InstantiateRune() => actualRune = Instantiate(runePool[Random.Range(0, runePool.Length)], RandomPointInArea(), Quaternion.identity);

    private void RuneTimeout()
    {
        if (runeTimeoutTimer <= 0)
            Destroy(actualRune);
    }

    private void ResetRune()
    {
        runeTimeoutTimer = 0;
        Destroy(actualRune);
    }

    #endregion

    private Vector3 RandomPointInArea()
    {
        float randomX = Random.Range(areaSize.x, -areaSize.x) / 2;
        float randomY = Random.Range(areaSize.y, -areaSize.y) / 2;

        return transform.position + new Vector3(randomX, randomY, 0);
    }

    private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0));

}
