using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Meter m_HealthBar = null;
    [SerializeField] protected Meter m_EnduranceMeter = null;

    protected int m_CurrentHealth = 10;
    private Vector2Int m_GridPosition;
    
    //Stamina
    protected float m_EnduranceTime = 1.0f;
    protected float m_EnduranceTotal = 1.0f;
    protected bool m_isEnduranceFull = true;
    public Vector2Int GridPosition 
    {
        get
        {
            return m_GridPosition;
        } 
        set
        {
            m_GridPosition = value;
            transform.position = (Vector3Int)m_GridPosition;
        }
    }
    public void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;
        m_HealthBar.UpdateMeter(m_CurrentHealth/10f);
        if(m_CurrentHealth <= 0)
        {
            OnDie();
        }
    }
    private void OnDie()
    {
        World.i.RemoveEntity(GridPosition);
        gameObject.SetActive(false);
    }
}
