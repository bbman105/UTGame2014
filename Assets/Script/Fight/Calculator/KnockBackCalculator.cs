using UnityEngine;
using System.Collections;

public class KnockBackCalculator : Calculator
{
    protected const int BaseKnockBackStrength = 50;
    protected const int MaxAggressiveStrength = 100;
    protected const int OnElementHitStrength = 160;
    protected const int BaseKnockUpStrength = 300;
    protected const int SmallSizeReduceStrength = -75;
    protected const int HugeSizeReduceStrength = -150;
    protected const int KnockWallBack = 200;
    int targetAggressive;
    public int TargetAggressive
    {
        get { return targetAggressive; }
        set { targetAggressive = value; }
    }
    int selfAggressive;
    public int SelfAggressive
    {
        get { return selfAggressive; }
        set { selfAggressive = value; }
    }

    int aggressivePlus;
    public int AggressivePlus
    {
        get { return aggressivePlus; }
        set { aggressivePlus = value; }
    }
    int elementPlus;
    public int ElementPlus
    {
        get { return elementPlus; }
        set { elementPlus = value; }
    }
    int sizePlus;
    public int SizePlus
    {
        get { return sizePlus; }
        set { sizePlus = value; }
    }
    int knockBackStrength;
    public int KnockBackStrength
    {
        get { return knockBackStrength; }
        set { knockBackStrength = value; }
    }
    int knockUpStrength;
    public int KnockUpStrength
    {
        get { return knockUpStrength; }
        set { knockUpStrength = value; }
    }
    //基本被擊飛力道倍率
    const float BaseBeKnockStrengthTimes = 1;
    //取得向上擊退力道
    public int GetKnockUpStrength(int _dominantSize)
    {
        SizePlus = 0;
        //是否有體型優勢
        if (_dominantSize == 1)
        {
            SizePlus = SmallSizeReduceStrength;
        }
        else if (_dominantSize == 2)
        {
            SizePlus = HugeSizeReduceStrength;
        }
        KnockUpStrength = BaseKnockUpStrength + SizePlus;
        return KnockUpStrength;
    }
    //取得撞牆向後擊退力道
    public int GetKnockWallBackStrength()
    {
        return KnockWallBack;
    }
    //取得向後擊退力道
    public int GetKnockBackStrength(
        int _selfAggressive, float _selfAggressiveTimes, int _selfAggressivePlus, int _beKnockStrengthPlus,
        int _targetAggressive, float _targetAggressiveTimes, int _targetAggressivePlus, int _targetKnockStrengthPlus,
        bool _onElementHit
        )
    {
        //初始化額外擊退加值
        AggressivePlus = 0;
        ElementPlus = 0;
        //加上狀態後的侵略值
        SelfAggressive = (int)(_selfAggressiveTimes * _selfAggressive) + _selfAggressivePlus;
        TargetAggressive = (int)(_targetAggressiveTimes * _targetAggressive) + _targetAggressivePlus;
        //比較雙方侵略值，判斷侵略額外擊退距離
        AggressivePlus = (int)(MaxAggressiveStrength * ((float)(TargetAggressive) / (float)(SelfAggressive + TargetAggressive)));
        //是否有元素相剋
        if (_onElementHit)
            ElementPlus = OnElementHitStrength;
        //計算最終擊退力道
        KnockBackStrength = (int)((BaseBeKnockStrengthTimes) * (BaseKnockBackStrength + AggressivePlus + ElementPlus + _targetKnockStrengthPlus + _beKnockStrengthPlus));
        /*
        Debug.Log("ResistancePlus=" + ResistancePlus);
        Debug.Log("ElementPlus=" + ElementPlus);
        Debug.Log("SizePlus=" + SizePlus);
        Debug.Log("KnockStrength=" + KnockStrength);
        */
        return KnockBackStrength;
    }


}
