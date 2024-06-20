using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player SO", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Developer Cheats")]
    public bool INVENCIBILITY_CHEAT;
    public bool INFINITE_MONEY;

    [Header("General Config")]
    public int maxHealth = 25;
    public int damage = 5;

    [Header("Movement Config")]
    [Range(10,50)] public float speed = 25;

    [Header("Damage Config")]
    public float fireRate = 1;
}
