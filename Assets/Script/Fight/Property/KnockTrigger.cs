using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public partial class Status : MonoBehaviour
{

    public List<StatusEffect> KnockPreTriggerTargetEffectList = new List<StatusEffect>();//碰撞前觸發目標狀態清單
    public List<StatusEffect> KnockPreTriggerSelfEffectList = new List<StatusEffect>();//碰撞前觸發自身狀態清單
    public List<StatusEffect> KnockReTriggerTargetEffectList = new List<StatusEffect>();//碰撞後觸發目標狀態清單
    public List<StatusEffect> KnockReTriggerSelfEffectList = new List<StatusEffect>();//碰撞後觸發自身狀態清單
    public List<Cure> KnockPreTriggerTargetCureList = new List<Cure>();//碰撞前觸發目標治癒
    public List<Cure> KnockPreTriggerSelfCureList = new List<Cure>();//碰撞前觸發自身治癒
    public List<Cure> KnockReTriggerTargetCureList = new List<Cure>();//碰撞後觸發目標治癒
    public List<Cure> KnockReTriggerSelfCureList = new List<Cure>();//碰撞後觸發自身治癒
    public List<Damage> KnockTriggerTargetDamageList = new List<Damage>();//碰撞觸發目標傷害
    public List<Damage> KnockTriggerSelfDamageList = new List<Damage>();//碰撞觸發自身傷害

    //觸發狀態效果
    public void AddReKnockTriggerTargetEffectList(StatusEffect _statusEffect)//加入碰撞後觸發目標狀態
    {
        KnockReTriggerTargetEffectList.Add(_statusEffect);
    }
    public void RemoveReKnockTriggerTargetEffectList(StatusEffect _statusEffect)//移除碰撞後觸發目標狀態
    {
        KnockReTriggerTargetEffectList.Remove(_statusEffect);
    }
    public void AddReKnockTriggerSelfEffectList(StatusEffect _statusEffect)//加入碰撞後觸發自身狀態
    {
        KnockReTriggerSelfEffectList.Add(_statusEffect);
    }
    public void RemoveReKnockTriggerSelfEffectList(StatusEffect _statusEffect)//移除碰撞後觸發自身狀態
    {
        KnockReTriggerSelfEffectList.Remove(_statusEffect);
    }
    public void AddPreKnockTriggerTargetEffectList(StatusEffect _statusEffect)//加入碰撞前觸發目標狀態
    {
        KnockPreTriggerTargetEffectList.Add(_statusEffect);
    }
    public void RemovePreKnockTriggerTargetEffectList(StatusEffect _statusEffect)//移除碰撞前觸發目標狀態
    {
        KnockPreTriggerTargetEffectList.Remove(_statusEffect);
    }
    public void AddPreKnockTriggerSelfEffectList(StatusEffect _statusEffect)//加入碰撞前觸發自身狀態
    {
        KnockPreTriggerSelfEffectList.Add(_statusEffect);
    }
    public void RemovePreKnockTriggerSelfEffectList(StatusEffect _statusEffect)//移除碰撞前觸發自身狀態
    {
        KnockPreTriggerSelfEffectList.Remove(_statusEffect);
    }

    //觸發治癒效果
    public void AddReKnockTriggerTargetCureList(Cure _cure)//加入碰撞後觸發目標治癒
    {
        KnockReTriggerTargetCureList.Add(_cure);
    }
    public void RemoveReKnockTriggerTargetCureList(Cure _cure)//移除碰撞後觸發目標治癒
    {
        KnockReTriggerTargetCureList.Remove(_cure);
    }
    public void AddReKnockTriggerSelfCureList(Cure _cure)//加入碰撞後觸發自身狀態
    {
        KnockReTriggerSelfCureList.Add(_cure);
    }
    public void RemoveReKnockTriggerSelfCureList(Cure _cure)//移除碰撞後觸發自身狀態
    {
        KnockReTriggerSelfCureList.Remove(_cure);
    }
    public void AddPreKnockTriggerTargetCureList(Cure _cure)//加入碰撞前觸發目標治癒
    {
        KnockPreTriggerTargetCureList.Add(_cure);
    }
    public void RemovePreKnockTriggerTargetCureList(Cure _cure)//移除碰撞前觸發目標治癒
    {
        KnockPreTriggerTargetCureList.Remove(_cure);
    }
    public void AddPreKnockTriggerSelfCureList(Cure _cure)//加入碰撞前觸發自身狀態
    {
        KnockPreTriggerSelfCureList.Add(_cure);
    }
    public void RemovePreKnockTriggerSelfCureList(Cure _cure)//移除碰撞前觸發自身狀態
    {
        KnockPreTriggerSelfCureList.Remove(_cure);
    }
    //觸發傷害效果
    public void AddKnockTriggerTargetDamageList(Damage _damage)//加入碰撞觸發目標傷害
    {
        KnockTriggerTargetDamageList.Add(_damage);
    }
    public void RemoveKnockTriggerTargetDamageList(Damage _damage)//移除碰撞觸發目標傷害
    {
        KnockTriggerTargetDamageList.Remove(_damage);
    }
    public void AddKnockTriggerSelfDamageList(Damage _damage)//加入碰撞後觸發自身傷害
    {
        KnockTriggerSelfDamageList.Add(_damage);
    }
    public void RemoveKnockTriggerSelfDamageList(Damage _damage)//移除碰撞後觸發自身傷害
    {
        KnockTriggerSelfDamageList.Remove(_damage);
    }
    void StartKnockTriggerDamage()//設定撞擊觸發傷害效果
    {
        for (int i = 0; i < KnockTriggerTargetDamageList.Count; i++)
        {
            AddDamage(KnockTriggerTargetDamageList[i], this, targetStatus);
        }
        for (int i = 0; i < KnockTriggerSelfDamageList.Count; i++)
        {
            AddDamage(KnockTriggerSelfDamageList[i], this, this);
        }
    }
    void StartPreKnockTrigger()//設定撞擊前觸發效果
    {
        for (int i = 0; i < KnockPreTriggerTargetEffectList.Count; i++)
        {
            AddEffect(KnockPreTriggerTargetEffectList[i], this, targetStatus);
        }
        for (int i = 0; i < KnockPreTriggerTargetCureList.Count; i++)
        {
            AddCure(KnockPreTriggerTargetCureList[i], this, targetStatus);
        }
        for (int i = 0; i < KnockPreTriggerSelfEffectList.Count; i++)
        {
            AddEffect(KnockPreTriggerSelfEffectList[i], this, this);
        }
        for (int i = 0; i < KnockPreTriggerSelfCureList.Count; i++)
        {
            AddCure(KnockPreTriggerSelfCureList[i], this, this);
        }
    }
    void StartReKnockTrigger()//設定撞擊後觸發效果
    {
        for (int i = 0; i < KnockReTriggerTargetEffectList.Count; i++)
        {
            AddEffect(KnockReTriggerTargetEffectList[i], this, targetStatus);
        }
        for (int i = 0; i < KnockReTriggerTargetCureList.Count; i++)
        {
            AddCure(KnockReTriggerTargetCureList[i], this, targetStatus);
        }
        for (int i = 0; i < KnockReTriggerSelfEffectList.Count; i++)
        {
            AddEffect(KnockReTriggerSelfEffectList[i], this, this);
        }
        for (int i = 0; i < KnockReTriggerSelfCureList.Count; i++)
        {
            AddCure(KnockReTriggerSelfCureList[i], this, this);
        }
    }
}
