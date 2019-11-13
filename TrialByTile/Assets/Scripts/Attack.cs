using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/Attack", order = 1)]
public class Attack : ScriptableObject
{
    [SerializeField] public Vector2Int[] Ranges;
    [SerializeField] public int LowDamage;
    [SerializeField] public int HighDamage;
    [SerializeField] public float EnduranceUsage;
    public int CalculateDamage(PlayerController user)
    {
        return Random.Range(LowDamage, HighDamage);
    }
    public bool IsValidAttack(Vector2Int difference)
    {
        for(int j = 0; j < Ranges.Length; j++)
        {
            if(Ranges[j] == difference) return true;
        }
        return false;
    }
}
