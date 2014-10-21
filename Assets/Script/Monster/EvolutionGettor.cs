using UnityEngine;
using System.Collections;

public class EvolutionGettor
{

    PlayerMonster pm;
    EvolutionLoad EvoLoader;//進化UI控制器
    int AllWeight;//權重總和
    bool CanEvolution { get; set; }//可否進化
    int Lv;//物種等級
    int CurWeight;//目前的運算權重
    int[] EveryWeight;//各自進化點數的權重
    int EvoLevelSpecies;//進化類型1=原生2階,2=突變2階級,3=狂野2階級,4=原生3階,5=突變3階級,6=狂野3階級
    int EvoType;//選擇三大類中的哪一類進化,0是原生,1是突變,2是狂野
    string EvoSpecies;//進化後的Species Key
    string[] NewSpeciesString;//進化後的物種key文字分割
    //紀錄未進化前的數值
    int PreHealth;
    int PreBrutal;
    int PreAggressive;
    int PreResistance;
    string TalentText;
    string Skilltext;
    public EvolutionGettor(PlayerMonster _pm)
    {
        this.pm = _pm;
        EveryWeight = new int[3];
        NewSpeciesString = new string[2];
        EvoLoader = new EvolutionLoad(pm);//建立進化介面控制器
    }
    public void GetEvoPhase()//設定怪獸是在第幾階的
    {
        if (pm.SpeciesLevel == 1)
        {
            pm.EvoPhase = 1;
        }
        else if (pm.SpeciesLevel > 1 && pm.SpeciesLevel < 5)
        {
            pm.EvoPhase = 2;
        }
        else if (pm.SpeciesLevel > 4 && pm.SpeciesLevel < 8)
        {
            pm.EvoPhase = 3;
        }
        else
        {
            Debug.Log("此怪物的SpeciesLevel錯誤");
        }
    }

    public bool CheckEvolution()//查看有沒有符合進化條件
    {
        AllWeight = pm.Natural + pm.Mutation + pm.Wild;//三個屬性權重總和
        if (pm.EvoPhase == 1)//如果目前為一階怪
        {
            if (pm.Lv == 10)//如果總進化點數大於20(已不使用)，且等級大於10就可進化，AllWeight >= 20已不使用
            {
                CanEvolution = true;//設為可進化
            }
        }
        else if (pm.EvoPhase == 2)//如果目前為二階怪
        {
            if (pm.Lv == 20)//如果總進化點數大於40(已不使用)，且等級大於20就可進化
            {
                CanEvolution = true;//設為可進化
            }
        }
        return CanEvolution;
    }

    public void GetEvolution()
    {
        if (CanEvolution)//如果可進化，開始選擇進化的物種
        {
            CanEvolution = false;//進化後，設為不可進化
            EvoSpecies = string.Format("{0}-{1}", pm.Species, pm.SpeciesLevel);
            EveryWeight[0] = pm.Natural;
            EveryWeight[1] = pm.Mutation;
            EveryWeight[2] = pm.Wild;
            for (int i = 0; i < EveryWeight.Length; i++)
            {
                if (GameDictionary.EvolutionTreeDic[EvoSpecies].EvoSpecies[i] == "0")//如果可進化物種ID是0代表不能進行該類型進化，所以權重設為0
                {
                    AllWeight -= EveryWeight[i];
                    EveryWeight[i] = 0;
                }
            }
            CurWeight = UnityEngine.Random.Range(0, AllWeight);
            for (int i = 0; i < EveryWeight.Length; i++)
            {
                CurWeight -= EveryWeight[i];
                if (CurWeight < 0)
                {
                    EvoType = i;
                    EvoLevelSpecies = i + 1;
                    if (pm.Lv == 20)//如果是二階升三階怪，則EvoType+3
                        EvoLevelSpecies += 3;
                    Evolution();//進化
                    break;
                }
            }
        }
    }
    private void Evolution()
    {
        if (GameDictionary.EvolutionTreeDic[EvoSpecies].EvoSpecies[EvoType] != "0")//進化後的物種如果不是0就代表可進化
        {
            try
            {
                NewSpeciesString = GameDictionary.EvolutionTreeDic[EvoSpecies].EvoSpecies[EvoType].Split('-');
                pm.Species = int.Parse(NewSpeciesString[0]);
                pm.SpeciesLevel = int.Parse(NewSpeciesString[1]);
                pm.SpeciesKey = GameDictionary.EvolutionTreeDic[EvoSpecies].EvoSpecies[EvoType];
                if (EvoLevelSpecies < 4)//如果是進化到二階
                {
                    pm.Level2Species = EvoLevelSpecies;
                    TalentText = GameDictionary.SpeciesEvolutionTalentDic[pm.Species][pm.Level2Species].TalentName;
                    for (int i = 0; i < GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level2Species].Length; i++)
                    {
                        if (i ==0)
                        {
                            Skilltext += GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level2Species][i].SkillName;
                        }
                        else
                        {
                            Skilltext += "," + GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level2Species][i].SkillName;
                        }
                    }
                }
                else//如果是進化到三階
                {
                    pm.Level3Species = EvoLevelSpecies;
                    TalentText = GameDictionary.SpeciesEvolutionTalentDic[pm.Species][pm.Level3Species].TalentName;
                    for (int i = 0; i < GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level3Species].Length; i++)
                    {
                        if (i == 0)
                        {
                            Skilltext += GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level3Species][i].SkillName;
                        }
                        else
                        {
                            Skilltext += "，" + GameDictionary.SpeciesEvolutionSkillDic[pm.Species][pm.Level3Species][i].SkillName;
                        }
                    }
                }
            }
            catch
            {
                Debug.Log("進化樹字典中的進化怪獸ID格式不正確，需為 Species-SpeciesLevel");
            }
            //紀錄未進化前的數值
            PreHealth = pm.Health;
            PreBrutal = pm.Brutal;
            PreAggressive = pm.Aggressive;
            PreResistance = pm.Resistance;

            UpdateDate();
            EvoLoader.ShowEvolution(PreHealth, PreBrutal, PreAggressive, PreResistance, TalentText, Skilltext);
            pm.SetOwnTalent();//更新擁有的天賦並更新影響屬性
        }
    }
    private void UpdateDate()
    {
        pm.SetSpeciesProperty();//更新物種進化屬性
        pm.SetSprite();//更新怪物圖像
        pm.SetSize(pm.Size);//更新體型大小
        pm.SetOwnSkill();//更新怪物擁有技能
        GetEvoPhase();//更新怪物階級
    }
}
