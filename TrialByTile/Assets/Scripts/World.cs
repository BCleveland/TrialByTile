using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World i;

    private Dictionary<Vector2Int, GameObject> m_Map;
    private void Awake() 
    {
        i = this;
        m_Map = new Dictionary<Vector2Int, GameObject>();
        for(int j = 0; j < transform.childCount; j++)
        {
            Transform child = transform.GetChild(j);
            m_Map.Add(Vector2Int.RoundToInt(child.position), child.gameObject);
        }    
    }
    public bool IsValidMovePosition(Vector2Int pos)
    {
        return m_Map.ContainsKey(pos);
    }
}
