using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Meter m_EnduranceMeter = null;
    [SerializeField] private GameObject[] m_MoveIndicators = null;
    private Vector2Int[] m_Dirs = new Vector2Int[4]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    private Vector2Int m_Position;

    //Stamina
    private float m_EnduranceTime = 1.0f;
    private float m_EnduranceTotal = 1.0f;
    private bool m_isEnduranceFull = true;

    private void Awake() 
    {
        UpdateValidMoves();
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
        Vector2Int diff = m_Position - tileIndex;
        if(diff.sqrMagnitude == 1)
        {
            if(World.i.IsValidMovePosition(tileIndex) && m_isEnduranceFull)
            {
                m_Position = tileIndex;
                transform.position = (Vector3Int)m_Position;
                UpdateValidMoves();
                UseEndurance(0.75f);
            }
        }
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
    private void UpdateValidMoves()
    {
        for(int j = 0; j < m_MoveIndicators.Length; j++)
        {
            m_MoveIndicators[j].SetActive(World.i.IsValidMovePosition(m_Position + m_Dirs[j]));
        }
    }
}
