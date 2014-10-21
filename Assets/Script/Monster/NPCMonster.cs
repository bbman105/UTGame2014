using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class NPCMonster : Monster
{
    private string npcType;
    public string NpcType
    {
        get { return npcType; }
        set { npcType = value; }
    }

    private int exp;
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    private int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public void StartSetMonsterAttribute(TextAsset XMLFile, int MID)
    {
        Debug.Log("test，要修改為讀取DIC");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFile.text);
        //讀取xml文件中欲抓取的MonsterID標籤
        XmlNode MIDXml = doc.SelectSingleNode(string.Format("root/MID{0}", MID));
        //從文件中讀取資料
        try
        {
            MonsterID = int.Parse(MIDXml.SelectSingleNode("MonsterID").InnerText);
            MonsterName = MIDXml.SelectSingleNode("Name").InnerText;
            Species = int.Parse(MIDXml.SelectSingleNode("Species").InnerText);
            SpeciesName = GameDictionary.SpeciesNameDic[SpeciesKey];
            Rare = int.Parse(MIDXml.SelectSingleNode("Rare").InnerText);
            Lv = int.Parse(MIDXml.SelectSingleNode("LV").InnerText);
            //Health = int.Parse(MIDXml.SelectSingleNode("Health").InnerText);
            //Brutal = int.Parse(MIDXml.SelectSingleNode("Brutal").InnerText);
            //Aggressive = int.Parse(MIDXml.SelectSingleNode("Aggressive").InnerText);
            //Resistance = int.Parse(MIDXml.SelectSingleNode("Resistance").InnerText);
            MainElement = int.Parse(MIDXml.SelectSingleNode("MainElement").InnerText);
            Gold = int.Parse(MIDXml.SelectSingleNode("Gold").InnerText);
            Exp = int.Parse(MIDXml.SelectSingleNode("EXP").InnerText);
            NpcType = MIDXml.SelectSingleNode("NPCType").InnerText;

        }
        catch (Exception ex)
        {
            Debug.Log("讀取NPC怪物數值文件時發生錯誤，文件名稱NPCMonster.xml");
            throw ex;
        }
        //依怪物種類設定物件圖像
        SetSprite();
        //依怪物名稱設定物件名稱
        SetName();
    }
    //設定sprite的方法
    private void SetSprite()
    {
        MonsterSprite = new SpriteRenderer();
        MonsterSprite = transform.FindChild("status").FindChild("sprite").GetComponent<SpriteRenderer>();
        MonsterSprite.sprite = Resources.Load<Sprite>(string.Format("Sprite/MonsterSprite/{0}", Species));
    }
    //設定名稱的方法
    private void SetName()
    {
        transform.name = MonsterName;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
