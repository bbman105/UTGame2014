using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

public class SetPlayerMonsterAttribute : SetMonsterAttribute
{
    int SelectedSpellID;

    public override void SetAttribute(int _monsterID, Status _status)
    {
        status = _status;
        status.StartSetCharaCalculator();//初始化計算器
        //設定怪物屬性
        status.MainElement = Player.MonsterDic[_monsterID].MainElement;
        //設定怪物品質
        status.Rare = Player.MonsterDic[_monsterID].Rare;
        //設定狀態屬性
        status.Alive = true;
        //取得名稱
        status.MonsterName = Player.MonsterDic[_monsterID].MonsterName;
        status.TmpMonsterID = tmpMonsterID++;
        //取得等級
        status.Lv = Player.MonsterDic[_monsterID].Lv;
        //取得血量
        status.StartSetHealthStatus();//初始設置HealthStatus
        status.SetMaxHealth(Player.MonsterDic[_monsterID].Health);
        status.SetCurHealth(Player.MonsterDic[_monsterID].Health);
        //取得體型
        status.Size = Player.MonsterDic[_monsterID].Size;
        //取得物種
        status.Species = Player.MonsterDic[_monsterID].Species;
        status.SpeciesLevel = Player.MonsterDic[_monsterID].SpeciesLevel;
        status.SpeciesKey = Player.MonsterDic[_monsterID].SpeciesKey;
        //取得殘暴
        status.Brutal = Player.MonsterDic[_monsterID].Brutal;
        status.BrutalPlus = 0;
        status.BrutalTimes = 1;
        //取得傷害加成
        status.DamagePlus = 0;
        status.DamageTimes = 1;
        //取得侵略
        status.Aggressive = Player.MonsterDic[_monsterID].Aggressive;
        status.AggressivePlus = 0;
        status.AggressiveTimes = 1;
        //取得速度
        status.SpeedTimes = 1;
        status.SpeedPlus = 0;
        //取得抵抗
        status.Resistance = Player.MonsterDic[_monsterID].Resistance;
        status.ResistancePlus = 0;
        status.ResistanceTimes = 1;
        //取得防禦加成
        status.DefensePlus = 0;
        status.DefenseTimes = 1;
        //取得治癒效果倍率加乘
        status.CureTimes = 1;


        status.DieSet();//初始化死亡屬性
        status.RetreatStartSet();//初始化撤退屬性
        status.KnockSet();//初始化撞擊屬性
        SetMonsterSkill(_monsterID);//讀取技能
        SetMonsterTalent(_monsterID);//讀取天賦
        status.StartSetModeler();//設置怪獸模型修改器
        status.SpeciesModeler.SetSpeciesModel();
    }

    void SetMonsterTalent(int _monsterID)
    {
        status.TalentList = new List<FightTalent>();
        for (int i = 0; i < Player.MonsterDic[_monsterID].OwnTalentList.Count; i++)
        {
            status.TalentList.Add(new FightTalent(Player.MonsterDic[_monsterID].OwnTalentList[i].TalentID));
        }
        for (int i = 0; i < Player.MonsterDic[_monsterID].OwnFeatureList.Count; i++)
        {
            status.TalentList.Add(new FightTalent(Player.MonsterDic[_monsterID].OwnFeatureList[i].OwnTalent.TalentID));
        }
        for(int i=0;i<status.TalentList.Count;i++)
        {
            status.TalentList[i].GiveEffect(status);
        }
    }

    void SetMonsterSkill(int _monsterID)
    {
        //建立怪物擁有技能清單，並依照技能順序排序清單
        SelectedSpellID = Player.MonsterDic[_monsterID].SelectedSkill.SpellID;
        //建立此怪物的skill清單
        status.SpellList = new List<Spell>();
        status.SpellList.Add(new PlayerMonsterSpell(SelectedSpellID));
    }
}
