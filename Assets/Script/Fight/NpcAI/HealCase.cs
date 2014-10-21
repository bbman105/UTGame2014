using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealCase : SpellAICase
{
    Status status;
    bool isUp;
    float triggerHealthPercent;
    int triggerHealth;


    public HealCase(SpellAI _spellAI, List<string> _caseValueList, int _caseRank)
        : base(_spellAI, _caseValueList,_caseRank)
    {
        CaseValueList = _caseValueList;
        spellAI = _spellAI;
        caseRank = _caseRank;
        if (CaseValueList.Count == 3)
        {
            //判斷觸發對象血量是高於還是低於
            switch (CaseValueList[1])
            {
                case "Up":
                    isUp = true;
                    break;
                case "Down":
                    isUp = false;
                    break;
                default:
                    Debug.Log("傳入AISpell.xml資料時，無法判讀HealthCase的觸發對象血量條件是高於還是低於");
                    break;
            }
            //判斷觸發對象血量值
            try
            {
                triggerHealthPercent = float.Parse(CaseValueList[2].Replace("%", "")) / 100;
            }
            catch
            {
                Debug.Log("傳入AISpell.xml資料時，無法判讀HealthCase的觸發對象血量條件百分比");
            }            
        }
        else
        {
            Debug.Log("傳入AISpell.xml資料時，無法判讀HealthCase的觸發條件格式錯誤");
        }
    }

    public override void CaseCheck(Status _self,Status _target)//檢查施法條件是否有通過
    {
        try
        {
            //判斷觸發對象是自身或是敵人
            switch (CaseValueList[0])
            {
                case "Target":
                    status = _target;
                    break;
                case "Self":
                    status = _self;
                    break;
                default:
                    Debug.Log("傳入AISpell.xml資料時，無法判讀HealthCase的觸發目標");
                    break;
            }
        }
        catch
        {
            Debug.Log("碰撞時設定HealthCase時發生錯誤");
        }
        triggerHealth = (int)(status.MaxHealth * triggerHealthPercent);//設定目標血量百分比轉為實際值
        switch (isUp)
        {
            case true:
                if (status.CurHealth >= triggerHealth)
                {
                    StartSpell();
                }
                else
                {
                    StopSepll();
                }
                break;
            case false:
                if (status.CurHealth <= triggerHealth)
                {
                    StartSpell();
                }
                else
                {
                    StopSepll();
                }
                break;
            default:
                break;
        }
    }

    private void StartSpell()
    {
        //Debug.Log("StartSpell");
        this.spellAI.CasePassList[caseRank] = true;
    }
    private void StopSepll()
    {
        //Debug.Log("StopSpell");
        this.spellAI.CasePassList[caseRank] = false;
    }
}
