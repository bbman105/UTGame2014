using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;

public class SpellAI
{
    int spellAIID;
    public int SpellAIID
    {
        get { return spellAIID; }
        set { spellAIID = value; }
    }
    float cd;
    public float Cd
    {
        get { return cd; }
        set { cd = value; }
    }
    bool onCD;//是否在CD中
    public bool OnCD
    {
        get { return onCD; }
        set { onCD = value; }
    }
    int weight;
    public int Weight
    {
        get { return weight; }
        set { weight = value; }
    }
    int spellID;
    public int SpellID
    {
        get { return spellID; }
        set { spellID = value; }
    }
    bool active;//AICase如果全通過則active=true;代表技能處於激活狀態
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }
    private Dictionary<int, bool> casePassList;//AICase通過條件List
    public Dictionary<int, bool> CasePassList
    {
        get { return casePassList; }
        set { casePassList = value; }
    }
    public List<SpellAICase> SpellAICaseList { get; set; }
    SpellAICase spellAICase;
    public SpellAI(int _spellAIID, Status _status)
    {
        SpellAICaseList = new List<SpellAICase>();
        SpellAIID = _spellAIID;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(Resources.Load<TextAsset>("String/AISpell").text);
        XmlNode spellAINode = doc.SelectSingleNode(string.Format("root/AISpellID{0}", SpellAIID));
        Weight = int.Parse(spellAINode.SelectSingleNode("Weight").InnerText);
        Active = true;
        CasePassList = new Dictionary<int, bool>();
        Cd = float.Parse(spellAINode.SelectSingleNode("CD").InnerText);
        OnCD = false;
        _status.SetNpcSpellCDCounter(this);//將此施法AI加入施法CD清單中
        SpellID = int.Parse(spellAINode.SelectSingleNode("SpellID").InnerText);
        for (int i = 0; i < 3; i++)//最多三個施法條件(Case)限制
        {
            if (spellAINode.SelectSingleNode(string.Format("Case{0}", i + 1)).InnerText != "0")
            {
                string[] caseStr = spellAINode.SelectSingleNode(string.Format("Case{0}", i + 1)).InnerText.Split(',');
                string caseName = caseStr[0];
                List<string> caseValueList = new List<string>();
                for (int j = 1; j < caseStr.Length; j++)
                {
                    caseValueList.Add(caseStr[j]);
                }//SpellAI _spellAI,Status _target,Status _self
                spellAICase = (SpellAICase)System.Activator.CreateInstance(Type.GetType(caseName),this, caseValueList,i);
                SpellAICaseList.Add(spellAICase);
                CasePassList.Add(i,new bool());//新增一個條件限制，如果一個SpellAI有3個條件限制，則此ActiveList會新增3次
            }
        }
    }


    private void SetSpellAICase(SpellAICase _case)
    {
        spellAICase = _case;
    }

}
