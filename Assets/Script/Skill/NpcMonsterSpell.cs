using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;



public class NpcMonsterSpell : Spell
{//PlayerMonsterSpell為玩家怪獸戰鬥時所擁有的技能

    public NpcMonsterSpell(int _spellID)//Npc怪物施法的建構式
        : base(_spellID)
    {
        SpellID = _spellID;
        LoadData();
    }

}
