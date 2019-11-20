using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] private RectTransform m_AttackIndicator = null;
    public Vector2Int GridPosition { get; set; }
    public Entity EntityStanding { get; set; }    
    
    private List<Attack> m_AccumulatedAttacks = new List<Attack>();
    private void Update() 
    {
        if(m_AttackIndicator != null) 
            m_AttackIndicator.position = Camera.main.WorldToScreenPoint(transform.position);     
    }
    public void AccumulateAttackIndicators(Attack attack)
    {
        m_AccumulatedAttacks.Add(attack);
    }
    public void ShowAttackIndicators()
    {
        if(m_AttackIndicator != null)
        m_AttackIndicator.GetComponent<UnityEngine.UI.Image>().color = 
        (m_AccumulatedAttacks.Count > 0) ? Color.red : Color.white;
        m_AccumulatedAttacks.Clear();
    }
}
