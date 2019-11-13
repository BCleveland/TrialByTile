using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World i;

    private Dictionary<Vector2Int, WorldTile> m_Map;
    private void Awake() 
    {
        i = this;
        m_Map = new Dictionary<Vector2Int, WorldTile>();
        for(int j = 0; j < transform.childCount; j++)
        {
            WorldTile child = transform.GetChild(j).GetComponent<WorldTile>();
            child.GridPosition = Vector2Int.RoundToInt(child.transform.position);
            m_Map.Add(child.GridPosition, child);
        }    
    }
    public bool IsValidMovePosition(Vector2Int pos)
    {
        //Position is in map, and no entity is standing there
        return m_Map.ContainsKey(pos) && m_Map[pos].EntityStanding == null;
    }
    public void PlaceEntity(Entity entity, Vector2Int pos)
    {
        m_Map[pos].EntityStanding = entity;
    }
    public void RemoveEntity(Vector2Int pos)
    {
        m_Map[pos].EntityStanding = null;
        PlayerController.i.UpdateValidMoves();
    }
    public void UpdateEntityPosition(Entity entity, Vector2Int oldPos, Vector2Int newPos)
    {
        m_Map[oldPos].EntityStanding = null;
        m_Map[newPos].EntityStanding = entity;
    }
    public Entity GetEntity(Vector2Int pos)
    {
        if(m_Map.ContainsKey(pos))
        {
            return m_Map[pos].EntityStanding;
        }
        return null;
    }
}
