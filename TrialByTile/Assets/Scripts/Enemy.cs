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
}
