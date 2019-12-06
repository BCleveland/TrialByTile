using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndicatorCooldown : MonoBehaviour
{
    private TextMeshProUGUI m_DEBUG_TEXT;
    private float m_Cooldown;
    private void Awake() 
    {
        m_DEBUG_TEXT = GetComponent<TextMeshProUGUI>();
    }
    public void SetCooldown(float cd)
    {
        m_Cooldown = cd;
        m_DEBUG_TEXT.gameObject.SetActive(true);
    }
    private void Update() 
    {
        m_Cooldown -= Time.deltaTime;
        m_DEBUG_TEXT.text = m_Cooldown.ToString("F1");
        if(m_Cooldown < 0)
        {
            m_DEBUG_TEXT.gameObject.SetActive(false);
        }
    }
}
