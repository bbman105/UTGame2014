using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{

    public int ResourceQuality { get; set; }//生產資源價值
    public int BaseResourceQuality { get; set; }//怪獸基本產值
    public float HappyQualityTimes { get; set; }//快樂影響產值百分比
    public int FeatureQualityPlus { get; set; }//特徵影響產值加值
    public float FeatureQualityTimes { get; set; }//特徵影響產值百分比
    public int TalentQualityPlus { get; set; }//天賦影響產值加值
    public float TalentQualityTimes { get; set; }//天賦影響產值百分比
    public void StartSetMonsterResourceQuality()//起始設定怪獸基本資源
    {
        BaseResourceQuality = 100;
        UpdateResourceQuality();//更新生產資源價值，快樂度改變或進化會呼叫此方法
    }
    /// <summary>
    /// 更新生產資源價值，快樂度改變或進化會呼叫此方法
    /// </summary>
    public void UpdateResourceQuality()//更新生產資源價值，快樂度改變或進化會呼叫此方法
    {
        HappyQualityTimes = (float)Happy / 100;
        ResourceQuality = (int)((((float)BaseResourceQuality * (1 + TalentQualityTimes + FeatureQualityTimes)) + (float)(TalentQualityPlus + FeatureQualityPlus)) * HappyQualityTimes);
    }
}
