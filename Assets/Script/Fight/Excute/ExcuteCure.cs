using UnityEngine;
using System.Collections;

public class ExcuteCure : BaseExcute
{

    CureCalculator cureCalculator = new CureCalculator();
    bool preATK;
    protected bool PreATK
    {
        get { return preATK; }
        set { preATK = value; }
    }
    int baseCure;
    protected int BaseCure
    {
        get { return baseCure; }
        set { baseCure = value; }
    }
    int curePlus;
    public int CurePlus
    {
        get { return curePlus; }
        set { curePlus = value; }
    }
    float cureTimes;
    public float CureTimes
    {
        get { return cureTimes; }
        set { cureTimes = value; }
    }
    int damagePlus;
    public int DamagePlus
    {
        get { return damagePlus; }
        set { damagePlus = value; }
    }
    int receiveDamagePlus;
    public int ReceiveDamagePlus
    {
        get { return receiveDamagePlus; }
        set { receiveDamagePlus = value; }
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

    int changeValue;
    public int ChangeValue
    {
        get { return changeValue; }
        set
        {
            if (value < 0)
                changeValue = 0;
            else
                changeValue = value;
        }
    }
    Status Attacker { get; set; }
    Status Receiver { get; set; }
    Cure ECure { get; set; }
    FightNumberSpawner fightNumberSpawner = new FightNumberSpawner();
    RetreatNumberSpawner retreatNumberSpawner = new RetreatNumberSpawner();
    public bool IsWork { get; set; }//為true才會觸發效果
    int ProbabilityRnd;//觸發機率骰數
    void ResetValue()
    {
        BaseCure = 0;
        CurePlus = 0;
        CureTimes = 0;
    }
    private void HealthChangePlus()
    {
        CurePlus = 0;
        CurePlus += DamagePlus;
        CurePlus += ReceiveDamagePlus;

        if (ECure.SelfCurHealthTimes != 0)
        {
            SelfCurHealthPlus = (int)((float)Attacker.CurHealth * ECure.SelfCurHealthTimes);
            CurePlus += SelfCurHealthPlus;
        }

        if (ECure.SelfMaxHealthTimes != 0)
        {
            SelfMaxHealthPlus = (int)((float)Attacker.MaxHealth * ECure.SelfMaxHealthTimes);
            CurePlus += SelfMaxHealthPlus;
        }

        if (ECure.SelfLoseHealthTimes != 0)
        {
            SelfLoseHealthPlus = (int)((float)(Attacker.LoseHealth) * ECure.SelfLoseHealthTimes);
            CurePlus += SelfLoseHealthPlus;
        }



        if (ECure.TargetCurHealthTimes != 0)
        {
            TargetCurHealthPlus = (int)((float)Receiver.CurHealth * ECure.TargetCurHealthTimes);
            CurePlus += TargetCurHealthPlus;
        }

        if (ECure.TargetMaxHealthTimes != 0)
        {
            TargetMaxHealthPlus = (int)((float)Receiver.MaxHealth * (float)ECure.TargetMaxHealthTimes);
            CurePlus += TargetMaxHealthPlus;
        }

        if (ECure.TargetLoseHealthTimes != 0)
        {
            TargetLoseHealthPlus = (int)((float)(Receiver.LoseHealth) * ECure.TargetLoseHealthTimes);
            CurePlus += TargetLoseHealthPlus;
        }
    }
    public void SetCure(Status _attacker, Status _receiver, Cure _cure)//設定治癒
    {
        ProbabilityRnd = UnityEngine.Random.Range(1, 101);
        if (ProbabilityRnd <= _cure.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
        {
            ResetValue();//重製數值
            Attacker = _attacker;
            Receiver = _receiver;
            ECure = _cure;
            //讀取傷害計算器所需的各項數值
            PreATK = ECure.IsPreATK;
            if (ECure.DamageTimes != 0 && !PreATK)
            {
                DamagePlus = (int)((float)Attacker.KnockDamage * ECure.DamageTimes);
            }
            if (ECure.ReceiveDamageTimes != 0 && !PreATK)
            {
                ReceiveDamagePlus = (int)((float)Receiver.KnockDamage * ECure.ReceiveDamageTimes);
            }

            BaseCure = ECure.BaseCure;
            CureTimes = Attacker.CureTimes;
            //ViewDebugLog();//查看DebugLog
            IsWork = true;
        }
        else
        {
            IsWork = false;
        }
    }

    public override void Excute(int _numberAni)
    {
        if (IsWork)
        {
            HealthChangePlus();
            //計算治癒輛並執行治癒
            ChangeValue = cureCalculator.GetCure(BaseCure, CurePlus, CureTimes);
            //Debug.Log("回寫=" + ChangeValue);
            if (ECure.TargetIsSelf)//如果目標為自己則補自己
            {
                if (Attacker.Alive)
                {
                    Attacker.OnHealthChange(ChangeValue, 0);
                    if (Attacker.RetreatCondition != 2)//如果不是已經完全撤退，才顯示跳數字
                    {
                        fightNumberSpawner.SpawnNumber(Attacker, _numberAni, ChangeValue);//設定顯示數字及動畫
                    }
                    else
                    {
                        retreatNumberSpawner.SpawnNumber(Attacker.MemberArrowRank, ChangeValue);//設定顯示數字
                    }
                }
            }
            else//如果目標為敵人則補敵人
            {
                if (Receiver.Alive)
                {
                    Receiver.OnHealthChange(ChangeValue, 0);
                    if (Receiver.RetreatCondition != 2)//如果不是已經完全撤退，才顯示跳數字
                    {
                        fightNumberSpawner.SpawnNumber(Receiver, _numberAni, ChangeValue);//設定顯示數字及動畫
                    }
                    else
                    {
                        retreatNumberSpawner.SpawnNumber(Attacker.MemberArrowRank, ChangeValue);//設定顯示數字
                    }
                }
            }
        }
    }
}
