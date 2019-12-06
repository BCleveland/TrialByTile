using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    void Start()
    {
        GridPosition = Vector2Int.RoundToInt(transform.position);
        World.i.PlaceEntity(this, GridPosition);
    }
    void TakeTurn()
    {
        //if is next to player, attack it
        //else, get best move direction and move if can otherwise stuck
    }
    void Update()
    {
        if(!m_isEnduranceFull)
        {
            m_EnduranceTime += Time.deltaTime;
            if(m_EnduranceTime > m_EnduranceTotal)
            {
                m_EnduranceTime = m_EnduranceTotal;
                m_isEnduranceFull = true;
                m_EnduranceMeter.UpdateMeter(1.0f);
                TakeTurn();
            }
            else
            {
                float percent = m_EnduranceTime/m_EnduranceTotal;
                m_EnduranceMeter.UpdateMeter(percent);
            }
        }
    }
    private void UseEndurance(float duration)
    {
        m_EnduranceTime = 0.0f;
        m_EnduranceTotal = duration;
        m_EnduranceMeter.UpdateMeter(0.0f);
        m_isEnduranceFull = false;
    }
}
