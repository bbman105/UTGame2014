using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Status : MonoBehaviour
{
    void CanSpellCheck()//可否施法檢查，改變IsStun或IsFear影響此值
    {
        if (IsStun || IsFear)
        {
            CanSpell = false;
        }
        else
        {
            CanSpell = true;
        }
    }
    void CanBeKnockCheck()//可否被撞飛，改變Fetter或Rigid影響此值
    {
        if (IsFetter || IsRigid)
        {
            CanBeKnock = false;
        }
        else
        {
            CanBeKnock = true;
        }
    }
    void FearCheck()//是否恐懼檢查，改變IsFear影響此值
    {
        if (IsFear)
        {
            SetReverseMovDirection(true);//設定怪物往後跑
            swingEffect.SetReverseMovAnimation(true);//向後跑動畫
            SpeciesModeler.SetReverseFace(true);//設定腳色面向後方
        }
        else
        {
            SetReverseMovDirection(false);//設定怪物往前跑
            swingEffect.SetReverseMovAnimation(false);//向前跑動畫
            SpeciesModeler.SetReverseFace(false);//設定腳色面向前方
        }
    }

    private List<CoolDownCounter> SpellCDCounter = new List<CoolDownCounter>();
    public void SetNpcSpellCDCounter(SpellAI _spellAI)
    {
        SpellCDCounter.Add(new NpcSpellCDCounter(_spellAI));//new 一個CD計時器並加入CDList清單
    }
    private void SpllCDCounterListFunction()//抓取NPC所有SpellAI的OnCDFunction
    {
        for (int i = 0; i < SpellCDCounter.Count; i++)
        {
            SpellCDCounter[i].OnCDFunction();
        }
    }
    public Dictionary<int, List<ExcuteEffect>> StatusEffectDic = new Dictionary<int, List<ExcuteEffect>>();
    List<int> StatusEffectBuffer;
    public List<int> RemoveStatusEffectList = new List<int>();
    public Dictionary<int, int> RemoveStatusEffectStackDic = new Dictionary<int, int>();//[效果類別編號][移除層數目] 例如[5][3]移除效果類別ID為5的效果3層
    public List<int> ImmuneTypeList = new List<int>();//免疫的效果類型ID字典

    Transform myTransform;
    GameObject myGameObject;
    TwinkleEffect twinkleEffect;
    SwingEffect swingEffect;
    ShockEffect shockEffect;
    ExcuteCure cureExcuter = new ExcuteCure();//治癒器
    ExcuteDamage damageExcuter = new ExcuteDamage();//傷害器
    public BlessingStatus blessingStatus;
    public CurseStatus curseStatus;
    public StunStatus stunStatus;
    public FetterStatus fetterStatus;
    public NailStatus nailStatus;
    public RigidStatus rigidStatus;
    public FearStatus fearStatus;
    // Use this for initialization
    void Awake()
    {
        myTransform = transform;
        myGameObject = gameObject;
        swingEffect = myTransform.GetComponentInChildren<SwingEffect>();
        twinkleEffect = myTransform.GetComponentInChildren<TwinkleEffect>();
        shockEffect = myTransform.GetComponentInChildren<ShockEffect>();
        blessingStatus = myTransform.GetComponent<BlessingStatus>();
        blessingStatus.BlessingStatusStartSet(this);
        curseStatus = myTransform.GetComponent<CurseStatus>();
        curseStatus.CurseStatusStartSet(this);
        stunStatus = myTransform.GetComponent<StunStatus>();
        stunStatus.StunStatusStartSet(this);
        fetterStatus = myTransform.GetComponent<FetterStatus>();
        fetterStatus.FetterStatusStartSet(this);
        nailStatus = myTransform.GetComponent<NailStatus>();
        nailStatus.NailStatusStartSet(this);
        rigidStatus = myTransform.GetComponent<RigidStatus>();
        rigidStatus.RigidStatusStartSet(this);
        fearStatus = myTransform.GetComponent<FearStatus>();
        fearStatus.FearStatusStartSet(this);
    }
    void StatusEffectFunction()
    {
        StatusEffectBuffer = new List<int>(StatusEffectDic.Keys);
        foreach (int key in StatusEffectBuffer)
        {
            for (int i = 0; i < StatusEffectDic[key].Count; i++)
            {
                StatusEffectDic[key][i].DuringTimerFunction();
            }
        }
        for (int i = 0; i < RemoveStatusEffectList.Count; i++)//移除StatusEffectDic中可刪除的效果
        {
            for (int j = 0; j < RemoveStatusEffectStackDic[RemoveStatusEffectList[i]]; j++)
            {
                try
                {
                    StatusEffectDic[RemoveStatusEffectList[i]].RemoveAt(0);
                }
                catch
                {
                    Debug.Log("要刪除的狀態清單不包含在已擁有的狀態清單:");
                    Debug.Log("要刪除此狀態時，發現目標狀態不存在此怪獸身上");
                    Debug.Log("嘗試檢查是否某些狀態再刪除前已被其他狀態抵銷或移除");
                }
            }
            try
            {
                if (StatusEffectDic[RemoveStatusEffectList[i]].Count == 0)
                {
                    StatusEffectDic.Remove(RemoveStatusEffectList[i]);
                }
            }
            catch
            {
                Debug.Log("要刪除的狀態清單不包含在已擁有的狀態清單:");
                Debug.Log("要刪除此狀態時，發現目標狀態不存在此怪獸身上");
                Debug.Log("嘗試檢查是否某些狀態再刪除前已被其他狀態抵銷或移除");
            }
            RemoveStatusEffectStackDic.Remove(RemoveStatusEffectList[i]);
            RemoveStatusEffectList.Remove(RemoveStatusEffectList[i]);
        }
    }
    public void ClearStatusEffectByType(int _effectType)//依照狀態效果類型篩選要刪除的狀態效果
    {
        StatusEffectBuffer = new List<int>(StatusEffectDic.Keys);
        foreach (int key in StatusEffectBuffer)
        {
            for (int i = 0; i < StatusEffectDic[key].Count; i++)
            {
                if (StatusEffectDic[key][i].EffectType == _effectType)
                {
                    StatusEffectDic[key][i].EndEffect();//結束此類型狀態效果
                }
            }
        }
    }
    public void ClearAllEffect()//清除除了狀態類型是8(Immortal)的所有狀態
    {
        StatusEffectBuffer = new List<int>(StatusEffectDic.Keys);
        foreach (int key in StatusEffectBuffer)
        {
            for (int i = 0; i < StatusEffectDic[key].Count; i++)
            {
                if (StatusEffectDic[key][i].EffectType != 8)//此狀態類型如果是8(Immortal)則不進行清除
                {
                    StatusEffectDic[key][i].EndEffect();//結束此類型狀態效果
                }
            }
        }
    }
    public void ClearDebuff()//清除Debuff
    {
        StatusEffectBuffer = new List<int>(StatusEffectDic.Keys);
        foreach (int key in StatusEffectBuffer)
        {
            for (int i = 0; i < StatusEffectDic[key].Count; i++)
            {
                if (StatusEffectDic[key][i].IsDebuff && StatusEffectDic[key][i].EffectType != 8)//此狀態類型如果是8(Immortal)則不進行清除
                {
                    StatusEffectDic[key][i].EndEffect();//結束此類型狀態效果
                }
            }
        }
    }
    public void ClearBuff()//清除Buff
    {
        StatusEffectBuffer = new List<int>(StatusEffectDic.Keys);
        foreach (int key in StatusEffectBuffer)
        {
            for (int i = 0; i < StatusEffectDic[key].Count; i++)
            {
                if (StatusEffectDic[key][i].IsBuff && StatusEffectDic[key][i].EffectType != 8)//此狀態類型如果是8(Immortal)則不進行清除
                {
                    StatusEffectDic[key][i].EndEffect();//結束此類型狀態效果
                }
            }
        }
    }
    public void AddEffect(StatusEffect _statusEffect, Status _self, Status _target)//給予狀態
    {
        ExcuteEffect EffectExcuter = myGameObject.AddComponent<ExcuteEffect>();
        EffectExcuter.SetEffect(_self, _target, _statusEffect);
    }
    public void AddCure(Cure _cure, Status _self, Status _target)//給予治癒
    {
        cureExcuter.SetCure(_self, _target, _cure);
        cureExcuter.Excute(21);
    }
    public void AddDamage(Damage _damage, Status _self, Status _target)//給予傷害
    {
        damageExcuter.SetDamage(_self, _target, _damage);
        damageExcuter.Excute(18);
    }
    // Update is called once per frame
    void Update()
    {
        SpllCDCounterListFunction();//NPC技能CD
        KnockExemptCDTimerFunction();
        StatusEffectFunction();
        if (dyingKnockWall)
            DyingKnockWallTimer();//死亡撞牆CD
        //if chara die ,then do DieFunction()
        if (!Alive) DieFunction();
        OnRetreat();//撤退判斷
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFunction();
    }

}
