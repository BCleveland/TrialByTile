using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    Camera m_Camera = null;
    PlayerController m_Player = null;
    void Awake()
    {
        m_Camera = Camera.main;
        m_Player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_Player.ReceiveInput(Vector2Int.RoundToInt(m_Camera.ScreenToWorldPoint(Input.mousePosition)));
        }
    }
}
