using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public partial class Status : MonoBehaviour
{
    public List<StatusEffect> BeKnockWallTriggerEffectList = new List<StatusEffect>();
    public List<Damage> BeKnockWallTriggerDamageList = new List<Damage>();
    public List<Cure> BeKnockWallTriggerCureList = new List<Cure>();

    public void AddKnockWallTriggerEffectList(StatusEffect _statusEffect)//新增被逼牆觸發狀態效果
    {
        BeKnockWallTriggerEffectList.Add(_statusEffect);
    }
    public void RemoveKnockWallTriggerEffectList(StatusEffect _statusEffect)//移除被逼牆觸發狀態效果
    {
        BeKnockWallTriggerEffectList.Remove(_statusEffect);
    }
    public void AddKnockWallTriggerDamageList(Damage _damage)//新增被逼牆觸發傷害效果
    {
        BeKnockWallTriggerDamageList.Add(_damage);
    }
    public void RemoveKnockWallTriggerDamageList(Damage _damage)//移除被逼牆觸發傷害效果
    {
        BeKnockWallTriggerDamageList.Remove(_damage);
    }
    public void AddKnockWallTriggerCureList(Cure _cure)//新增被逼牆觸發治癒效果
    {
        BeKnockWallTriggerCureList.Add(_cure);
    }
    public void RemoveKnockWallTriggerCureList(Cure _cure)//移除被逼牆觸發治癒效果
    {
        BeKnockWallTriggerCureList.Remove(_cure);
    }
    void StartKnockWallTriggerDamage()//執行被逼牆觸發傷害效果
    {
        for (int i = 0; i < BeKnockWallTriggerDamageList.Count; i++)
        {
            AddDamage(BeKnockWallTriggerDamageList[i], this, this);
        }
    }
    void StartKnockWallTriggerEffect()//執行被逼牆觸發狀態效果
    {
        for (int i = 0; i < BeKnockWallTriggerEffectList.Count; i++)
        {
            AddEffect(BeKnockWallTriggerEffectList[i], this, this);
        }
    }
    void StartKnockWallTriggerCure()//執行被逼牆觸發治癒效果
    {
        for (int i = 0; i < BeKnockWallTriggerCureList.Count; i++)
        {
            AddCure(BeKnockWallTriggerCureList[i], this, this);
        }
    }

}
