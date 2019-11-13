using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField] private Transform m_MeterVisual = null;
    [SerializeField] private bool m_HideOnFull = false;
    private float m_YPos = 0.0f;
    private void Awake() 
    {
        m_YPos = transform.localPosition.y;    
    }
    public void UpdateMeter(float percent)
    {
        m_MeterVisual.gameObject.SetActive(true);
        m_MeterVisual.localScale = new Vector3(percent*0.8f, 0.15f, 1);
        m_MeterVisual.localPosition = new Vector3(-0.4f + percent*0.4f, m_YPos, 0);
        if (m_HideOnFull && percent >= 1.0)
        {
            m_MeterVisual.gameObject.SetActive(false);
        }
    }
}
