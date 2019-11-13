﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public static PlayerController i;

    [SerializeField] private Attack[] m_Attacks = null;
    [SerializeField] private Meter m_EnduranceMeter = null;
    [SerializeField] private GameObject[] m_MoveIndicators = null;
    private Vector2Int[] m_Dirs = new Vector2Int[4]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };


    //Stamina
    private float m_EnduranceTime = 1.0f;
    private float m_EnduranceTotal = 1.0f;
    private bool m_isEnduranceFull = true;

    private void Awake() 
    {
        i = this;
        UpdateValidMoves();
    }
    private void Start() 
    {
        World.i.PlaceEntity(this, Vector2Int.RoundToInt(transform.position));    
    }
    private void Update() 
    {
        UpdateEndurance();
    }
    private void UpdateEndurance()
    {
        if(!m_isEnduranceFull)
        {
            m_EnduranceTime += Time.deltaTime;
            if(m_EnduranceTime > m_EnduranceTotal)
            {
                m_EnduranceTime = m_EnduranceTotal;
                m_isEnduranceFull = true;
                m_EnduranceMeter.UpdateMeter(1.0f);
                SetMoveIndicators(true);
            }
            else
            {
                float percent = m_EnduranceTime/m_EnduranceTotal;
                m_EnduranceMeter.UpdateMeter(percent);
            }
        }
    }
    public void ReceiveInput(Vector2Int tileIndex)
    {
        if(m_isEnduranceFull)
        {
            Vector2Int diff = GridPosition - tileIndex;
            if(diff.sqrMagnitude == 1 && World.i.IsValidMovePosition(tileIndex))
            {
                World.i.UpdateEntityPosition(this, GridPosition, tileIndex);
                GridPosition = tileIndex;
                UpdateValidMoves();
                UseEndurance(0.75f);
            }
            //Impossible to move and attack in the same tap
            else
            {
                List<Attack> validAttacks = new List<Attack>();
                for(int j = 0; j < m_Attacks.Length; j++)
                {
                    if(m_Attacks[j].IsValidAttack(diff))
                    {
                        validAttacks.Add(m_Attacks[j]);
                    }
                }
                if(validAttacks.Count == 1)
                {
                    UseAttack(validAttacks[0], World.i.GetEntity(tileIndex));
                }
                else if(validAttacks.Count > 1)
                {
                    //Do multi-attack menu here
                }
            }
        }
    }
    private void UseAttack(Attack attack, Entity target)
    {
        target.TakeDamage(attack.CalculateDamage(this));
        UseEndurance(attack.EnduranceUsage);
    }
    private void UseEndurance(float duration)
    {
        m_EnduranceTime = 0.0f;
        m_EnduranceTotal = duration;
        m_EnduranceMeter.UpdateMeter(0.0f);
        m_isEnduranceFull = false;
        SetMoveIndicators(false);
    }
    private void SetMoveIndicators(bool active)
    {
        for(int j = 0; j < m_MoveIndicators.Length; j++)
        {
            SpriteRenderer r = m_MoveIndicators[j].GetComponent<SpriteRenderer>();
            Color color = r.color;
            color.a = active ? 1.0f : 0.5f;
            r.color = color;
        }
    }
    public void UpdateValidMoves()
    {
        for(int j = 0; j < m_MoveIndicators.Length; j++)
        {
            m_MoveIndicators[j].SetActive(World.i.IsValidMovePosition(GridPosition + m_Dirs[j]));
        }
    }
}
