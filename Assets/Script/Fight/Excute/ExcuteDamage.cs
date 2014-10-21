using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExcuteDamage : BaseExcute, IViewDebugLog
{
    DamageCalculator damageCalculator = new DamageCalculator();
    bool absoluteDamage;
    protected bool AbsoluteDamage
    {
        get { return absoluteDamage; }
        set { absoluteDamage = value; }
    }
    int attack;
    protected int Attack
    {
        get { return attack; }
        set
        {
            if (value < 0)
                attack = 0;
            else
                attack = value;
        }
    }
    float spellAttackTimes;
    protected float SpellAttackTimes
    {
        get { return spellAttackTimes; }
        set
        {
            if (value < 0)
                spellAttackTimes = 0;
            else
                spellAttackTimes = value;
        }
    }
    float attackTimes;
    public float AttackTimes
    {
        get { return attackTimes; }
        set
        {
            if (value < 0)
                attackTimes = 0;
            else
                attackTimes = value;
        }
    }
    int attackPlus;
    protected int AttackPlus
    {
        get { return attackPlus; }
        set { attackPlus = value; }
    }

    int selfCurHealthPuls;
    public int SelfCurHealthPlus
    {
        get { return selfCurHealthPuls; }
        set { selfCurHealthPuls = value; }
    }
    int selfMaxHealthPlus;
    public int SelfMaxHealthPlus
    {
        get { return selfMaxHealthPlus; }
        set { selfMaxHealthPlus = value; }
    }
    int selfLoseHealthPlus;
    public int SelfLoseHealthPlus
    {
        get { return selfLoseHealthPlus; }
        set { selfLoseHealthPlus = value; }
    }
    int targetCurHealthPuls;
    public int TargetCurHealthPlus
    {
        get { return targetCurHealthPuls; }
        set { targetCurHealthPuls = value; }
    }
    int targetMaxHealthPlus;
    public int TargetMaxHealthPlus
    {
        get { return targetMaxHealthPlus; }
        set { targetMaxHealthPlus = value; }
    }
    int targetLoseHealthPlus;
    public int TargetLoseHealthPlus
    {
        get { return targetLoseHealthPlus; }
        set { targetLoseHealthPlus = value; }
    }
    float penetration;
    protected float Penetration
    {
        get { return penetration; }
        set
        {
            if (value < 0)
                penetration = 0;
            else if (value > 1)
                penetration = 1;
            else
                penetration = value;
        }
    }
    int defense;
    protected int Defense
    {
        get { return defense; }
        set
        {
            if (value < 0)
                defense = 0;
            else
                defense = value;
        }
    }
    float defenseTimes;
    protected float DefenseTimes
    {
        get { return defenseTimes; }
        set
        {
            if (value < 0)
                defenseTimes = 0;
            else
                defenseTimes = value;
        }
    }
    int defensePlus;
    protected int DefensePlus
    {
        get { return defensePlus; }
        set { defensePlus = value; }
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
    int changeValue;
    public int ChangeValue
    {
        get { return changeValue; }
        set
        {
            if (ChangeValue < 0)
                changeValue = 0;
            else
                changeValue = value;
        }
    }
    int ElementDamagePlus { get; set; }//元素爆擊加成
    float ElementDamageTimes { get; set; }//元素爆擊加成
    Status Attacker { get; set; }
    Status Receiver { get; set; }
    Damage EDamage { get; set; }
    FightNumberSpawner numberSpawner = new FightNumberSpawner();
    public bool IsWork { get; set; }//為true才會觸發效果
    int ProbabilityRnd;//觸發機率骰數
    void ResetValue()
    {
        AbsoluteDamage = false;
        Attack = 0;
        AttackTimes = 1;
        SpellAttackTimes = 0;
        AttackPlus = 0;
        Penetration = 0;
        Defense = 0;
        DefenseTimes = 1;
        DefensePlus = 0;
        ReduceDamageTimes = 0;
        ReduceDamagePlus = 0;
        ElementDamagePlus = 0;
        ElementDamageTimes = 1;
    }

    public void SetKnockWallDamage(Status _receiver, Damage _damage)
    {
        ProbabilityRnd = UnityEngine.Random.Range(1, 101);
        if (ProbabilityRnd <= _damage.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
        {
            ResetValue();//重製數值
            Receiver = _receiver;
            Attacker = _receiver;
            EDamage = _damage;
            AbsoluteDamage = EDamage.AbsoluteDamage;
            HealthChangePlus();
            AttackPlus = (int)((float)AttackPlus * Receiver.BeKnockWallResultDMGTimes) + Receiver.BeKnockWallResultDMGPlus;
            IsWork = true;
        }
        else
        {
            IsWork = false;
        }

    }
    public void SetDamage(Status _attacker, Status _receiver, Damage _damage)//設定傷害
    {
        ProbabilityRnd = UnityEngine.Random.Range(1, 101);
        if (ProbabilityRnd <= _damage.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
        {
            ResetValue();//重製數值
            Attacker = _attacker;
            Receiver = _receiver;
            EDamage = _damage;
            //讀取傷害計算器所需的各項數值
            AbsoluteDamage = EDamage.AbsoluteDamage;
            Attack = (int)(Attacker.Brutal * Attacker.BrutalTimes) + Attacker.BrutalPlus;
            HealthChangePlus();

            SpellAttackTimes = EDamage.DamageTimes;
            AttackTimes = Attacker.DamageTimes;
            AttackPlus += (Attacker.DamagePlus + EDamage.DamagePlus);
            Penetration = EDamage.Penetration;
            Defense = (int)(Receiver.Resistance * Receiver.ResistanceTimes) + Receiver.ResistancePlus;
            DefenseTimes = Receiver.DefenseTimes;
            DefensePlus = Receiver.DefensePlus;
            ReduceDamagePlus = Receiver.ReduceDamagePlus;
            ReduceDamageTimes = Receiver.ReduceDamageTimes;

            if (_attacker.OnElementHit)
            {
                ElementDamagePlus = _attacker.CriticalDMGPlus + _receiver.BeCriticalDMGPlus;
                ElementDamageTimes = (1.3f + _attacker.CriticalDMGTimes + _receiver.BeCriticalDMGTimes);
            }
            IsWork = true;
            //ViewDebugLog();//查看DebugLog
        }
        else
        {
            IsWork = false;
        }
    }
    private void HealthChangePlus()
    {
        if (EDamage.SelfCurHealthTimes != 0)
        {
            SelfCurHealthPlus = (int)((float)Attacker.CurHealth * EDamage.SelfCurHealthTimes);
            AttackPlus += SelfCurHealthPlus;
        }

        if (EDamage.SelfMaxHealthTimes != 0)
        {
            SelfMaxHealthPlus = (int)((float)Attacker.MaxHealth * EDamage.SelfMaxHealthTimes);
            AttackPlus += SelfMaxHealthPlus;
        }

        if (EDamage.SelfLoseHealthTimes != 0)
        {
            SelfLoseHealthPlus = (int)((float)(Attacker.LoseHealth) * EDamage.SelfLoseHealthTimes);
            AttackPlus += SelfLoseHealthPlus;
        }



        if (EDamage.TargetCurHealthTimes != 0)
        {
            TargetCurHealthPlus = (int)((float)Receiver.CurHealth * EDamage.TargetCurHealthTimes);
            AttackPlus += TargetCurHealthPlus;
        }

        if (EDamage.TargetMaxHealthTimes != 0)
        {
            TargetMaxHealthPlus = (int)((float)Receiver.MaxHealth * EDamage.TargetMaxHealthTimes);
            AttackPlus += TargetMaxHealthPlus;
        }

        if (EDamage.TargetLoseHealthTimes != 0)
        {
            TargetLoseHealthPlus = (int)((float)(Receiver.LoseHealth) * EDamage.TargetLoseHealthTimes);
            AttackPlus += TargetLoseHealthPlus;
        }
    }
    public override void Excute(int _numberAni)
    {
        if (IsWork)
        {
            //計算傷害並執行傷害
            ChangeValue = damageCalculator.GetDamage(AbsoluteDamage,
                Attack, AttackTimes, SpellAttackTimes, AttackPlus, Penetration, ElementDamagePlus, ElementDamageTimes
                , Defense, DefenseTimes, DefensePlus, ReduceDamagePlus, ReduceDamageTimes);
            if (EDamage.TargetIsSelf)
            {
                if (Attacker.Alive)
                {
                    Attacker.OnHealthChange(-ChangeValue, 0);
                    if (Attacker.RetreatCondition != 2)//如果不是已經完全撤退，才顯示跳數字
                    {
                        numberSpawner.SpawnNumber(Attacker, _numberAni, ChangeValue);//設定顯示數字及動畫
                    }
                }
            }
            else
            {
                if (Receiver.Alive)
                {
                    Receiver.OnHealthChange(-ChangeValue, 0);
                    if (Receiver.RetreatCondition != 2)//如果不是已經完全撤退，才顯示跳數字
                    {
                        numberSpawner.SpawnNumber(Receiver, _numberAni, ChangeValue);//設定顯示數字及動畫
                    }
                }
            }
        }
    }
    public void ExcuteKnockWallDamage(int _numberAni)
    {
        if (Attacker.Alive)
        {
            ChangeValue = damageCalculator.GetKnockWallDamage(AttackPlus);
            Attacker.OnHealthChange(-ChangeValue, 0);
            numberSpawner.SpawnNumber(Attacker, _numberAni, ChangeValue);//設定顯示數字及動畫
        }
    }
    public void ViewDebugLog()
    {
        Debug.Log("//////////////" + Attacker.MonsterName + "//////////////");
        Debug.Log("Brutal=" + Attacker.Brutal);
        Debug.Log("BrutalTimes=" + Attacker.BrutalTimes);
        Debug.Log("BrutalPlus=" + Attacker.BrutalPlus);
        Debug.Log("AbsoluteDamage=" + AbsoluteDamage);
        Debug.Log("Attack=" + Attack);
        Debug.Log("AttackTimes=" + SpellAttackTimes);
        Debug.Log("AttackPlus=" + AttackPlus);
        Debug.Log("Penetration=" + Penetration);
        Debug.Log("Defense=" + Defense);
        Debug.Log("DefenseTimes=" + DefenseTimes);
        Debug.Log("DefensePlus=" + DefensePlus);
    }
}
