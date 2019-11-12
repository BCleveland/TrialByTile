using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpriteLooper : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Sprites = null;
    [SerializeField] private float m_SecondsPerFrame = 0.2f;
    private SpriteRenderer m_SpriteRenderer = null;
    private int m_CurrentFrame;
    private float m_Timer;
    private void Awake() 
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();    
        m_CurrentFrame = Random.Range(0, m_Sprites.Length);
        m_Timer = Random.Range(0.0f, m_SecondsPerFrame);
    }
    void Update()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer >= m_SecondsPerFrame)
        {
            m_Timer -= m_SecondsPerFrame;
            Debug.Log(m_Sprites.Length);
            m_CurrentFrame = (m_CurrentFrame + 1) % m_Sprites.Length;
            m_SpriteRenderer.sprite = m_Sprites[m_CurrentFrame];
        }
    }
}
