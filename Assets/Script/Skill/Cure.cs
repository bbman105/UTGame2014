using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Cure
{

    int cureID;
    public int CureID
    {
        get { return cureID; }
        set { cureID = value; }
    }
    bool targetIsSelf;
    public bool TargetIsSelf
    {
        get { return targetIsSelf; }
        set { targetIsSelf = value; }
    }
    bool isPreATK;
    public bool IsPreATK
    {
        get { return isPreATK; }
        set { isPreATK = value; }
    }
    int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    float probability;
    public float Probability
    {
        get { return probability; }
        set { probability = value; }
    }
    int baseCure;
    public int BaseCure
    {
        get { return baseCure; }
        set { baseCure = value; }
    }
    float selfCurHealthTimes;
    public float SelfCurHealthTimes
    {
        get { return selfCurHealthTimes; }
        set { selfCurHealthTimes = value; }
    }
    float selfMaxHealthTimes;
    public float SelfMaxHealthTimes
    {
        get { return selfMaxHealthTimes; }
        set { selfMaxHealthTimes = value; }
    }
    float selfLoseHealthTimes;
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
    float damageTimes;
    public float DamageTimes
    {
        get { return damageTimes; }
        set { damageTimes = value; }
    }
    float receiveDamageTimes;
    public float ReceiveDamageTimes
    {
        get { return receiveDamageTimes; }
        set { receiveDamageTimes = value; }
    }



    public Cure(int _cureID,bool _targetIsSelf)
    {

        if(_cureID!=0)
        {
            //依照法術傷害編號讀取法術傷害資料中的各屬性
            TextAsset XMLFlie = Resources.Load<TextAsset>("String/Cure");
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFlie.text);
            XmlNode root = doc.DocumentElement;


            try
            {
                //抓取此技能的node區段
                XmlNode CureNode = root.SelectSingleNode("CID" + _cureID);
                CureID = int.Parse(CureNode.SelectSingleNode("CureID").InnerText);
                IsPreATK = bool.Parse(CureNode.SelectSingleNode("PreATK").InnerText);
                Speed = int.Parse(CureNode.SelectSingleNode("Speed").InnerText);
                Probability = float.Parse(CureNode.SelectSingleNode("Probability").InnerText);
                BaseCure = int.Parse(CureNode.SelectSingleNode("BaseCure").InnerText);
                //治癒增值(自身目前生命)
                SelfCurHealthTimes = float.Parse(CureNode.SelectSingleNode("SelfCurHealthTimes").InnerText);
                //治癒增值(自身最大生命)
                SelfMaxHealthTimes = float.Parse(CureNode.SelectSingleNode("SelfMaxHealthTimes").InnerText);
                //治癒增值(自身損失生命)
                SelfLoseHealthTimes = float.Parse(CureNode.SelectSingleNode("SelfLoseHealthTimes").InnerText);
                //治癒增值(目標目前生命)
                TargetCurHealthTimes = float.Parse(CureNode.SelectSingleNode("TargetCurHealthTimes").InnerText);
                //治癒增值(目標最大生命)
                TargetMaxHealthTimes = float.Parse(CureNode.SelectSingleNode("TargetMaxHealthTimes").InnerText);
                //治癒增值(目標損失生命)
                TargetLoseHealthTimes = float.Parse(CureNode.SelectSingleNode("TargetLoseHealthTimes").InnerText);
                DamageTimes = float.Parse(CureNode.SelectSingleNode("DamageTimes").InnerText);
                ReceiveDamageTimes = float.Parse(CureNode.SelectSingleNode("ReceiveDamageTimes").InnerText);
                TargetIsSelf = _targetIsSelf;
 
            }
            catch
            {
                Debug.Log("讀取Cure.xml資料時發生錯誤");
            }
        }
    }
}
