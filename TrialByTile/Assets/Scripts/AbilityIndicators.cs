using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIndicators : MonoBehaviour
{
    public static AbilityIndicators i;
    private void Awake() 
    {
        i = this;
    }
    public void UpdateAbilities(List<Attack> attacks)
    {
        for(int i = 0; i < attacks.Count; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = attacks[i].Sprite;
            transform.GetChild(i).GetComponent<Image>().color = attacks[i].BackgroundColor;
        }
    }
    public void UseAbility(int index, float cd)
    {
        transform.GetChild(index).GetChild(1).GetComponent<IndicatorCooldown>().SetCooldown(cd);
    }
}
