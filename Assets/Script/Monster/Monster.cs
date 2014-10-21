using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public abstract class Monster : MonoBehaviour
{
    //monsterResource
    public SpriteRenderer MonsterSprite { get; set; }//怪獸的圖像
    /////////////////////////怪物屬性////////////////////////////
    //怪物編號，不會重複，每隻怪物獨有的EntityID
    private int monsterID;
    public int MonsterID
    {
        get { return monsterID; }
        set
        {
            if (value < 0)
            {
                monsterID = 0;
            }
            else { monsterID = value; }
        }
    }
    //名稱
    private string monsterName;
    public string MonsterName
    {
        get { return monsterName; }
        set { monsterName = value; }
    }
    //物種，值不可為負數
    private int _species;
    public int Species
    {
        get { return _species; }
        set
        {
            if (value < 0)
            {
                _species = 0;
            }
            else { _species = value; }
        }
    }
    private int speciesLevel;
    public int SpeciesLevel
    {
        get { return speciesLevel; }
        set { speciesLevel = value; }
    }
    private string speciesKey;
    public string SpeciesKey
    {
        get { return speciesKey; }
        set { speciesKey = value; }
    }
    private string speciesName;
    public string SpeciesName
    {
        get { return speciesName; }
        set { speciesName = value; }
    }
    //罕見值，區間在1~5，1=銅,5=紫
    private int rare;
    public int Rare
    {
        get { return rare; }
        set
        {
            if (value > 5)
            {
                rare = 5;
            }
            else if (value < 1)
            {
                rare = 1;
            }
            else { rare = value; }
        }
    }
    //體型，值不可為負數
    private float size;
    public float Size
    {
        get { return size; }
        set
        {
            if (value < 0) { size = 0; }
            else { size = value; }
        }
    }
    //階級，屬於第幾階，目前開放到第三階，1=1階、2=2階，3=3階值不可小於1或大於3
    private int evoPhase;
    public int EvoPhase
    {
        get { return evoPhase; }
        set
        {
            if (value < 1)
            {
                evoPhase = 1;
            }
            else if (value > 3)
            {
                evoPhase = 3;
            }
            else { evoPhase = value; }
        }
    }
    //怪物等級，介於1~30之間，目前開放到30級
    private int lv;
    public int Lv
    {
        get { return lv; }
        set
        {
            if (value > 30)
            {
                lv = 30;
            }
            else if (value < 1)
            {
                lv = 1;
            }
            else { lv = value; }
        }
    }
    //怪物生命，介於0~5000之間
    private int health;
    public int Health
    {
        get { return health; }
        protected set
        {
            if (value > 5000)
            {
                health = 5000;
            }
            else if (value < 0)
            {
                health = 0;
            }
            else { health = value; }
        }
    }

    //主屬性
    private int mainElement;
    public int MainElement
    {
        get { return mainElement; }
        protected set { mainElement = value; }
    }

    ///////////////////////////三大戰鬥基底屬性/////////////////////
    //殘暴性
    private int brutal;
    public int Brutal
    {
        get { return brutal; }
        protected set
        {
            if (value < 0)
                brutal = 0;
            else
                brutal = value;
        }
    }
    //侵略性
    private int aggressive;
    public int Aggressive
    {
        get { return aggressive; }
        protected set
        {
            if (value < 0)
                aggressive = 0;
            else
                aggressive = value;
        }
    }
    //防禦性
    private int resistance;
    public int Resistance
    {
        get { return resistance; }
        protected set
        {
            if (value < 0)
                resistance = 0;
            else
                resistance = value;
        }
    }
    public int InRoomID { get; set; }//所在房間ID




    public string RareToString(int _rare)
    {
        string result = "";
        switch (_rare)
        {
            case 1:
                result = "Copper";
                break;
            case 2:
                result = "Silver";
                break;
            case 3:
                result = "Gold";
                break;
            case 4:
                result = "Dimand";
                break;
            case 5:
                result = "Crystal";
                break;
        }
        return result;
    }
}
