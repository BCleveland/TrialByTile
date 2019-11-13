using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Meter m_HealthBar = null;

    protected int m_CurrentHealth = 10;
    private Vector2Int m_GridPosition;
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
