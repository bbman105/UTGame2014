using UnityEngine;
using System.Collections;
using System;

public partial class PlayerMonster : Monster
{
    public void SetEvolution(string _species, string _speciesLevel, int _levelSpecies)
    {
        try
        {
            string LevelSpeciesName = "";
            string LevelSpeciesValue = "";
            if (_levelSpecies < 4)//如果是進化到二階
            {
                LevelSpeciesName = "LV2Species";
            }
            else
            {
                LevelSpeciesName = "LV3Species";
            }
            LevelSpeciesValue = _levelSpecies.ToString();
            string[] attrNameArray = new string[3] { "Species", "SpeciesLevel", LevelSpeciesName };
            string[] attrValueArray = new string[3] { _species, _speciesLevel, LevelSpeciesValue };
            IODataFromArcalet.SetPlayerMonster(attrNameArray, attrValueArray, MonsterID);//向Server寫入玩者怪獸資料
        }
        catch(Exception ex)
        {
            Debug.LogWarning("進化資料要送至Server時發生錯誤");
            Debug.LogWarning(ex);
        }

    }
    /// <summary>
    /// 設定物種
    /// </summary>
    public void SetSpecies(int _value)
    {
        Species = _value;
    }
    /// <summary>
    /// 設定物種階級
    /// </summary>
    public void SetSpeciesLevel(int _value)
    {
        SpeciesLevel = _value;
    }
    /// <summary>
    /// 傳入選擇的物種，設定二階選擇進化的物種
    /// </summary>
    public void SetLevel2Species(int _value)
    {
        Level2Species = _value;
    }
    /// <summary>
    /// 傳入選擇的物種，設定三階選擇進化的物種
    /// </summary>
    public void SetLevel3Species(int _value)
    {
        Level3Species = _value;
    }
}
