using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Type", menuName = "Enemy SO", order = 1)]
public class EnemyTypeSO : ScriptableObject
{
    [Header("Basic Config")]
    public int MoneyToAdd;
    public int Health;

    [Header("Movement Config")]
    [Range(15, 30)] public float Speed;

    [Header("Attack Config")]
    [Range(1, 5)] public int Damage;
}
