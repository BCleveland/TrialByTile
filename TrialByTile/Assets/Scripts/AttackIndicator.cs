using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackIndicator : MonoBehaviour
{
    public Entity AttatchedEntity = null;
    private Image m_Image = null;
    private void Awake() {
        m_Image = transform.GetChild(3).GetComponent<Image>();
    }
    private void Update() 
    {
        if(AttatchedEntity != null) 
            transform.position = Camera.main.WorldToScreenPoint(AttatchedEntity.transform.position);     
    }
    public void Setup(Entity entity, List<Attack> attacks)
    {
        gameObject.SetActive(true);
        AttatchedEntity = entity;
        Debug.Log(attacks.Count);
        m_Image.sprite = AttackIndicatorManager.i.Frames[attacks.Count-1];
        DisableChildren();
        Transform correctIndicator = transform.GetChild(attacks.Count-1);
        correctIndicator.gameObject.SetActive(true);
        for(int i = 0; i < attacks.Count; i++)
        {
            Image mask = correctIndicator.GetChild(i).GetComponent<Image>();
            mask.color = attacks[i].BackgroundColor;
            mask.transform.GetChild(0).GetComponent<Image>().sprite = attacks[i].Sprite;
        }
    }
    private void DisableChildren()
    {
        for(int j = 0; j < 3; j++)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
