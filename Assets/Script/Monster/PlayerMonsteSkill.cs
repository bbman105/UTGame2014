using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{
    //此怪物擁有的技能
    public List<Skill> OwnSkillList;
    //此怪物目前選擇的技能
    public Skill SelectedSkill { get; set; }

    private void StartSetMonsterSkill()//抓取此怪獸目前擁有的技能與已學得的技能
    {
        ChangeMonsterSkill(SelectedSkillID);//賭取目前選擇的技能
        OwnSkillList = new List<Skill>();
        SetOwnSkill();//取得擁有的技能
    }
    public void SetOwnSkill()//取得擁有的技能
    {
        OwnSkillList.Clear();//清空以學的技能
        if (GameDictionary.SpeciesEvolutionSkillDic[Species].ContainsKey(0))
        {
            for (int i = 0; i < GameDictionary.SpeciesEvolutionSkillDic[Species][0].Length; i++)
            {
                OwnSkillList.Add(GameDictionary.SpeciesEvolutionSkillDic[Species][0][i]);
            }
        }
        if (Level2Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionSkillDic[Species].ContainsKey(Level2Species))
            {
                for (int i = 0; i < GameDictionary.SpeciesEvolutionSkillDic[Species][Level2Species].Length; i++)
                {
                    OwnSkillList.Add(GameDictionary.SpeciesEvolutionSkillDic[Species][Level2Species][i]);
                }
            }
            else
            {
                Debug.Log("怪獸第二階進化階段錯誤，讀取不到第二階物種擁有的技能");
            }
        }
        if (Level3Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionSkillDic[Species].ContainsKey(Level3Species))
            {
                for (int i = 0; i < GameDictionary.SpeciesEvolutionSkillDic[Species][Level3Species].Length; i++)
                {
                    OwnSkillList.Add(GameDictionary.SpeciesEvolutionSkillDic[Species][Level3Species][i]);
                }
            }
            else
            {
                Debug.Log("怪獸第三階進化階段錯誤，讀取不到第三階物種擁有的技能");
            }
        }
    }
    public void ChangeMonsterSkill(int _spellID)
    {
        if (GameDictionary.SkillDic.ContainsKey(_spellID))
        {
            IODataFromArcalet.SetPlayerMonster("SelectedSkill", _spellID.ToString(), MonsterID);//向Server寫入玩者怪獸資料
            SelectedSkillID = _spellID;
            SelectedSkill = new Skill(_spellID);
            Debug.Log("怪獸技能已更換");
        }
        else
        {
            Debug.Log("更換不存在的怪獸技能ID");
        }
    }

}
