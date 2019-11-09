using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Slider m_StaminaSlider = null;
    [SerializeField] private GameObject[] m_MoveIndicators = null;
    private Vector2Int[] m_Dirs = new Vector2Int[4]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };
    private Vector2Int m_Position;

    private void Awake() 
    {
        UpdateValidMoves();
    }

    public void ReceiveInput(Vector2Int tileIndex)
    {
        Vector2Int diff = m_Position - tileIndex;
        if(diff.sqrMagnitude == 1)
        {
            if(World.i.IsValidMovePosition(tileIndex))
            {
                m_Position = tileIndex;
                transform.position = (Vector3Int)m_Position;
                UpdateValidMoves();
            }
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
