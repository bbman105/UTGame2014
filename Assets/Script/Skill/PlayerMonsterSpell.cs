using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;



public class PlayerMonsterSpell : Spell, IComparable
{//PlayerMonsterSpell為玩家怪獸戰鬥時所擁有的技能
    public int Rank { get; set; }

    public PlayerMonsterSpell()
    {
        SpellID = 0;//當spellID是0時，等於普攻
        Rank = 3;//預設排序為最後
        LoadData();
    }
    public PlayerMonsterSpell(int _spellID)//玩家怪物施法的建構式
        : base(_spellID)
    {
        SpellID = _spellID;
        LoadData();
    }



    int IComparable.CompareTo(System.Object obj)//依Rank做排序
    {
        int result = 0;
        PlayerMonsterSpell spell = (PlayerMonsterSpell)obj;
        result = this.Rank - spell.Rank;
        return result;
    }
}
