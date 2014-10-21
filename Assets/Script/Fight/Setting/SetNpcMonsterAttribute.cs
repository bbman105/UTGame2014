using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

public class SetNpcMonsterAttribute : SetMonsterAttribute
{
    NPC npc;
    Sprite NpcTypeSprite;//Npc種類的圖像，如果是Boss顯示金黃冠，屬於小怪(Underline)就不顯示

    public void SetNpcType(Status status, GameObject _chara)//設定Npc種類頭上ICON
    {
        if (status.NpcType != "" || status.NpcType != "Underling")
        {
            _chara.transform.FindChild("NpcType").gameObject.SetActive(true);
            NpcTypeSprite = Resources.Load<Sprite>(string.Format("Sprite/HeadIcon/{0}", status.NpcType));
            _chara.transform.FindChild("NpcType").FindChild("type").GetComponent<SpriteRenderer>().sprite = NpcTypeSprite;
        }
        else
        {
            _chara.transform.FindChild("NpcType").gameObject.SetActive(false);
        }
    }

    public override void SetAttribute(int _monsterID, Status _status)
    {
        npc = GameDictionary.NPCDic[_monsterID];
        status = _status;
        status.StartSetCharaCalculator();//初始化計算器
        //設定怪物屬性
        status.MainElement = npc.MainElement;
        //設定狀態屬性
        status.Alive = true;
        status.MonsterID = _monsterID;
        //取得名稱
        status.MonsterName = npc.MonsterName;
        status.TmpMonsterID = tmpMonsterID++;
        //取得AI
        status.AiID = npc.AIID;
        //取德掉落戰利品ID
        status.LootID = npc.Loot;
        status.DropPercent = npc.DropPercent;
        //取得等級
        status.Lv = npc.LV;
        //取得經驗
        status.Exp = npc.EXP;
        //取得Npc種類
        status.NpcType = npc.NPCType;
        //取得金錢
        status.Gold = npc.Gold;
        //取得血量
        status.StartSetHealthStatus();//初始設置HealthStatus
        status.SetMaxHealth(npc.Health);
        status.SetCurHealth(npc.Health);
        //取得體型
        status.Size = npc.Size;
        //取得物種
        status.Species = npc.Species;
        status.SpeciesLevel = npc.SpeciesLevel;
        status.SpeciesKey = string.Format("{0}-{1}", status.Species, status.SpeciesLevel);
        //取得殘暴
        status.Brutal = npc.Brutal;
        status.BrutalPlus = 0;
        status.BrutalTimes = 1;
        //取得傷害加成
        status.DamagePlus = 0;
        status.DamageTimes = 1;
        //取得侵略
        status.Aggressive = npc.Aggressive;
        status.AggressivePlus = 0;
        status.AggressiveTimes = 1;
        //取得速度
        status.SpeedTimes = 1;
        status.SpeedPlus = 0;
        //取得抵抗
        status.Resistance = npc.Resistance;
        status.ResistancePlus = 0;
        status.ResistanceTimes = 1;
        //取得防禦加成
        status.DefensePlus = 0;
        status.DefenseTimes = 1;
        //取得治癒效果倍率加乘
        status.CureTimes = 1;


        status.DieSet();//初始化死亡屬性
        status.KnockSet();//初始化撞擊屬性
        SetMonsterSkill(_monsterID, status);//讀取技能
        status.StartSetModeler();//設置怪獸模型修改器
        status.SpeciesModeler.SetSpeciesModel();
        SetNpcType(status, status.gameObject);//設置NPC種類ICON
    }

    void SetMonsterSkill(int _monsterID, Status status)
    {
        status.Ai = new NpcAI(status.AiID, status);
        status.SpellList = new List<Spell>();
        for (int i = 0; i < status.Ai.SpellAIList.Count; i++)
        {
            status.SpellList.Add(new NpcMonsterSpell(status.Ai.SpellAIList[i].SpellID));
        }
        status.NpcSkillSet();
    }
}
