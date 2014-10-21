﻿using UnityEngine;
using System.Collections;

public class PlayerResource
{

    private int gold;
    public int Gold
    {
        get { return gold; }
        private set
        {
            if (value < 0)
                gold = 0;
            else
                gold = value;
        }
    }

    private int curEnergy;
    public int CurEnergy
    {
        get { return curEnergy; }
        set
        {
            if (value < 0)
                curEnergy = 0;
            else if (value > MaxEnergy)
                curEnergy = MaxEnergy;
            else
                curEnergy = value;
        }
    }
    public int MaxEnergy { get; set; }
    public int Dimand { get; set; }
    //怪物強化素材
    //火元素素材
    private int fireSilver;
    public int FireSilver
    {
        get { return fireSilver; }
        private set
        {
            if (value < 0)
                fireSilver = 0;
            else
                fireSilver = value;
        }
    }
    private int fireGold;
    public int FireGold
    {
        get { return fireGold; }
        private set
        {
            if (value < 0)
                fireGold = 0;
            else
                fireGold = value;
        }
    }
    private int fireDimand;
    public int FireDimand
    {
        get { return fireDimand; }
        private set
        {
            if (value < 0)
                fireDimand = 0;
            else
                fireDimand = value;
        }
    }
    private int fireCrystal;
    public int FireCrystal
    {
        get { return fireCrystal; }
        private set
        {
            if (value < 0)
                fireCrystal = 0;
            else
                fireCrystal = value;
        }
    }
    //木元素素材
    private int woodSilver;
    public int WoodSilver
    {
        get { return woodSilver; }
        private set
        {
            if (value < 0)
                woodSilver = 0;
            else
                woodSilver = value;
        }
    }
    private int woodGold;
    public int WoodGold
    {
        get { return woodGold; }
        private set
        {
            if (value < 0)
                woodGold = 0;
            else
                woodGold = value;
        }
    }
    private int woodDimand;
    public int WoodDimand
    {
        get { return woodDimand; }
        private set
        {
            if (value < 0)
                woodDimand = 0;
            else
                woodDimand = value;
        }
    }
    private int woodCrystal;
    public int WoodCrystal
    {
        get { return woodCrystal; }
        private set
        {
            if (value < 0)
                woodCrystal = 0;
            else
                woodCrystal = value;
        }
    }
    //水元素素材
    private int waterSilver;
    public int WaterSilver
    {
        get { return waterSilver; }
        private set
        {
            if (value < 0)
                waterSilver = 0;
            else
                waterSilver = value;
        }
    }
    private int waterGold;
    public int WaterGold
    {
        get { return waterGold; }
        private set
        {
            if (value < 0)
                waterGold = 0;
            else
                waterGold = value;
        }
    }
    private int waterDimand;
    public int WaterDimand
    {
        get { return waterDimand; }
        private set
        {
            if (value < 0)
                waterDimand = 0;
            else
                waterDimand = value;
        }
    }
    private int waterCrystal;
    public int WaterCrystal
    {
        get { return waterCrystal; }
        private set
        {
            if (value < 0)
                waterCrystal = 0;
            else
                waterCrystal = value;
        }
    }
    /// <summary>
    ///增加資源功能：<para></para>
    ///resourceType為資源種類，當resourceType為0時增加金幣、1時增加目前能量、2時增加最大能量、3時增加鑽石<para></para>
    ///resourceNum為增加的量，填100就是增加100，填-100就是減少100
    /// </summary>
    public void AddResource(byte _resourceType, int _resourceNum)
    {
        switch (_resourceType)
        {
            case 0:
                AddGold(_resourceNum);
                break;
            case 1:
                AddEnergy(_resourceNum);
                break;
            case 2:
                AddMaxEnergy(_resourceNum);
                break;
            case 3:
                AddDimand(_resourceNum);
                break;
            default:
                Debug.Log("增加的資源種類傳入錯誤的類型");
                break;
        }
        ResourceUI.UpdateResourceUI();
    }
    /// <summary>
    ///設定資源功能：<para></para>
    ///resourceType為資源種類，當resourceType為0時設定金幣、1時設定目前能量、2時設定最大能量、3時設定鑽石<para></para>
    ///resourceNum為設定的量，填100就是設定為100
    /// </summary>
    public void SetResource(byte _resourceType, int _resourceNum)
    {
        switch (_resourceType)
        {
            case 0:
                SetGold(_resourceNum);
                break;
            case 1:
                SetEnergy(_resourceNum);
                break;
            case 2:
                SetMaxEnergy(_resourceNum);
                break;
            case 3:
                SetDimand(_resourceNum);
                break;
            default:
                Debug.Log("設定數量的資源種類傳入錯誤的類型");
                break;
        }
        ResourceUI.UpdateResourceUI();
    }
    /// <summary>
    /// 增加金幣
    /// </summary>
    void AddGold(int _gold)
    {
        Gold += _gold;
    }
    /// <summary>
    /// 設定金幣
    /// </summary>
    void SetGold(int _gold)
    {
        Gold = _gold;
    }
    /// <summary>
    /// 增加能量
    /// </summary>
    void AddEnergy(int _energy)
    {
        CurEnergy += _energy;
    }
    /// <summary>
    /// 設定能量
    /// </summary>
    void SetEnergy(int _energy)
    {
        CurEnergy = _energy;
    }
    /// <summary>
    /// 增加鑽石
    /// </summary>
    void AddDimand(int _dimand)
    {
        Dimand += _dimand;
    }
    /// <summary>
    /// 設定鑽石
    /// </summary>
    void SetDimand(int _dimand)
    {
        Dimand = _dimand;
    }
    /// <summary>
    /// 增加最大能量
    /// </summary>
    void AddMaxEnergy(int _maxEnergy)
    {
        MaxEnergy += _maxEnergy;
    }
    /// <summary>
    /// 設定最大能量
    /// </summary>
    void SetMaxEnergy(int _maxEnergy)
    {
        MaxEnergy = _maxEnergy;
    }

    /// <summary>
    /// 儲存資料到client
    /// </summary>
    void SaveEnhanceDateToClient(int _type)//儲存資料到client
    {
        switch (_type)
        {
            case 1://火銀
                PlayerPrefs.SetInt("FireSilver", FireSilver);
                break;
            case 2://火金
                PlayerPrefs.SetInt("FireGold", FireGold);
                break;
            case 3://火鑽
                PlayerPrefs.SetInt("FireDimand", FireDimand);
                break;
            case 4://火晶
                PlayerPrefs.SetInt("FireCrystal", FireCrystal);
                break;
            case 11://木銀
                PlayerPrefs.SetInt("WoodSilver", WoodSilver);
                break;
            case 12://木金
                PlayerPrefs.SetInt("WoodGold", WoodGold);
                break;
            case 13://木鑽
                PlayerPrefs.SetInt("WoodDimand", WoodDimand);
                break;
            case 14://木晶
                PlayerPrefs.SetInt("WoodCrystal", WoodCrystal);
                break;
            case 21://水銀
                PlayerPrefs.SetInt("WaterSilver", WaterSilver);
                break;
            case 22://水金
                PlayerPrefs.SetInt("WaterGold", WaterGold);
                break;
            case 23://水鑽
                PlayerPrefs.SetInt("WaterDimand", WaterDimand);
                break;
            case 24://水晶
                PlayerPrefs.SetInt("WaterCrystal", WaterCrystal);
                break;
            default:
                Debug.LogError("傳入的怪獸進化素材種類錯誤");
                break;
        }
    }
    /// <summary>
    /// 增加怪獸強化素材，傳入1~4為火，11~14為木，21~24為水
    /// </summary>
    public void AddEnhanceMaterial(int _type, int _addNum)
    {
        try
        {
            switch (_type)
            {
                case 1://火銀
                    FireSilver += _addNum;
                    break;
                case 2://火金
                    FireGold += _addNum;
                    break;
                case 3://火鑽
                    FireDimand += _addNum;
                    break;
                case 4://火晶
                    FireCrystal += _addNum;
                    break;
                case 11://木銀
                    WoodSilver += _addNum;
                    break;
                case 12://木金
                    WoodGold += _addNum;
                    break;
                case 13://木鑽
                    WoodDimand += _addNum;
                    break;
                case 14://木晶
                    WoodCrystal += _addNum;
                    break;
                case 21://水銀
                    WaterSilver += _addNum;
                    break;
                case 22://水金
                    WaterGold += _addNum;
                    break;
                case 23://水鑽
                    WaterDimand += _addNum;
                    break;
                case 24://水晶
                    WaterCrystal += _addNum;
                    break;
                default:
                    Debug.LogError("傳入的怪獸進化素材種類錯誤");
                    break;
            }
        }
        catch
        {
            Debug.LogError("設定怪獸強化元素時發生錯誤");
        }
        SaveEnhanceDateToClient(_type);//儲存資料到client
    }
    /// <summary>
    /// 設定怪獸強化素材，傳入1~4為火，11~14為木，21~24為水
    /// </summary>
    public void SetEnhanceMaterial(byte _type, int _setNum)
    {
        try
        {
            switch (_type)
            {
                case 1://火銀
                    FireSilver = _setNum;
                    break;
                case 2://火金
                    FireGold = _setNum;
                    break;
                case 3://火鑽
                    FireDimand = _setNum;
                    break;
                case 4://火晶
                    FireCrystal = _setNum;
                    break;
                case 11://木銀
                    WoodSilver = _setNum;
                    break;
                case 12://木金
                    WoodGold = _setNum;
                    break;
                case 13://木鑽
                    WoodDimand = _setNum;
                    break;
                case 14://木晶
                    WoodCrystal = _setNum;
                    break;
                case 21://水銀
                    WaterSilver = _setNum;
                    break;
                case 22://水金
                    WaterGold = _setNum;
                    break;
                case 23://水鑽
                    WaterDimand = _setNum;
                    break;
                case 24://水晶
                    WaterCrystal = _setNum;
                    break;
                default:
                    Debug.LogError("傳入的怪獸進化素材種類錯誤");
                    break;
            }
        }
        catch
        {
            Debug.LogError("設定怪獸強化元素時發生錯誤");
        }
        SaveEnhanceDateToClient(_type);//儲存資料到client
    }
    /// <summary>
    /// 傳入素材種類，取得玩家擁有的怪獸強化素材數量，傳入1~4為火，11~14為木，21~24為水
    /// </summary>
    public int GetEnhanceMaterial(int _type)
    {
        int materialNum = 0;
        try
        {
            switch (_type)
            {
                case 1://火銀
                    materialNum = FireSilver;
                    break;
                case 2://火金
                    materialNum = FireGold;
                    break;
                case 3://火鑽
                    materialNum = FireDimand;
                    break;
                case 4://火晶
                    materialNum = FireCrystal;
                    break;
                case 11://木銀
                    materialNum = WoodSilver;
                    break;
                case 12://木金
                    materialNum = WoodGold;
                    break;
                case 13://木鑽
                    materialNum = WoodDimand;
                    break;
                case 14://木晶
                    materialNum = WoodCrystal;
                    break;
                case 21://水銀
                    materialNum = WaterSilver;
                    break;
                case 22://水金
                    materialNum = WaterGold;
                    break;
                case 23://水鑽
                    materialNum = WaterDimand;
                    break;
                case 24://水晶
                    materialNum = WaterCrystal;
                    break;
                default:
                    Debug.LogError("傳入的怪獸進化素材種類錯誤");
                    break;
            }
        }
        catch
        {
            Debug.LogError("設定怪獸強化元素時發生錯誤");
        }
        return materialNum;
    }
}