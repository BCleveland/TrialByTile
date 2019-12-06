using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat 
{
    STR, DEX, INT
}

public class PlayerController : Entity
{
    public static PlayerController i;

    [SerializeField] private Attack[] m_Attacks = null;
    [SerializeField] private GameObject[] m_MoveIndicators = null;
    [Header("Endurance")]
    [SerializeField] private float m_EnduranceMinTime = 0.4f;
    [SerializeField] private float m_EnduranceMaxTime = 1.2f;
    private Vector2Int[] m_Dirs = new Vector2Int[4]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    private Dictionary<Stat, int> m_Stats;


    //Attack indicator Menu
    private Vector2Int m_TargetedTile;
    private bool m_IsInIndicatorSelect = false;
    private List<Attack> m_ValidSelectedAttacks;

    private void Awake() 
    {
        i = this;
        m_Stats = new Dictionary<Stat, int>();
        m_Stats.Add(Stat.STR, 1);
        m_Stats.Add(Stat.DEX, 5);
        m_Stats.Add(Stat.INT, 1);

        for(int j = 0; j < m_Attacks.Length; j++)
        {
            m_Attacks[j] = ScriptableObject.Instantiate(m_Attacks[j]);
            m_Attacks[j].HotbarLocation = j;
        }
    }
    private void Start() 
    {
        World.i.PlaceEntity(this, Vector2Int.RoundToInt(transform.position));    
        UpdateValidMoves();

        AbilityIndicators.i.UpdateAbilities(new List<Attack>(m_Attacks));
    }
    private void Update() 
    {
        UpdateEndurance();
    }
    private void UpdateEndurance()
    {
        if(!m_isEnduranceFull)
        {
            m_EnduranceTime += Time.deltaTime;
            if(m_EnduranceTime > m_EnduranceTotal)
            {
                m_EnduranceTime = m_EnduranceTotal;
                m_isEnduranceFull = true;
                m_EnduranceMeter.UpdateMeter(1.0f);
                SetMoveIndicators(true);
            }
            else
            {
                float percent = m_EnduranceTime/m_EnduranceTotal;
                m_EnduranceMeter.UpdateMeter(percent);
            }
        }
    }
    public void ReceiveInput(Vector2Int tileIndex)
    {
        if(m_isEnduranceFull)
        {
            Vector2Int diff = GridPosition - tileIndex;
            if(diff.sqrMagnitude == 1 && World.i.IsValidMovePosition(tileIndex))
            {
                World.i.UpdateEntityPosition(this, GridPosition, tileIndex);
                GridPosition = tileIndex;
                UpdateValidMoves();
                UseEndurance(CalculateOnStat(Stat.DEX, m_EnduranceMaxTime, m_EnduranceMinTime));
            }
            //Impossible to move and attack in the same tap
            else
            {
                List<Attack> validAttacks = new List<Attack>();
                for(int j = 0; j < m_Attacks.Length; j++)
                {
                    if(m_Attacks[j].IsValidAttack(diff))
                    {
                        validAttacks.Add(m_Attacks[j]);
                    }
                }
                if(validAttacks.Count == 1)
                {
                    UseAttack(validAttacks[0], World.i.GetEntity(tileIndex));
                }
                else if(validAttacks.Count > 1)
                {
                    //Do multi-attack menu here
                    m_ValidSelectedAttacks = validAttacks;
                    //Track the tapped tile
                    m_TargetedTile = tileIndex;
                    //Wait for release, calculate angle and UseAttack() at that index
                    m_IsInIndicatorSelect = true;
                }
            }
        }
    }
    public void RecieveUpInput(Vector2 pos)
    {
        if(!m_IsInIndicatorSelect) return;
        Vector2 from = m_TargetedTile;
        Vector2 diff = pos - from;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x) + 180f;
        if(m_ValidSelectedAttacks.Count == 2)
        {
            if(angle > 45 && angle < 225)
            {
                UseAttack(m_ValidSelectedAttacks[1], World.i.GetEntity(m_TargetedTile));
            }
            else
            {
                UseAttack(m_ValidSelectedAttacks[0], World.i.GetEntity(m_TargetedTile));
            }
        }
        else if(m_ValidSelectedAttacks.Count == 3)
        {
            if(angle < 320 && angle > 220)
            {
                UseAttack(m_ValidSelectedAttacks[1], World.i.GetEntity(m_TargetedTile));
            }
            else if(angle > 90 && angle <= 220)
            {
                UseAttack(m_ValidSelectedAttacks[2], World.i.GetEntity(m_TargetedTile));
            }
            else
            {
                UseAttack(m_ValidSelectedAttacks[0], World.i.GetEntity(m_TargetedTile));
            }
        }
        m_IsInIndicatorSelect = false;
    }
    private void UseAttack(Attack attack, Entity target)
    {
        target.TakeDamage(Mathf.RoundToInt(CalculateOnStat(Stat.STR, attack.LowDamage, attack.HighDamage)));
        UseEndurance(attack.EnduranceUsage);
        AbilityIndicators.i.UseAbility(attack.HotbarLocation, 1.5f);
    }
    private void UseEndurance(float duration)
    {
        m_EnduranceTime = 0.0f;
        m_EnduranceTotal = duration;
        m_EnduranceMeter.UpdateMeter(0.0f);
        m_isEnduranceFull = false;
        SetMoveIndicators(false);
    }
    private void SetMoveIndicators(bool active)
    {
        for(int j = 0; j < m_MoveIndicators.Length; j++)
        {
            SpriteRenderer r = m_MoveIndicators[j].GetComponent<SpriteRenderer>();
            Color color = r.color;
            color.a = active ? 1.0f : 0.5f;
            r.color = color;
        }
    }
    public void UpdateValidMoves()
    {
        for(int j = 0; j < m_MoveIndicators.Length; j++)
        {
            m_MoveIndicators[j].SetActive(World.i.IsValidMovePosition(GridPosition + m_Dirs[j]));
        }
        for(int j = 0; j < m_Attacks.Length; j++)
        {
            m_Attacks[j].AddSelfToTiles(GridPosition);
        }
        World.i.FinalizeAttacks();
    }


    public float CalculateOnStat(Stat stat, float min, float max)
    {
        float constant = 174.8348f; //  1 / (log(10/9)/8)
        int statValue = m_Stats[stat];
        return Mathf.Lerp(min, max, Mathf.Log(statValue) + (statValue-1f)/constant);
    }
}
