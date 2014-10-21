using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{

    //此怪物擁有的天賦
    public List<Talent> OwnTalentList;
    /////////////////////////影響屬性////////////////////////////////
    public const float BaseHealthTimes = 1;
    public const float BaseBrutalTimes = 1;
    public const float BaseAggressiveTiems = 1;
    public const float BaseResistanceTimes = 1;
    //升級
    public int AddLvHealthPlus;
    public int AddLvBrutalPlus;
    public int AddLvResistancePlus;
    public int AddLvAggressivePlus;
    public int LvHealthPlus;
    public int LvBrutalPlus;
    public int LvResistancePlus;
    public int LvAggressivePlus;
    public float LvSizePlus;
    //天賦加值屬性
    public int TalentHealthPlus;
    public float TalentHealthTimes;
    public int TalentBrutalPlus;
    public float TalentBrutalTimes;
    public int TalentResistancePlus;
    public float TalentResistanceTimes;
    public int TalentAggressivePlus;
    public float TalentAggressiveTimes;
    public float TalentSizePlus;
    //特徵加值屬性
    public int FeatureHealthPlus;
    public float FeatureHealthTimes;
    public int FeatureBrutalPlus;
    public float FeatureBrutalTimes;
    public int FeatureResistancePlus;
    public float FeatureResistanceTimes;
    public int FeatureAggressivePlus;
    public float FeatureAggressiveTimes;
    public float FeatureSizePlus;
    //品質
    public int RareHealthPlus;
    public int RareBrutalPlus;
    public int RareResistancePlus;
    public int RareAggressivePlus;
    //物種
    public int SpeciesHealthPlus;
    public int SpeciesBrutalPlus;
    public int SpeciesResistancePlus;
    public int SpeciesAggressivePlus;
    public float SpeciesSizePlus;
    public void SetProperty()//設定最終屬性
    {
        /*
        Debug.Log("LvBrutalPlus=" + LvBrutalPlus);
        Debug.Log("TalentBrutalPlus=" + TalentBrutalPlus);
        Debug.Log("FeatureBrutalPlus=" + FeatureBrutalPlus);
        Debug.Log("SpeciesBrutalPlus=" + SpeciesBrutalPlus);
        Debug.Log("RareBrutalPlus=" + RareBrutalPlus);
        Debug.Log("TalentBrutalTimes=" + TalentBrutalTimes);
        Debug.Log("FeatureBrutalTimes=" + FeatureBrutalTimes);
        */
        Health = (int)((LvHealthPlus + TalentHealthPlus + FeatureHealthPlus + SpeciesHealthPlus + RareHealthPlus) * (BaseHealthTimes + TalentHealthTimes + FeatureHealthTimes));
        Brutal = (int)((LvBrutalPlus + TalentBrutalPlus + FeatureBrutalPlus + SpeciesBrutalPlus + RareBrutalPlus) * (BaseBrutalTimes + TalentBrutalTimes + FeatureBrutalTimes));
        Resistance = (int)((LvResistancePlus + TalentResistancePlus + FeatureResistancePlus + SpeciesResistancePlus + RareResistancePlus) * (BaseResistanceTimes + TalentResistanceTimes + FeatureResistanceTimes));
        Aggressive = (int)((LvAggressivePlus + TalentAggressivePlus + FeatureAggressivePlus + SpeciesAggressivePlus + RareAggressivePlus) * (BaseAggressiveTiems + TalentAggressiveTimes + FeatureAggressiveTimes));
        Size = (TalentSizePlus + FeatureSizePlus + SpeciesSizePlus);
    }
    public void ReRollLvProperty()//重骰屬性
    {
        LvHealthPlus -= AddLvHealthPlus;
        LvBrutalPlus -= AddLvBrutalPlus;
        LvAggressivePlus -= AddLvAggressivePlus;
        LvResistancePlus -= AddLvResistancePlus;
        AddLvHealthPlus = 0;
        AddLvBrutalPlus = 0;
        AddLvResistancePlus = 0;
        AddLvAggressivePlus = 0;
        int deployPoint = 4;
        for (int i = 0; i < deployPoint; i++)
        {
            int rnd = UnityEngine.Random.Range(0, 4);
            switch (rnd)
            {
                case 0:
                    LvHealthPlus += 3;
                    AddLvHealthPlus += 3;
                    break;
                case 1:
                    LvBrutalPlus += 1;
                    AddLvBrutalPlus += 1;
                    break;
                case 2:
                    LvResistancePlus += 1;
                    AddLvResistancePlus += 1;
                    break;
                case 3:
                    LvAggressivePlus += 1;
                    AddLvAggressivePlus += 1;
                    break;
            }
        }
        SetProperty();//設定最終屬性
    }
    public void RollLvProperty()//升級骰升級屬性
    {
        AddLvHealthPlus = 0;
        AddLvBrutalPlus = 0;
        AddLvResistancePlus = 0;
        AddLvAggressivePlus = 0;
        int deployPoint = 4;
        for (int i = 0; i < deployPoint; i++)
        {
            int rnd = UnityEngine.Random.Range(0, 4);
            switch (rnd)
            {
                case 0:
                    LvHealthPlus += 3;
                    AddLvHealthPlus += 3;
                    break;
                case 1:
                    LvBrutalPlus += 1;
                    AddLvBrutalPlus += 1;
                    break;
                case 2:
                    LvResistancePlus += 1;
                    AddLvResistancePlus += 1;
                    break;
                case 3:
                    LvAggressivePlus += 1;
                    AddLvAggressivePlus += 1;
                    break;
            }
        }
        SetProperty();//設定最終屬性
    }
    public void SetSpeciesProperty()//設定物種進化影響屬性
    {
        SpeciesHealthPlus = 0;
        SpeciesBrutalPlus = 0;
        SpeciesResistancePlus = 0;
        SpeciesAggressivePlus = 0;
        SpeciesSizePlus = 0;
        if (GameDictionary.SpeciesEvolutionPropertyDic[Species].ContainsKey(0))
        {
            SpeciesHealthPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][0].AddHealth;
            SpeciesBrutalPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][0].AddBrutal;
            SpeciesResistancePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][0].AddResistance;
            SpeciesAggressivePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][0].AddAggressive;
            SpeciesSizePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][0].AddSize;
        }
        else
        {
            Debug.Log("Species=" + Species);
            Debug.Log("怪獸最初進化階段屬性讀取不到");
        }
        if (Level2Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionPropertyDic[Species].ContainsKey(Level2Species))
            {
                SpeciesHealthPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level2Species].AddHealth;
                SpeciesBrutalPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level2Species].AddBrutal;
                SpeciesResistancePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level2Species].AddResistance;
                SpeciesAggressivePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level2Species].AddAggressive;
                SpeciesSizePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level2Species].AddSize;
            }
            else
            {
                Debug.Log("Species=" + Species);
                Debug.Log("Level2Species=" + Level2Species);
                Debug.Log("怪獸第二階進化階段錯誤，讀取不到第二階物種進化屬性");
            }
        }
        if (Level3Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionPropertyDic[Species].ContainsKey(Level3Species))
            {
                SpeciesHealthPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level3Species].AddHealth;
                SpeciesBrutalPlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level3Species].AddBrutal;
                SpeciesResistancePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level3Species].AddResistance;
                SpeciesAggressivePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level3Species].AddAggressive;
                SpeciesSizePlus += GameDictionary.SpeciesEvolutionPropertyDic[Species][Level3Species].AddSize;
            }
            else
            {
                Debug.Log("Species=" + Species);
                Debug.Log("Level2Species=" + Level2Species);
                Debug.Log("怪獸第二階進化階段錯誤，讀取不到第二階物種進化屬性");
            }
        }
        SetProperty();//設定最終屬性
    }
    /// <summary>
    /// 品質強化一階
    /// </summary>
    public void EnhanceRare()//品質強化一階
    {
        Rare++;
        SetRareProperty();//設定品質影響屬性
    }
    void SetRareProperty()//設定品質影響屬性
    {
        RareHealthPlus = 0;
        RareBrutalPlus = 0;
        RareResistancePlus = 0;
        RareAggressivePlus = 0;
        for (int i = 1; i <= Rare; i++)
        {
            RareHealthPlus += GameDictionary.SpeciesRarePropertyDic[Species][i].BaseHealth;
            RareBrutalPlus += GameDictionary.SpeciesRarePropertyDic[Species][i].BaseBrutal;
            RareResistancePlus += GameDictionary.SpeciesRarePropertyDic[Species][i].BaseResistance;
            RareAggressivePlus += GameDictionary.SpeciesRarePropertyDic[Species][i].BaseAggressive;
        }
        SetProperty();//設定最終屬性
    }
    private void StartSetMonsterProperty()//初始化怪獸起始數值
    {
        OwnTalentList = new List<Talent>();
        OwnFeatureList = new List<Feature>();
        SetOwnFeature();//讀入怪物擁有特徵
        SetOwnTalent();//設定擁有的天賦及影響屬性
        SetRareProperty();//設定品質影響屬性
        SetSpeciesProperty();//設定物種進化影響屬性
        SetProperty();//設定最終屬性
    }
    public void SetOwnTalent()//設定擁有的天賦
    {
        OwnTalentList.Clear();//清空以學的天賦
        if (Level2Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionTalentDic[Species].ContainsKey(Level2Species))
            {
                OwnTalentList.Add(GameDictionary.SpeciesEvolutionTalentDic[Species][Level2Species]);
            }
            else
            {
                Debug.Log("Species=" + Species);
                Debug.Log("Level2Species=" + Level2Species);
                Debug.Log("怪獸第二階進化階段錯誤，讀取不到第二階物種擁有的天賦");
            }
        }
        if (Level3Species != 0)
        {
            if (GameDictionary.SpeciesEvolutionTalentDic[Species].ContainsKey(Level3Species))
            {
                OwnTalentList.Add(GameDictionary.SpeciesEvolutionTalentDic[Species][Level3Species]);
            }
            else
            {
                Debug.Log("Species=" + Species);
                Debug.Log("Level3Species=" + Level3Species);
                Debug.Log("怪獸第三階進化階段錯誤，讀取不到第三階物種擁有的天賦");
            }
        }
        SetTalentAddProperty();//設定天賦增加的屬性
        SetProperty();//設定最終屬性
    }
    void SetTalentAddProperty()
    {
        TalentHealthPlus = 0;
        TalentHealthTimes = 0;
        TalentBrutalPlus = 0;
        TalentBrutalTimes = 0;
        TalentResistancePlus = 0;
        TalentResistanceTimes = 0;
        TalentAggressivePlus = 0;
        TalentAggressiveTimes = 0;
        TalentSizePlus = 0;
        for (int i = 0; i < OwnTalentList.Count; i++)
        {
            TalentHealthPlus += OwnTalentList[i].TalentHealthPlus;
            TalentHealthTimes += OwnTalentList[i].TalentHealthTimes;
            TalentBrutalPlus += OwnTalentList[i].TalentBrutalPlus;
            TalentBrutalTimes += OwnTalentList[i].TalentBrutalTimes;
            TalentResistancePlus += OwnTalentList[i].TalentResistancePlus;
            TalentResistanceTimes += OwnTalentList[i].TalentResistanceTimes;
            TalentAggressivePlus += OwnTalentList[i].TalentAggressivePlus;
            TalentAggressiveTimes += OwnTalentList[i].TalentAggressiveTimes;
            TalentSizePlus += OwnTalentList[i].TalentSizePlus;
        }
    }
}
