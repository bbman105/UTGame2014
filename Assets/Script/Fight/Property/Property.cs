using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class Status : MonoBehaviour
{
    private string monsterName;
    public string MonsterName
    {
        get { return monsterName; }
        set { monsterName = value; }
    }

    private int monsterID;
    public int MonsterID
    {
        get { return monsterID; }
        set { monsterID = value; }
    }
    public int TmpMonsterID { get; set; }//用來在場上識別的獨佔ID
    private int aiID;
    public int AiID
    {
        get { return aiID; }
        set { aiID = value; }
    }
    public AI Ai;
    public int LootID { get; set; }//戰利品ID
    public float DropPercent { get; set; }//戰利品掉落機率
    //體型
    private float size;
    public float Size
    {
        get { return size; }
        set
        {
            if (size != value)
            {
                size = value;
                SizeReduceSpeed = sizeReduceSpeedCalculator.GetReduceSize(Size);//當Size改變時更新因Size而影響的SizeReduceSpeed值
            }
        }
    }
    private float sizeReduceSpeed;
    public float SizeReduceSpeed
    {
        get { return sizeReduceSpeed; }
        set
        {
            if (sizeReduceSpeed != value)
            {
                sizeReduceSpeed = value;
                Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
            }
        }
    }
    //怪物屬性
    private int mainElement;
    public int MainElement
    {
        get { return mainElement; }
        set { mainElement = value; }
    }
    //怪物品質
    private int rare;
    public int Rare
    {
        get { return rare; }
        set { rare = value; }
    }
    //ExtendProperty
    private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }
    private float curHealth;
    public float CurHealth
    {
        get { return curHealth; }
        private set
        {
            if (value < 0)
            {
                curHealth = 0;
            }
            else if (value > MaxHealth)
            {
                curHealth = MaxHealth;
            }
            else { curHealth = value; }
        }
    }
    private float loseHealth;
    public float LoseHealth
    {
        get { return loseHealth; }
        set { loseHealth = value; }
    }

    private float damageTimes;
    public float DamageTimes
    {
        get { return damageTimes; }
        set { damageTimes = value; }
    }
    private int damagePlus;
    public int DamagePlus
    {
        get { return damagePlus; }
        set { damagePlus = value; }
    }
    private float defenseTimes;
    public float DefenseTimes
    {
        get { return defenseTimes; }
        set { defenseTimes = value; }
    }
    private int defensePlus;
    public int DefensePlus
    {
        get { return defensePlus; }
        set { defensePlus = value; }
    }
    private float cureTimes;
    public float CureTimes//受到治癒效果倍率
    {
        get { return cureTimes; }
        set { cureTimes = value; }
    }

    //殘暴
    private int brutal;
    public int Brutal
    {
        get { return brutal; }
        set { brutal = value; }
    }
    //殘暴倍率加值
    private float brutalTimes;
    public float BrutalTimes
    {
        get { return brutalTimes; }
        set { brutalTimes = value; }
    }
    //殘暴加值
    private int brutalPlus;
    public int BrutalPlus
    {
        get { return brutalPlus; }
        set { brutalPlus = value; }
    }
    //侵略
    private int aggressive;
    public int Aggressive
    {
        get { return aggressive; }
        set
        {
            aggressive = value;
            Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
        }
    }
    //侵略倍率加值
    private float aggressiveTimes;
    public float AggressiveTimes
    {
        get { return aggressiveTimes; }
        set
        {
            if (aggressiveTimes != value)
            {
                aggressiveTimes = value;
                Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
            }
        }
    }
    //侵略加值
    private int aggressivePlus;
    public int AggressivePlus
    {
        get { return aggressivePlus; }
        set
        {
            if (aggressivePlus != value)
            {
                aggressivePlus = value;
                Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
            }
        }
    }
    //抵抗
    private int resistance;
    public int Resistance
    {
        get { return resistance; }
        set { resistance = value; }
    }
    //抵抗倍率加值
    private float resistanceTimes;
    public float ResistanceTimes
    {
        get { return resistanceTimes; }
        set { resistanceTimes = value; }
    }
    //抵抗加值
    private int resistancePlus;
    public int ResistancePlus
    {
        get { return resistancePlus; }
        set { resistancePlus = value; }
    }
    private float penetration;
    public float Penetration
    {
        get { return penetration; }
        set { penetration = value; }
    }
    bool isCurse;
    public bool IsCurse
    {
        get { return isCurse; }
        set { isCurse = value; }
    }
    bool isBlessing;
    public bool IsBlessing
    {
        get { return isBlessing; }
        set { isBlessing = value; }
    }
    bool isStun;
    public bool IsStun
    {
        get { return isStun; }
        set
        {
            if (value != isStun)
            {
                isStun = value;
                CanSpellCheck();
            }
        }
    }
    bool isFear;
    public bool IsFear
    {
        get { return isFear; }
        set
        {
            if (value != isFear)
            {
                isFear = value;
                CanSpellCheck();
                FearCheck();
            }
        }
    }
    bool isSilence;
    public bool IsSilence
    {
        get { return isSilence; }
        set { isSilence = value; }
    }
    bool isFetter;
    public bool IsFetter
    {
        get { return isFetter; }
        set
        {
            if (value != isFetter)
            {
                isFetter = value;
                CanBeKnockCheck();
            }
        }
    }
    bool isNail;
    public bool IsNail
    {
        get { return isNail; }
        set
        {
            if (value != isNail)
            {
                isNail = value;
            }
        }
    }
    bool isRigid;
    public bool IsRigid
    {
        get { return isRigid; }
        set
        {
            if (value != isRigid)
            {
                isRigid = value;
                CanBeKnockCheck();
            }
        }
    }
    bool isInvincible;
    public bool IsInvincible
    {
        get { return isInvincible; }
        set { isInvincible = value; }
    }



    //傷害減免值
    private int reduceDamagePlus;
    public int ReduceDamagePlus
    {
        get { return reduceDamagePlus; }
        set
        {
            if (value < 0)
                reduceDamagePlus = 0;
            else
                reduceDamagePlus = value;
        }
    }
    //傷害減免率
    private float reduceDamageTimes;
    public float ReduceDamageTimes
    {
        get { return reduceDamageTimes; }
        set
        {
            if (value < 0)
                ReduceDamageTimes = 0;
            else if (value > 1)
                ReduceDamageTimes = 1;
            else
                reduceDamageTimes = value;
        }
    }



    public const float BaseSpeed = 7;
    public const float SpeedBuff = 15;
    public const float SpeedDenominator = 200;
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (speed != value)
            {
                if (value > 35)
                    speed = 35;
                else if (value < 0)
                {
                    speed = 0;
                }
                else
                {
                    speed = value;
                }
                OnSpeedChage();
            }
        }
    }
    void OnSpeedChage()
    {
        Acceleration = Speed;
        MaxMovSpd = BaseMaxSpd + MaxSpdBuff * (Speed / MaxSpdDenominator);
        swingEffect.SetSwingSpd(Speed, MaxSpdDenominator);
    }
    private float speedTimes;
    public float SpeedTimes
    {
        get { return speedTimes; }
        set
        {
            if (speedTimes != value)
            {
                speedTimes = value;
                Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
            }
        }
    }
    private float speedPlus;
    public float SpeedPlus
    {
        get { return speedPlus; }
        set
        {
            if (speedPlus != value)
            {
                speedPlus = value;
                Speed = ((BaseSpeed + (SpeedBuff * ((Aggressive * AggressiveTimes) + AggressivePlus) / SpeedDenominator)) * SpeedTimes) + SpeedPlus - SizeReduceSpeed;
            }
        }
    }
    private const byte percent = 100;

    //BaseProperty
    private int species;
    public int Species
    {
        get { return species; }
        set { species = value; }
    }
    private int speciesLevel;
    public int SpeciesLevel
    {
        get { return speciesLevel; }
        set { speciesLevel = value; }
    }
    private string speciesKey;
    public string SpeciesKey
    {
        get { return speciesKey; }
        set { speciesKey = value; }
    }

    private int lv;
    public int Lv
    {
        get { return lv; }
        set { lv = value; }
    }

    //NPC怪物用，殺死後可獲得的經驗
    private int exp;
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    //NPC怪物用，殺死後可獲得的金錢
    private int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }
    //Conditions
    private bool alive;
    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }
    public string NpcType { get; set; }
    //Skill
    public List<Spell> SpellList;
    public List<FightTalent> TalentList;
    //怪獸對應到進出場操控面板(MemberArrow)的編號
    public int MemberArrowRank { get; set; }
    //資料
    XmlDocument Doc;//資料來源
    SetMonsterAttribute MonsterAttributetype;
    public void SetMonsterAttribute(int _monsterID, SetMonsterAttribute _monsterType)//玩家怪獸
    {
        MonsterAttributetype = _monsterType;
        MonsterAttributetype.SetAttribute(_monsterID, this);
    }
}
