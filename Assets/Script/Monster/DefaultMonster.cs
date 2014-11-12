using UnityEngine;
using System.Collections;

public struct DefaultMonsterData
{
    public int DefaultMosnterID { get; set; }
    public string MonsterName { get; set; }
    public int Species { get; set; }
    public int SpeciesLevel { get; set; }
    public int Rare { get; set; }
    public int LV { get; set; }
    public int LvHealth { get; set; }
    public int Wild { get; set; }
    public int Mutation { get; set; }
    public int Natural { get; set; }
    public int Happy { get; set; }
    public int LvBrutal { get; set; }
    public int LvAggressive { get; set; }
    public int LvResistance { get; set; }
    public int MainElement { get; set; }
    public int EXP { get; set; }
    public int Personality { get; set; }
    public int SelectedSkill { get; set; }
    public int Level2Species { get; set; }
    public int Level3Species { get; set; }
    public int InRoomID { get; set; }
    public void DefaultSetMonsterData(int _defaultMonsterID,string _monsterName,int _species,int _speciesLevel,
        int _rare,int _lv,int _lvHealth,int _wild,int _mutation,int _natural,int _happy,int _lvBrutal,int _lvAgressive,int _lvResistance,
        int _mainElement,int _exp,int _personality,int _selectedSkill,int _level2Species,int _level3Species,int _inRoomID
        )
    {
        DefaultMosnterID = _defaultMonsterID;
        MonsterName = _monsterName;
        Species = _species;
        SpeciesLevel = _speciesLevel;
        Rare = _rare;
        LV = _lv;
        LvHealth = _lvHealth;
        Wild = _wild;
        Mutation = _mutation;
        Natural = _natural;
        Happy = _happy;
        LvBrutal = _lvBrutal;
        LvAggressive = _lvAgressive;
        LvResistance = _lvResistance;
        MainElement = _mainElement;
        EXP = _exp;
        Personality = _personality;
        SelectedSkill = _selectedSkill;
        Level2Species = _level2Species;
        Level3Species = _level3Species;
        InRoomID = _inRoomID;       
    }
}
