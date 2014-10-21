using UnityEngine;
using System.Collections;

public class ForceStatus
{
    private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private float curHealth;
    public float CurHealth
    {
        get { return curHealth; }
        set { curHealth = value; }
    }
    private float curHealthPercent;
    public float CurHealthPercent
    {
        get { return curHealthPercent; }
        set { curHealthPercent = value; }
    }
    private int curCharaNum;
    public int CurCharaNum
    {
        get { return curCharaNum; }
        set { curCharaNum = value; }
    }
    private int maxCharaNum;
    public int MaxCharaNum
    {
        get { return maxCharaNum; }
        set { maxCharaNum = value; }
    }
    private int deathCharaNum;
    public int DeathCharaNum
    {
        get { return deathCharaNum; }
        set { deathCharaNum = value; }
    }
    private int hit;
    public int Hit
    {
        get { return hit; }
        set { hit = value; }
    }
    private int elementHit;
    public int ElementHit
    {
        get { return elementHit; }
        set { elementHit = value; }
    }
    int comboHit;//連級數
    public int ComboHit
    {
        get { return comboHit; }
        set { comboHit = value; }
    }
    private float elementHitRate;
    public float ElementHitRate
    {
        get { return elementHitRate; }
        set { elementHitRate = value; }
    }
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    private int aliveNum;
    public int AliveNum
    {
        get { return aliveNum; }
        set { aliveNum = value; }
    }
    private int deathNu;
    public int DeathNu
    {
        get { return deathNu; }
        set { deathNu = value; }
    }
    public void DoElementHit()//元素報及
    {
        AddHitNum();//撞擊次數增加
        AddElementHitNum();//元素爆擊次數增加
        AddComboHitNum();//連擊次數增加
    }
    public void DoNormalHit()//一般撞擊
    {
        AddHitNum();//撞擊次數增加
    }
    public void DoBeElementHit()//被元素報及
    {
        AddHitNum();//撞擊次數增加
        ClearComboHitNum();//清空連擊
    }
    void AddHitNum()
    {
        Hit++;
    }
    void AddElementHitNum()
    {
        ElementHit++;
    }
    void AddComboHitNum()
    {
        ComboHit++;
    }
    void ClearComboHitNum()
    {
        ComboHit = 0;
    }

}
