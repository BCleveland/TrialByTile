using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField] private Transform m_MeterVisual = null;
    [SerializeField] private bool m_HideOnFull = false;
    public void UpdateMeter(float percent)
    {
        m_MeterVisual.gameObject.SetActive(true);
        m_MeterVisual.localScale = new Vector3(percent, 0.15f, 1);
        m_MeterVisual.localPosition = new Vector3(-0.5f + percent/2f, 0.55f, 0);
        if (m_HideOnFull && percent >= 1.0)
        {
            m_MeterVisual.gameObject.SetActive(false);
        }
    }
}
