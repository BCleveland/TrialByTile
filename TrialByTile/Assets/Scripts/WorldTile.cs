using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public Vector2Int GridPosition { get; set; }
    public Entity EntityStanding { get; set; }    
    
    private List<Attack> m_AccumulatedAttacks = new List<Attack>();
    public void AccumulateAttackIndicators(Attack attack)
    {
        m_AccumulatedAttacks.Add(attack);
    }
    public void ShowAttackIndicators()
    {
        if(EntityStanding != null && m_AccumulatedAttacks.Count > 0)
        {
            AttackIndicatorManager.i.SetupAttackIndicator(m_AccumulatedAttacks, EntityStanding);
        }
        m_AccumulatedAttacks.Clear();
    }
}
