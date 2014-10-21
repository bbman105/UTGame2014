using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;



public class Spell : BaseSkill
{//Spell為怪獸戰鬥時所擁有的技能
    public int TargetEffectID { get; set; }
    public int TargetDamageID { get; set; }
    public int TargetCureID { get; set; }
    public int SelfEffectID { get; set; }
    public int SelfDamageID { get; set; }
    public int SelfCureID { get; set; }
    public List<Damage> TargetDamageClass { get; set; }
    public List<StatusEffect> TargetPreEffectClass { get; set; }
    public List<StatusEffect> TargetReEffectClass { get; set; }
    public List<Cure> TargetPreCureClass { get; set; }
    public List<Cure> TargetReCureClass { get; set; }
    public List<Damage> SelfDamageClass { get; set; }
    public List<StatusEffect> SelfPreEffectClass { get; set; }
    public List<StatusEffect> SelfReEffectClass { get; set; }
    public List<Cure> SelfPreCureClass { get; set; }
    public List<Cure> SelfReCureClass { get; set; }
    private Cure TmpCure { get; set; }
    private StatusEffect TmpEffect { get; set; }
    ExcuteDamage targetDamage;
    ExcuteDamage selfDamage;
    ExcuteCure targetCure;
    ExcuteCure selfCure;
    ExcuteEffect newExcuteEffect;
    static XmlDocument doc;
    XmlNode SpellNode;
    public Spell()
    {

    }
    public Spell(int _SpellID)
    {
        SpellID = _SpellID;
        LoadData();
    }
    public void LoadData()
    {
        TargetDamageClass = new List<Damage>();
        TargetPreEffectClass = new List<StatusEffect>();
        TargetReEffectClass = new List<StatusEffect>();
        TargetPreCureClass = new List<Cure>();
        TargetReCureClass = new List<Cure>();
        SelfDamageClass = new List<Damage>();
        SelfPreEffectClass = new List<StatusEffect>();
        SelfReEffectClass = new List<StatusEffect>();
        SelfPreCureClass = new List<Cure>();
        SelfReCureClass = new List<Cure>();
        targetDamage = new ExcuteDamage();
        selfDamage = new ExcuteDamage();
        targetCure = new ExcuteCure();
        selfCure = new ExcuteCure();

        if (doc == null)
        {
            try
            {
                doc = new XmlDocument();
                doc.LoadXml(Resources.Load<TextAsset>("String/Spell").text);
            }
            catch
            {
                Debug.Log(string.Format("從入徑{0}中，找不到文件", "String/Spell"));
            }
        }


        if (SpellID != 0)//按邊號讀取技能
        {
            try
            {
                //抓取此技能的node區段
                SpellNode = doc.SelectSingleNode("root/SPID" + SpellID);
                SkillName = SpellNode.SelectSingleNode("SkillName").InnerText;
                Description = SpellNode.SelectSingleNode("Description").InnerText;

                TargetAni = int.Parse(SpellNode.SelectSingleNode("TargetAni").InnerText);
                TargetAreaAni = int.Parse(SpellNode.SelectSingleNode("TargetAreaAni").InnerText);
                SelfAni = int.Parse(SpellNode.SelectSingleNode("SelfAni").InnerText);
                SelfAreaAni = int.Parse(SpellNode.SelectSingleNode("SelfAreaAni").InnerText);

                //觸發目標效果
                string[] triggerTargetStr = SpellNode.SelectSingleNode("TargetEffect").InnerText.Split(',');
                string[] triggerTargetType = new string[triggerTargetStr.Length];
                string[] triggerTargetID = new string[triggerTargetStr.Length];
                for (int i = 0; i < triggerTargetStr.Length; i++)
                {

                    triggerTargetType[i] = triggerTargetStr[i].Substring(0, 1);
                    triggerTargetID[i] = triggerTargetStr[i].Substring(1);
                    if (triggerTargetType[i] != "0")
                    {
                        switch (triggerTargetType[i])
                        {
                            case "D":
                                TargetDamageID = int.Parse(triggerTargetID[i]);
                                TargetDamageClass.Add(new Damage(TargetDamageID, false));
                                break;
                            case "C":
                                TargetCureID = int.Parse(triggerTargetID[i]);
                                TmpCure = new Cure(TargetCureID, false);
                                if (TmpCure.IsPreATK)
                                    TargetPreCureClass.Add(TmpCure);
                                else
                                    TargetReCureClass.Add(TmpCure);
                                break;
                            case "E":
                                TargetEffectID = int.Parse(triggerTargetID[i]);
                                TmpEffect = new StatusEffect(TargetEffectID, false);
                                if (TmpEffect.IsPreATK)
                                    TargetPreEffectClass.Add(TmpEffect);
                                else
                                    TargetReEffectClass.Add(TmpEffect);
                                break;
                            default:
                                Debug.Log("施法觸發目標法術的類型無法辨識");
                                break;
                        }
                    }
                }

                //觸發自身效果
                string[] triggerSelfStr = SpellNode.SelectSingleNode("SelfEffect").InnerText.Split(',');
                string[] triggerSelfType = new string[triggerSelfStr.Length];
                string[] triggerSelfID = new string[triggerSelfStr.Length];
                for (int i = 0; i < triggerSelfStr.Length; i++)
                {
                    triggerSelfType[i] = triggerSelfStr[i].Substring(0, 1);
                    triggerSelfID[i] = triggerSelfStr[i].Substring(1);
                    if (triggerSelfType[i] != "0")
                    {
                        switch (triggerSelfType[i])
                        {
                            case "D":
                                SelfDamageID = int.Parse(triggerSelfID[i]);
                                SelfDamageClass.Add(new Damage(SelfDamageID, true));
                                break;
                            case "C":
                                SelfCureID = int.Parse(triggerSelfID[i]);
                                TmpCure = new Cure(SelfCureID, true);
                                if (TmpCure.IsPreATK)
                                    SelfPreCureClass.Add(TmpCure);
                                else
                                    SelfReCureClass.Add(TmpCure);
                                break;
                            case "E":
                                SelfEffectID = int.Parse(triggerSelfID[i]);
                                TmpEffect = new StatusEffect(SelfEffectID, true);
                                if (TmpEffect.IsPreATK)
                                    SelfPreEffectClass.Add(TmpEffect);
                                else
                                {
                                    SelfReEffectClass.Add(TmpEffect);
                                }

                                break;
                            default:
                                Debug.Log("施法觸發自身法術的類型無法辨識");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(String.Format("在抓取Spell.xml檔案時，讀取施法技能ID{0}時發生錯誤", SpellID));
                Debug.Log(ex);
            }

        }
    }
    public void ExcuteSpellDamage(Status _attacker, Status _receiver)
    {
        ExcuteTargetDamage(_attacker, _receiver);
        ExcuteSelfDamage(_attacker, _receiver);
    }
    public void ExcutePreEffect(Status _attacker, Status _receiver)
    {
        ExcuteTargetPreEffect(_attacker, _receiver);
        ExcuteSelfPreEffect(_attacker, _receiver);
    }
    public void ExcuteReEffect(Status _attacker, Status _receiver)
    {
        ExcuteTargetReEffect(_attacker, _receiver);
        ExcuteSelfReEffect(_attacker, _receiver);
    }
    public void ExcutePreCure(Status _attacker, Status _receiver)
    {
        ExcuteTargetPreCure(_attacker, _receiver);
        ExcuteSelfPreCure(_attacker, _receiver);
    }
    public void ExcuteReCure(Status _attacker, Status _receiver)
    {
        ExcuteTargetReCure(_attacker, _receiver);
        ExcuteSelfReCure(_attacker, _receiver);
    }
    public void SpawnEffect(Status _attacker, Status _receiver)
    {
        if (TargetAni != 0)
        {
            EffectSpawner targetES = new EffectSpawner(TargetAni);
            targetES.SpawnTargetEffect(_attacker, _receiver);
        }
        if (TargetAreaAni != 0)
        {
            EffectSpawner targetAreaES = new EffectSpawner(TargetAreaAni);
            targetAreaES.SpawnAreaEffect(_attacker, _receiver);
        }
        if (SelfAni != 0)
        {
            EffectSpawner selfES = new EffectSpawner(SelfAni);
            selfES.SpawnTargetEffect(_attacker, _attacker);
        }
        if (SelfAreaAni != 0)
        {
            EffectSpawner selfAreaES = new EffectSpawner(SelfAreaAni);
            selfAreaES.SpawnAreaEffect(_attacker, _attacker);
        }
    }
    void ExcuteTargetPreEffect(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < TargetPreEffectClass.Count; i++)
        {
            newExcuteEffect = _receiver.gameObject.AddComponent<ExcuteEffect>();
            newExcuteEffect.SetEffect(_attacker, _receiver, TargetPreEffectClass[i]);
        }
    }
    void ExcuteTargetReEffect(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < TargetReEffectClass.Count; i++)
        {
            newExcuteEffect = _receiver.gameObject.AddComponent<ExcuteEffect>();
            newExcuteEffect.SetEffect(_attacker, _receiver, TargetReEffectClass[i]);
        }
    }
    void ExcuteSelfPreEffect(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < SelfPreEffectClass.Count; i++)
        {
            newExcuteEffect = _attacker.gameObject.AddComponent<ExcuteEffect>();
            newExcuteEffect.SetEffect(_attacker, _receiver, SelfPreEffectClass[i]);
        }
    }
    void ExcuteSelfReEffect(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < SelfReEffectClass.Count; i++)
        {
            newExcuteEffect = _attacker.gameObject.AddComponent<ExcuteEffect>();
            newExcuteEffect.SetEffect(_attacker, _receiver, SelfReEffectClass[i]);
        }
    }
    void ExcuteTargetPreCure(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < TargetPreCureClass.Count; i++)
        {
            targetCure.SetCure(_attacker, _receiver, TargetPreCureClass[i]);
            targetCure.Excute(21);
        }
    }
    void ExcuteSelfPreCure(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < SelfPreCureClass.Count; i++)
        {
            selfCure.SetCure(_attacker, _receiver, SelfPreCureClass[i]);
            selfCure.Excute(21);
        }
    }
    void ExcuteTargetReCure(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < TargetReCureClass.Count; i++)
        {
            targetCure.SetCure(_attacker, _receiver, TargetReCureClass[i]);
            targetCure.Excute(21);
        }
    }
    void ExcuteSelfReCure(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < SelfReCureClass.Count; i++)
        {
            selfCure.SetCure(_attacker, _receiver, SelfReCureClass[i]);
            selfCure.Excute(21);
        }
    }

    void ExcuteTargetDamage(Status _attacker, Status _receiver)
    {
        _attacker.KnockDamage = 0;//重設撞擊顯示傷害為0
        for (int i = 0; i < TargetDamageClass.Count; i++)
        {
            targetDamage.SetDamage(_attacker, _receiver, TargetDamageClass[i]);
            if (!_attacker.OnElementHit)//未爆擊
                targetDamage.Excute(18);
            else
                targetDamage.Excute(19);//爆擊

            _attacker.KnockDamage += targetDamage.ChangeValue;
        }
    }
    void ExcuteSelfDamage(Status _attacker, Status _receiver)
    {
        for (int i = 0; i < SelfDamageClass.Count; i++)
        {
            selfDamage.SetDamage(_attacker, _receiver, SelfDamageClass[i]);
            if (!_attacker.OnElementHit)//未爆擊
                selfDamage.Excute(18);
            else
                selfDamage.Excute(19);//爆擊
        }
    }

}
