using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Damage
{
    private int damageID;
    public int DamageID
    {
        get { return damageID; }
        set { damageID = value; }
    }
    private bool targetIsSelf;
    public bool TargetIsSelf
    {
        get { return targetIsSelf; }
        set { targetIsSelf = value; }
    }

    private float probability;
    public float Probability
    {
        get { return probability; }
        set { probability = value; }
    }
    private bool absoluteDamage;
    public bool AbsoluteDamage
    {
        get { return absoluteDamage; }
        set { absoluteDamage = value; }
    }
    private float penetration;
    public float Penetration
    {
        get { return penetration; }
        set { penetration = value; }
    }
    private float damageTimes;
    public float DamageTimes
    {
        get { return damageTimes; }
        set { damageTimes = value; }
    }
    private int damagePlus;
    public int DamagePlus
    {
        get { return damagePlus; }
        set { damagePlus = value; }
    }
    private float selfCurHealthTimes;
    public float SelfCurHealthTimes
    {
        get { return selfCurHealthTimes; }
        set { selfCurHealthTimes = value; }
    }
    private float selfMaxHealthTimes;
    public float SelfMaxHealthTimes
    {
        get { return selfMaxHealthTimes; }
        set { selfMaxHealthTimes = value; }
    }
    private float selfLoseHealthTimes;
    public float SelfLoseHealthTimes
    {
        get { return selfLoseHealthTimes; }
        set { selfLoseHealthTimes = value; }
    }
    private float targetCurHealthTimes;
    public float TargetCurHealthTimes
    {
        get { return targetCurHealthTimes; }
        set { targetCurHealthTimes = value; }
    }
    private float targetMaxHealthTimes;
    public float TargetMaxHealthTimes
    {
        get { return targetMaxHealthTimes; }
        set { targetMaxHealthTimes = value; }
    }
    private float targetLoseHealthTimes;
    public float TargetLoseHealthTimes
    {
        get { return targetLoseHealthTimes; }
        set { targetLoseHealthTimes = value; }
    }


    public Damage(int _damageID,bool _targetIsSelf)
    {
        if(_damageID!=0)
        {
            //依照法術傷害編號讀取法術傷害資料中的各屬性
            TextAsset XMLFlie = Resources.Load<TextAsset>("String/Damage");
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFlie.text);
            XmlNode root = doc.DocumentElement;



            try
            {
                //抓取此技能的node區段
                XmlNode SpellNode = root.SelectSingleNode("DID" + _damageID);
                DamageID = int.Parse(SpellNode.SelectSingleNode("DamageID").InnerText);
                TargetIsSelf = _targetIsSelf;
                AbsoluteDamage = bool.Parse(SpellNode.SelectSingleNode("AbsoluteDamage").InnerText);
                //傷害倍率改變
                DamageTimes = float.Parse(SpellNode.SelectSingleNode("DamageTimes").InnerText);
                //觸發機率
                Probability = float.Parse(SpellNode.SelectSingleNode("Probability").InnerText);
                //傷害增值改變
                DamagePlus = int.Parse(SpellNode.SelectSingleNode("DamagePlus").InnerText);
                //傷害穿透率
                Penetration = float.Parse(SpellNode.SelectSingleNode("Penetration").InnerText);
                //傷害增值(自身目前生命)
                SelfCurHealthTimes = float.Parse(SpellNode.SelectSingleNode("SelfCurHealthTimes").InnerText);
                //傷害增值(自身最大生命)
                SelfMaxHealthTimes = float.Parse(SpellNode.SelectSingleNode("SelfMaxHealthTimes").InnerText);
                //傷害增值(自身損失生命)
                SelfLoseHealthTimes = float.Parse(SpellNode.SelectSingleNode("SelfLoseHealthTimes").InnerText);
                //傷害增值(目標目前生命)
                TargetCurHealthTimes = float.Parse(SpellNode.SelectSingleNode("TargetCurHealthTimes").InnerText);
                //傷害增值(目標最大生命)
                TargetMaxHealthTimes = float.Parse(SpellNode.SelectSingleNode("TargetMaxHealthTimes").InnerText);
                //傷害增值(目標損失生命)
                TargetLoseHealthTimes = float.Parse(SpellNode.SelectSingleNode("TargetLoseHealthTimes").InnerText);
            }
            catch
            {
                Debug.Log("讀取Damage.xml資料時發生錯誤");
            }
        }

    }
}
