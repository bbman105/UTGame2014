using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;



public class Skill : BaseSkill
{//Skill類別為非戰鬥用技能類別，戰鬥用為Spell
    public Skill()
    {

    }

    public Skill(int _SpellID)
    {
        SpellID = _SpellID;
        LoadData();
    }
    public void LoadData()
    {
        if (SpellID != 0)//按邊號讀取技能
        {
            //依照法術編號讀取法術資料中的法術各屬性
            TextAsset XMLFlie = Resources.Load<TextAsset>("String/Spell");
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFlie.text);
            XmlNode root = doc.DocumentElement;

            try
            {
                //抓取此技能的node區段
                XmlNode SpellNode = root.SelectSingleNode("SPID" + SpellID);
                SkillName = SpellNode.SelectSingleNode("SkillName").InnerText;
                Description = SpellNode.SelectSingleNode("Description").InnerText;
                Damage = float.Parse(SpellNode.SelectSingleNode("Damage").InnerText);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                Debug.Log("讀取技能資料時發生錯誤");
            }
        }
    }

}
