using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class NpcAI : AI
{

    public NpcAI(int _aiID, Status _status)
        : base(_aiID,_status)
    {
        SpellAIList = new List<SpellAI>();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(Resources.Load<TextAsset>("String/AI").text);
        XmlNode AINode = doc.SelectSingleNode(string.Format("root/AIID{0}", AiID));
        for (int i = 0; i < 10; i++)//一個怪物AI最多10組施法AI
        {
            int spellAI = int.Parse(AINode.SelectSingleNode(string.Format("Spell{0}ID", i + 1)).InnerText);
            if (spellAI != 0)
                SpellAIList.Add(new SpellAI(spellAI, status));
        }
    }
    public void CheckAISpell(Status _targetStatus)
    {
        //檢查目標NPC通過施法條件檢定的技能
        for (int i = 0; i < SpellAIList.Count; i++)
        {
            for (int j = 0; j < SpellAIList[i].SpellAICaseList.Count; j++)
            {
                SpellAIList[i].SpellAICaseList[j].CaseCheck(status, _targetStatus);

            }
            if (SpellAIList[i].CasePassList.ContainsValue(false))//如果施法中有一項Case條件沒通過，此施法設定為不激活，若全部通過，則設定為激活
            {
                SpellAIList[i].Active = false;
            }
            else
            {
                SpellAIList[i].Active = true;
            }                
        }
    }

}
