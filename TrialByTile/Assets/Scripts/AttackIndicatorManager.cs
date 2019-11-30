using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicatorManager : MonoBehaviour
{
    public static AttackIndicatorManager i = null;
    [SerializeField] private int m_StartingAttackIndicators = 8;
    [SerializeField] public Sprite[] Frames = null;

    private List<AttackIndicator> m_AttackIndicators = null;
    private void Awake() 
    {
        i = this;

        m_AttackIndicators = new List<AttackIndicator>();
        GameObject indicatorPrefab = Resources.Load("Prefabs/AttackIndicator") as GameObject;
        GameObject canvas = GameObject.Find("Canvas");
        for(int i = 0; i < m_StartingAttackIndicators; i++)
        {
            m_AttackIndicators.Add(
                Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity, canvas.transform)
                .GetComponent<AttackIndicator>());
        }
    }
    public void ResetAttackIndicators()
    {
        foreach(AttackIndicator a in m_AttackIndicators)
        {
            a.AttatchedEntity = null;
            a.gameObject.SetActive(false);
        }
    }
    public void SetupAttackIndicator(List<Attack> attackList, Entity entityStanding)
    {
        foreach(AttackIndicator a in m_AttackIndicators)
        {
            if(a.AttatchedEntity == null)
            {
                a.Setup(entityStanding, attackList);
                return;
            }
        }
        Debug.LogWarning("Attack indicator array out of bounds! Expanding...");
        GameObject indicatorPrefab = Resources.Load("Prefabs/AttackIndicator") as GameObject;
        m_AttackIndicators.Add(
                Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<AttackIndicator>());
        m_AttackIndicators[m_AttackIndicators.Count-1].Setup(entityStanding, attackList);
    }
}
