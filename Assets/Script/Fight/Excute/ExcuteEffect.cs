using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class ExcuteEffect : MonoBehaviour
{
    public int EffectID { get; set; }
    bool isBuff;
    public bool IsBuff//是否為buff
    {
        get { return isBuff; }
        set { isBuff = value; }
    }
    bool isDebuff;
    public bool IsDebuff//是否為Debuff
    {
        get { return isDebuff; }
        set { isDebuff = value; }
    }
    //狀態種類0=一般,1=持續傷害,2=虛弱,3=緩速,4=暈眩,5=恐懼,6=束縛,7=定身
    private int effectType;
    public int EffectType
    {
        get { return effectType; }
        set { effectType = value; }
    }
    bool preATK;
    public bool PreATK//是否為前置效果
    {
        get { return preATK; }
        set { preATK = value; }
    }
    int effectClassID;
    public int EffectClassID//效果類型ID
    {
        get { return effectClassID; }
        set { effectClassID = value; }
    }
    int effectClassLevel;
    public int EffectClassLevel//效果等級
    {
        get { return effectClassLevel; }
        set { effectClassLevel = value; }
    }
    //常駐狀態法術
    private bool isPermanent;
    public bool IsPermanent
    {
        get { return isPermanent; }
        set { isPermanent = value; }
    }
    float duringTime;
    public float DuringTime//持續時間
    {
        get { return duringTime; }
        set { duringTime = value; }
    }
    float circleTime;
    public float CircleTime//觸發週期
    {
        get { return circleTime; }
        set { circleTime = value; }
    }
    float circleTimer;
    public float CircleTimer//觸發週期計時
    {
        get { return circleTimer; }
        set { circleTimer = value; }
    }
    bool firstTrigger;
    public bool FirstTrigger//初始是否觸發
    {
        get { return firstTrigger; }
        set { firstTrigger = value; }
    }
    Damage TriggerDamage { get; set; }//觸發的傷害效果
    ExcuteDamage selfDamage;
    bool haveCircleDamage;
    Cure TriggerCure { get; set; }//觸發的治癒效果
    ExcuteCure selfCure;
    bool haveCircleCure;
    StatusEffect TriggerEffect { get; set; }//觸發的狀態效果
    ExcuteEffect slefEffect;
    bool haveCircleStatusEffect;
    int TriggerSelfProbabilityRnd;//觸發自身效果的機率
    bool haveCircleEffect;//是否有時間內觸發效果
    float DeltTime;

    int damagePlus;
    public int DamagePlus//傷害加值改變
    {
        get { return damagePlus; }
        set { damagePlus = value; }
    }
    float damageTimes;
    public float DamageTimes//傷害倍率改變
    {
        get { return damageTimes; }
        set { damageTimes = value; }
    }
    int defensePlus;
    protected int DefensePlus//防禦加值改變
    {
        get { return defensePlus; }
        set { defensePlus = value; }
    }
    float defenseTimes;
    protected float DefenseTimes//防禦倍率改變
    {
        get { return defenseTimes; }
        set
        { defenseTimes = value; }
    }
    int speedPlus;
    public int SpeedPlus//速度加值改變
    {
        get { return speedPlus; }
        set { speedPlus = value; }
    }
    float speedTimes;
    public float SpeedTimes//速度倍率改變
    {
        get { return speedTimes; }
        set { speedTimes = value; }
    }
    int brutalPlus;
    public int BrutalPlus//殘暴加值改變
    {
        get { return brutalPlus; }
        set { brutalPlus = value; }
    }
    float brutalTimes;
    public float BrutalTimes//殘暴倍率改變
    {
        get { return brutalTimes; }
        set { brutalTimes = value; }
    }
    int aggressivePlus;
    public int AggressivePlus//侵略加值改變
    {
        get { return aggressivePlus; }
        set { aggressivePlus = value; }
    }
    float aggressiveTimes;
    public float AggressiveTimes//侵略倍率改變
    {
        get { return aggressiveTimes; }
        set { aggressiveTimes = value; }
    }
    int resistancePlus;
    public int ResistancePlus//抵抗加值改變
    {
        get { return resistancePlus; }
        set { resistancePlus = value; }
    }
    float resistanceTimes;
    public float ResistanceTimes//抵抗倍率改變
    {
        get { return resistanceTimes; }
        set { resistanceTimes = value; }
    }
    //傷害減免值
    private int reduceDamagePlus;
    public int ReduceDamagePlus
    {
        get { return reduceDamagePlus; }
        set
        {
            if (value < 0)
                reduceDamagePlus = 0;
            else
                reduceDamagePlus = value;
        }
    }
    //傷害減免率
    private float reduceDamageTimes;
    public float ReduceDamageTimes
    {
        get { return reduceDamageTimes; }
        set
        {
            if (value < 0)
                ReduceDamageTimes = 0;
            else if (value > 1)
                ReduceDamageTimes = 1;
            else
                reduceDamageTimes = value;
        }
    }
    //逼牆傷害加值改變
    private int knockWallDMGPlus;
    public int KnockWallDMGPlus
    {
        get { return knockWallDMGPlus; }
        set { knockWallDMGPlus = value; }
    }
    //逼牆傷害倍率改變
    private float knockWallDMGTimes;
    public float KnockWallDMGTimes
    {
        get { return knockWallDMGTimes; }
        set { knockWallDMGTimes = value; }
    }
    //被逼牆傷害加值改變
    private int beKnockWallDMGPlus;
    public int BeKnockWallDMGPlus
    {
        get { return beKnockWallDMGPlus; }
        set { beKnockWallDMGPlus = value; }
    }
    //被逼牆傷害倍率改變
    private float beKnockWallDMGTimes;
    public float BeKnockWallDMGTimes
    {
        get { return beKnockWallDMGTimes; }
        set { beKnockWallDMGTimes = value; }
    }
    //逼牆暈眩加值改變
    private float knockWallStunPlus;
    public float KnockWallStunPlus
    {
        get { return knockWallStunPlus; }
        set { knockWallStunPlus = value; }
    }
    //被逼牆暈眩加值改變
    private float beKnockWallStunPlus;
    public float BeKnockWallStunPlus
    {
        get { return beKnockWallStunPlus; }
        set { beKnockWallStunPlus = value; }
    }
    //被擊退力道加值改變
    private int beKnockStrengthPlus;
    public int BeKnockStrengthPlus
    {
        get { return beKnockStrengthPlus; }
        set { beKnockStrengthPlus = value; }
    }

    //擊退力道加值改變
    private int knockStrengthPlus;
    public int KnockStrengthPlus
    {
        get { return knockStrengthPlus; }
        set { knockStrengthPlus = value; }
    }

    //重擊傷害加值改變
    private int criticalDMGPlus;
    public int CriticalDMGPlus
    {
        get { return criticalDMGPlus; }
        set { criticalDMGPlus = value; }
    }
    //重擊傷害倍率改變
    private float criticalDMGTimes;
    public float CriticalDMGTimes
    {
        get { return criticalDMGTimes; }
        set { criticalDMGTimes = value; }
    }
    //受到重擊傷害加值改變
    private int beCriticalDMGPlus;
    public int BeCriticalDMGPlus
    {
        get { return beCriticalDMGPlus; }
        set { beCriticalDMGPlus = value; }
    }
    //受到重擊傷害倍率改變
    private float beCriticalDMGTimes;
    public float BeCriticalDMGTimes
    {
        get { return beCriticalDMGTimes; }
        set { beCriticalDMGTimes = value; }
    }
    //擊飛定身時間加值改變
    private float knockNailTimePlus;
    public float KnockNailTimePlus
    {
        get { return knockNailTimePlus; }
        set { knockNailTimePlus = value; }
    }
    //被擊飛定身時間加值改變
    private float beKnockNailTimePlus;
    public float BeKnockNailTimePlus
    {
        get { return beKnockNailTimePlus; }
        set { beKnockNailTimePlus = value; }
    }
    //免疫的效果類型
    private int immuneType;
    public int ImmuneType
    {
        get { return immuneType; }
        set { immuneType = value; }
    }
    //詛咒
    private bool isCurse;
    public bool IsCurse
    {
        get { return isCurse; }
        set { isCurse = value; }
    }
    //祝福
    private bool isBlessing;
    public bool IsBlessing
    {
        get { return isBlessing; }
        set { isBlessing = value; }
    }
    bool isStun;
    public bool IsStun//是否暈眩
    {
        get { return isStun; }
        set { isStun = value; }
    }
    bool isFear;
    public bool IsFear//是否恐懼
    {
        get { return isFear; }
        set { isFear = value; }
    }
    bool isSilence;
    public bool IsSilence//是否沉默
    {
        get { return isSilence; }
        set { isSilence = value; }
    }
    bool isFetter;
    public bool IsFetter//是否束縛
    {
        get { return isFetter; }
        set { isFetter = value; }
    }
    bool isnail;
    public bool IsNail//是否定身
    {
        get { return isnail; }
        set { isnail = value; }
    }
    bool isRigid;
    public bool IsRigid//是否剛體
    {
        get { return isRigid; }
        set { isRigid = value; }
    }
    bool isInvincible;
    public bool IsInvincible//是否無敵
    {
        get { return isInvincible; }
        set { isInvincible = value; }
    }
    int effectAniID;
    public int EffectAniID//特效ID
    {
        get { return effectAniID; }
        set { effectAniID = value; }
    }
    int effectAniUnqueID;
    Damage KnockSelfDamage { get; set; }//碰撞觸發自身的傷害效果
    Cure KnockSelfCure { get; set; }//碰撞觸發自身的治癒效果
    StatusEffect KnockSelfEffect { get; set; }//碰撞觸發自身的狀態效果
    Damage KnockTargetDamage { get; set; }//碰撞觸發目標的傷害效果
    Cure KnockTargetCure { get; set; }//碰撞觸發目標的治癒效果
    StatusEffect KnockTargetEffect { get; set; }//碰撞觸發目標的狀態效果
    StatusEffect BeKnockWallTriggerEffect { get; set; }//被逼牆觸發狀態效果
    Damage BeKnockWallTriggerDamage { get; set; }//被逼牆觸發傷害
    Cure BeKnockWallTriggerCure { get; set; }//被逼牆觸發治癒
    bool stackable;
    public bool Stackable//是否可堆疊
    {
        get { return stackable; }
        set { stackable = value; }
    }
    int maxStackTimes;
    public int MaxStackTimes//可堆疊的最大次數
    {
        get { return maxStackTimes; }
        set { maxStackTimes = value; }
    }
    bool resetDuringTime;
    public bool ResetDuringTime//堆疊時是否刷新持續時間
    {
        get { return resetDuringTime; }
        set { resetDuringTime = value; }
    }


    Status Attacker { get; set; }
    Status Receiver { get; set; }
    StatusEffect EStatusEffect { get; set; }

    float DuringTimer { get; set; }
    public bool IsWork { get; set; }//為true才會觸發效果
    int ProbabilityRnd;//觸發機率骰數

    public void SetEffect(Status _attacker, Status _receiver, StatusEffect _statusEffect)//設定效果
    {
        ProbabilityRnd = UnityEngine.Random.Range(1, 101);
        if (ProbabilityRnd <= _statusEffect.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
        {
            Attacker = _attacker;
            Receiver = _receiver;
            EStatusEffect = _statusEffect;
            EffectID = _statusEffect.EffectID;
            EffectType = _statusEffect.EffectType;
            ImmuneType = _statusEffect.ImmuneType;
            IsBuff = EStatusEffect.IsBuff;
            IsDebuff = EStatusEffect.IsDebuff;
            EffectClassID = EStatusEffect.EffectClassID;
            EffectClassLevel = EStatusEffect.EffectClassLevel;
            if (EStatusEffect.CircleTime != 0)//觸發週期時間不等於0才會觸發
            {
                haveCircleEffect = true;
                CircleTime = EStatusEffect.CircleTime;
                FirstTrigger = EStatusEffect.FirstTrigger;
                if (EStatusEffect.TriggerDamage != 0)
                {
                    if (EStatusEffect.TargetIsSelf)
                        TriggerDamage = new Damage(EStatusEffect.TriggerDamage, true);
                    else
                        TriggerDamage = new Damage(EStatusEffect.TriggerDamage, false);
                    selfDamage = new ExcuteDamage();
                    haveCircleDamage = true;
                }
                if (EStatusEffect.TriggerCure != 0)
                {
                    if (EStatusEffect.TargetIsSelf)
                        TriggerCure = new Cure(EStatusEffect.TriggerCure, true);
                    else
                        TriggerCure = new Cure(EStatusEffect.TriggerCure, false);
                    selfCure = new ExcuteCure();
                    selfCure.SetCure(Attacker, Receiver, TriggerCure);//需確保當下就設置好Cure，因為依造成的傷害恢復血量需抓當下的數值
                    haveCircleCure = true;
                }
                if (EStatusEffect.TriggerEffect != 0)
                {
                    if (EStatusEffect.TargetIsSelf)
                        TriggerEffect = new StatusEffect(EStatusEffect.TriggerEffect, true);
                    else
                        TriggerEffect = new StatusEffect(EStatusEffect.TriggerEffect, false);
                    haveCircleStatusEffect = true;
                }
            }

            IsPermanent = EStatusEffect.IsPermanent;
            if (!IsPermanent)//是否為常駐狀態，若否設置持續時間，若是則設定持續時間>0確保法術狀態會執行
                DuringTime = EStatusEffect.DuringTime;
            else
                DuringTime = 1;
            DamagePlus = EStatusEffect.DamagePlus;
            DamageTimes = EStatusEffect.DamageTimes;
            DefensePlus = EStatusEffect.DefensePlus;
            DefenseTimes = EStatusEffect.DefenseTimes;
            BrutalPlus = EStatusEffect.BrutalPlus;
            BrutalTimes = EStatusEffect.BrutalTimes;
            AggressivePlus = EStatusEffect.AggressivePlus;
            AggressiveTimes = EStatusEffect.AggressiveTimes;
            ResistancePlus = EStatusEffect.ResistancePlus;
            ResistanceTimes = EStatusEffect.ResistanceTimes;
            SpeedPlus = EStatusEffect.SpeedPlus;
            SpeedTimes = EStatusEffect.SpeedTimes;
            ReduceDamagePlus = EStatusEffect.ReduceDamagePlus;
            ReduceDamageTimes = EStatusEffect.ReduceDamageTimes;
            KnockWallDMGPlus = EStatusEffect.KnockWallDMGPlus;
            KnockWallDMGTimes = EStatusEffect.KnockWallDMGTimes;
            BeKnockWallDMGPlus = EStatusEffect.BeKnockWallDMGPlus;
            BeKnockWallDMGTimes = EStatusEffect.BeKnockWallDMGTimes;
            KnockWallStunPlus = EStatusEffect.KnockWallStunPlus;
            BeKnockWallStunPlus = EStatusEffect.BeKnockWallStunPlus;
            KnockStrengthPlus = EStatusEffect.KnockStrengthPlus;
            BeKnockStrengthPlus = EStatusEffect.BeKnockStrengthPlus;
            KnockNailTimePlus = EStatusEffect.KnockNailTimePlus;
            BeKnockNailTimePlus = EStatusEffect.BeKnockNailTimePlus;
            CriticalDMGPlus = EStatusEffect.CriticalDMGPlus;
            CriticalDMGTimes = EStatusEffect.CriticalDMGTimes;
            BeCriticalDMGPlus = EStatusEffect.BeCriticalDMGPlus;
            BeCriticalDMGTimes = EStatusEffect.BeCriticalDMGTimes;
            IsCurse = EStatusEffect.IsCurse;
            IsBlessing = EStatusEffect.IsBlessing;
            IsStun = EStatusEffect.IsStun;
            IsFear = EStatusEffect.IsFear;
            IsSilence = EStatusEffect.IsSilence;
            IsFetter = EStatusEffect.IsFetter;
            IsNail = EStatusEffect.IsNail;
            IsRigid = EStatusEffect.IsRigid;
            IsInvincible = EStatusEffect.IsInvincible;
            EffectAniID = EStatusEffect.EffectAni;
            //碰撞觸發效果
            if (EStatusEffect.KnockTriggerSelfDamage != 0)
                KnockSelfDamage = new Damage(EStatusEffect.KnockTriggerSelfDamage, true);
            if (EStatusEffect.KnockTriggerSelfCure != 0)
                KnockSelfCure = new Cure(EStatusEffect.KnockTriggerSelfCure, true);
            if (EStatusEffect.KnockTriggerSelfEffect != 0)
                KnockSelfEffect = new StatusEffect(EStatusEffect.KnockTriggerSelfEffect, true);
            if (EStatusEffect.KnockTriggerTargetDamage != 0)
                KnockTargetDamage = new Damage(EStatusEffect.KnockTriggerTargetDamage, false);
            if (EStatusEffect.KnockTriggerTargetCure != 0)
                KnockTargetCure = new Cure(EStatusEffect.KnockTriggerTargetCure, false);
            if (EStatusEffect.KnockTriggerTargetEffect != 0)
                KnockTargetEffect = new StatusEffect(EStatusEffect.KnockTriggerTargetEffect, false);
            //被逼牆觸發效果
            if (EStatusEffect.BeKnockWallTriggerSelfEffect != 0)
                BeKnockWallTriggerEffect = new StatusEffect(EStatusEffect.BeKnockWallTriggerSelfEffect, true);
            if (EStatusEffect.BeKnockWallTriggerSelfDamage != 0)
                BeKnockWallTriggerDamage = new Damage(EStatusEffect.BeKnockWallTriggerSelfDamage, true);
            if (EStatusEffect.BeKnockWallTriggerSelfCure != 0)
                BeKnockWallTriggerCure = new Cure(EStatusEffect.BeKnockWallTriggerSelfCure, true);
            Stackable = EStatusEffect.Stackable;
            MaxStackTimes = EStatusEffect.MaxStackTimes;
            ResetDuringTime = EStatusEffect.ResetDuringTime;
            IsWork = true;
            if (IsWork)
                ImmuneBuffCheck();//是否對Buff免疫
            if (IsWork)
                ImmuneDeBuffCheck();//是否對Debfu免疫
            if (IsWork)
                ImmuneTypeCheck();//是否對此狀態類型免疫檢查
            if (IsWork)
                ClassIDCheck();
            if (IsWork)
                Excute();//執行效果
            //ViewDebugLog();//查看DebugLog
        }
        else
        {
            IsWork = false;//設定為不可執行
            Remover.Destroy(this);//機率沒過，則直接移除此效果，不透過EndEffect
        }
    }
    private void ImmuneBuffCheck()//是否對Buff免疫
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (IsBuff && Attacker.IsCurse)//如果此狀態為buff且目標有詛咒狀態則設不執行效果
            {
                if (EffectType != 8)//此狀態類型如果是8(Immortal)則不取消執行
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
        else
        {
            if (IsBuff && Receiver.IsCurse)//如果此狀態為buff且目標有詛咒狀態則設不執行效果
            {
                if (EffectType != 8)//此狀態類型如果是8(Immortal)則不取消執行
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
    }
    private void ImmuneDeBuffCheck()//是否對Debfu免疫
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (IsDebuff && Attacker.IsBlessing)//如果此狀態為Debuff且目標有祝福狀態則設不執行效果
            {
                if (EffectType != 8)//此狀態類型如果是8(Immortal)則不取消執行
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
        else
        {
            if (IsDebuff && Receiver.IsBlessing)//如果此狀態為Debuff且目標有祝福狀態則設不執行效果
            {
                if (EffectType != 8)//此狀態類型如果是8(Immortal)則不取消執行
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
    }
    private void ImmuneTypeCheck()//是否對此狀態類型免疫檢查
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (EffectType != 0 && Attacker.ImmuneTypeList.Contains(EffectType))//如果此狀態類型不為0(不分類)，且目標擁有免疫的類型與此狀態類型相同則進行免疫
            {
                IsWork = false;//設定為不可執行
                Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
            }
        }
        else
        {
            if (EffectType != 0 && Receiver.ImmuneTypeList.Contains(EffectType))//如果此狀態類型不為0(不分類)，且目標擁有免疫的類型與此狀態類型相同則進行免疫
            {
                IsWork = false;//設定為不可執行
                Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
            }
        }
    }
    private void ClassIDCheck()
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (!Attacker.StatusEffectDic.ContainsKey(EffectClassID))//如果沒有存在相同的效果類型，則將此效果加入怪獸擁有的效果字典中
            {
                Attacker.StatusEffectDic[EffectClassID] = new List<ExcuteEffect>();
                Attacker.StatusEffectDic[EffectClassID].Add(this);
            }
            else
            {
                if (Attacker.StatusEffectDic[EffectClassID][0].EffectClassLevel <= EffectClassLevel)//如果此效果類型等級大於已存在的效果類型等級
                {
                    if (Attacker.StatusEffectDic[EffectClassID][0].EffectID == EffectID)//如果是同效果ID
                    {
                        if (Stackable)//如果是可堆疊的效果
                        {
                            if (Attacker.StatusEffectDic[EffectClassID].Count < MaxStackTimes)
                            {
                                Attacker.StatusEffectDic[EffectClassID].Add(this);
                            }
                            else
                            {
                                Attacker.StatusEffectDic[EffectClassID][0].EndEffect();
                                Attacker.StatusEffectDic[EffectClassID].Add(this);
                            }
                            if (ResetDuringTime)
                            {
                                for (int i = 0; i < Attacker.StatusEffectDic[EffectClassID].Count; i++)//如果開啟堆疊時重置效果時間，則重置堆疊中所有效果的持續時間
                                {
                                    Attacker.StatusEffectDic[EffectClassID][i].DuringTimer = DuringTime;
                                }
                            }
                        }
                        else//不可堆疊則清除怪物身上此效果類別ID的所有效果，再把此效果加入怪獸擁有效果字典中
                        {
                            for (int i = 0; i < Attacker.StatusEffectDic[EffectClassID].Count; i++)//清除怪物身上此效果類別ID的所有效果
                            {
                                Attacker.StatusEffectDic[EffectClassID][i].EndEffect();
                            }
                            Attacker.StatusEffectDic[EffectClassID].Add(this);
                        }
                    }
                    else//如果是不同效果，但是同效果類型，則清除怪物身上的效果，再把此效果加入怪獸擁有效果字典中
                    {
                        for (int i = 0; i < Attacker.StatusEffectDic[EffectClassID].Count; i++)//清除怪物身上此效果類別ID的所有效果
                        {
                            Attacker.StatusEffectDic[EffectClassID][i].EndEffect();
                        }
                        Attacker.StatusEffectDic[EffectClassID].Add(this);
                    }
                }
                else
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
        else
        {
            if (!Receiver.StatusEffectDic.ContainsKey(EffectClassID))//如果沒有存在相同的效果類型，則將此效果加入怪獸擁有的效果字典中
            {
                Receiver.StatusEffectDic[EffectClassID] = new List<ExcuteEffect>();
                Receiver.StatusEffectDic[EffectClassID].Add(this);
            }
            else
            {
                if (Receiver.StatusEffectDic[EffectClassID][0].EffectClassLevel <= EffectClassLevel)//如果此效果類型等級大於已存在的效果類型等級
                {
                    if (Receiver.StatusEffectDic[EffectClassID][0].EffectID == EffectID)//如果是同效果ID
                    {
                        if (Stackable)//如果是可堆疊的效果
                        {
                            if (Receiver.StatusEffectDic[EffectClassID].Count < MaxStackTimes)
                            {
                                Receiver.StatusEffectDic[EffectClassID].Add(this);
                            }
                            else
                            {
                                Receiver.StatusEffectDic[EffectClassID][0].EndEffect();
                                Receiver.StatusEffectDic[EffectClassID].Add(this);
                            }
                            if (ResetDuringTime)
                            {
                                for (int i = 0; i < Receiver.StatusEffectDic[EffectClassID].Count; i++)//如果開啟堆疊時重置效果時間，則重置堆疊中所有效果的持續時間
                                {
                                    Receiver.StatusEffectDic[EffectClassID][i].DuringTimer = DuringTime;
                                }
                            }
                        }
                        else//不可堆疊則清除怪物身上此效果類別ID的所有效果，再把此效果加入怪獸擁有效果字典中
                        {
                            for (int i = 0; i < Receiver.StatusEffectDic[EffectClassID].Count; i++)//清除怪物身上此效果類別ID的所有效果
                            {
                                Receiver.StatusEffectDic[EffectClassID][i].EndEffect();
                            }
                            Receiver.StatusEffectDic[EffectClassID].Add(this);
                        }
                    }
                    else//如果是不同效果，但是同效果類型，則清除怪物身上的效果，再把此效果加入怪獸擁有效果字典中
                    {
                        for (int i = 0; i < Receiver.StatusEffectDic[EffectClassID].Count; i++)//清除怪物身上此效果類別ID的所有效果
                        {
                            Receiver.StatusEffectDic[EffectClassID][i].EndEffect();
                        }
                        Receiver.StatusEffectDic[EffectClassID].Add(this);
                    }
                }
                else
                {
                    IsWork = false;//設定為不可執行
                    Remover.Destroy(this);//如果此效果類型等級較小，則直接移除此效果，不透過EndEffect
                }
            }
        }
    }

    private void TriggerSelfDamage()
    {
        if (Receiver.Alive)//目標活著
        {
            TriggerSelfProbabilityRnd = UnityEngine.Random.Range(1, 101);
            if (TriggerSelfProbabilityRnd <= TriggerDamage.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
            {
                selfDamage.SetDamage(Attacker, Receiver, TriggerDamage);
                selfDamage.Excute(20);
            }
        }
    }
    private void TriggerSelfCure()
    {
        if (Receiver.Alive)//目標活著
        {
            TriggerSelfProbabilityRnd = UnityEngine.Random.Range(1, 101);
            if (TriggerSelfProbabilityRnd <= TriggerCure.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
            {
                selfCure.Excute(21);
            }
        }
    }
    private void TriggerSelfEffect()
    {
        if (Attacker.Alive)//目標活著
        {
            TriggerSelfProbabilityRnd = UnityEngine.Random.Range(1, 101);
            if (TriggerSelfProbabilityRnd <= TriggerEffect.Probability * 100)//如果通過機率檢定，則觸發此狀態效果
            {
                slefEffect = Attacker.gameObject.AddComponent<ExcuteEffect>();
                slefEffect.SetEffect(Attacker, Receiver, TriggerEffect);
                slefEffect.Excute();
            }
        }
    }
    public void DuringTimerFunction()
    {
        if (IsWork)
        {
            if (DuringTimer > 0)
            {
                DeltTime = Time.deltaTime;
                if (haveCircleEffect)
                {
                    CircleTimer -= DeltTime;
                    if (CircleTimer > 0)
                    {
                        if (CircleTimer < 0)
                        {
                            CircleTimer = CircleTime + CircleTimer;
                            if (haveCircleDamage)
                                TriggerSelfDamage();
                            if (haveCircleCure)
                                TriggerSelfCure();
                            if (haveCircleStatusEffect)
                                TriggerSelfEffect();
                        }
                    }
                    else
                    {
                        CircleTimer = CircleTime + CircleTimer;
                        if (haveCircleDamage)
                            TriggerSelfDamage();
                        if (haveCircleCure)
                            TriggerSelfCure();
                        if (haveCircleStatusEffect)
                            TriggerSelfEffect();
                    }
                }
                if (!IsPermanent)//不為常駐法術時才會減持續時間
                    DuringTimer -= DeltTime;
            }
            else
            {
                EndEffect();
            }
        }
    }
    private void StartEffect()
    {
        AddTriggerDamage();//加入撞擊觸發效果
        AddTriggerEffect();//加入撞擊觸發效果
        AddTriggerCure();//加入撞擊觸發效果
        AddKnockWallTriggerEffect();//加入撞牆觸發效果
        AddKnockWallTriggerDamage();//加入撞牆觸發傷害
        AddKnockWallTriggerCure();//加入撞牆觸發治癒
        if (EStatusEffect.TargetIsSelf)
        {
            if (Attacker.Alive)
            {
                //設定基本屬性
                Attacker.SetBaseProperty(
                    DamagePlus, DamageTimes,
                    DefensePlus, DefenseTimes, ReduceDamagePlus, ReduceDamageTimes,
                    BrutalPlus, BrutalTimes,
                    AggressivePlus, AggressiveTimes,
                    ResistancePlus, ResistanceTimes,
                    SpeedPlus, SpeedTimes
                    );
                //設定擊退屬性
                Attacker.SetKnockProperty(KnockStrengthPlus, BeKnockStrengthPlus,
                    KnockNailTimePlus, BeKnockNailTimePlus,
                    KnockWallDMGPlus, KnockWallDMGTimes, BeKnockWallDMGPlus, BeKnockWallDMGTimes,
                    KnockWallStunPlus, BeKnockWallStunPlus
                    );
                //設定爆擊傷害屬性
                Attacker.SetElementHitProperty(CriticalDMGPlus, CriticalDMGTimes, BeCriticalDMGPlus, BeCriticalDMGTimes);
                //如果免疫類型不為0，則加入一個此免疫狀態類型到免疫狀態清單中
                if (ImmuneType != 0)
                    Attacker.ImmuneTypeList.Add(ImmuneType);
                if (ImmuneType != 0)//如果有免疫的狀態
                    Attacker.ClearStatusEffectByType(ImmuneType);//刪除身上屬於免疫對象的狀態


                if (IsBlessing)//如果效果會祝福則產生祝福
                {
                    Attacker.ClearDebuff();//清除debuff
                    Attacker.SetBlessingProperty(IsBlessing, EffectClassID);
                }
                if (IsCurse)//如果效果會詛咒則產生詛咒
                {
                    Attacker.ClearBuff();//清除buff
                    Attacker.SetCurseProperty(IsCurse, EffectClassID);
                }
                if (IsStun)//如果效果會暈眩則產生暈眩
                    Attacker.SetStunProperty(IsStun, EffectClassID);
                if (IsFetter)//如果效果會束縛則產生束縛
                    Attacker.SetFetterProperty(IsFetter, EffectClassID);
                if (IsNail)//如果效果會定身則產生定身
                    Attacker.SetNailProperty(IsNail, EffectClassID);
                if (IsRigid)//如果效果會鋼體則產生鋼體
                    Attacker.SetRigidProperty(IsRigid, EffectClassID);
                if (IsFear)//如果效果會恐懼則產生恐懼
                    Attacker.SetFearProperty(IsFear, EffectClassID);
                SpawnEffect(Attacker);//產生特效
            }
        }
        else
        {
            if (Receiver.Alive)
            {
                Receiver.SetBaseProperty(
                    DamagePlus, DamageTimes,
                    DefensePlus, DefenseTimes, ReduceDamagePlus, ReduceDamageTimes,
                    BrutalPlus, BrutalTimes,
                    AggressivePlus, AggressiveTimes,
                    ResistancePlus, ResistanceTimes,
                    SpeedPlus, SpeedTimes
                    );
                //設定擊退屬性
                Receiver.SetKnockProperty(KnockStrengthPlus, BeKnockStrengthPlus,
                    KnockNailTimePlus, BeKnockNailTimePlus,
                    KnockWallDMGPlus, KnockWallDMGTimes, BeKnockWallDMGPlus, BeKnockWallDMGTimes,
                    KnockWallStunPlus, BeKnockWallStunPlus
                    );
                //設定爆擊傷害屬性
                Receiver.SetElementHitProperty(CriticalDMGPlus, CriticalDMGTimes, BeCriticalDMGPlus, BeCriticalDMGTimes);
                //如果免疫類型不為0，則加入一個此免疫狀態類型到免疫狀態清單中
                if (ImmuneType != 0)
                    Receiver.ImmuneTypeList.Add(ImmuneType);
                if (ImmuneType != 0)//如果有免疫的狀態
                    Receiver.ClearStatusEffectByType(ImmuneType);//刪除身上屬於免疫對象的狀態
                if (IsBlessing)//如果效果會祝福則產生祝福
                {
                    Receiver.ClearDebuff();//清除debuff
                    Receiver.SetBlessingProperty(IsBlessing, EffectClassID);
                }
                if (IsCurse)//如果效果會詛咒則產生詛咒
                {
                    Receiver.ClearBuff();//清除Buff
                    Receiver.SetCurseProperty(IsCurse, EffectClassID);
                }
                if (IsStun)//如果效果會暈眩則產生暈眩
                    Receiver.SetStunProperty(IsStun, EffectClassID);
                if (IsFetter)//如果效果會束縛則產生束縛
                    Receiver.SetFetterProperty(IsFetter, EffectClassID);
                if (IsNail)//如果效果會定身則產生定身
                    Receiver.SetNailProperty(IsNail, EffectClassID);
                if (IsRigid)//如果效果會鋼體則產生鋼體
                    Receiver.SetRigidProperty(IsRigid, EffectClassID);
                if (IsFear)//如果效果會恐懼則產生恐懼
                    Receiver.SetFearProperty(IsFear, EffectClassID);
                SpawnEffect(Receiver);//產生特效
            }
        }
    }
    public void EndEffect()//EndEffect會連帶移除此類型的所有效果
    {
        IsWork = false;
        RemoveTriggerDamage();//加入撞擊觸發效果
        RemoveTriggerEffect();//加入撞擊觸發效果
        RemoveTriggerCure();//加入撞擊觸發效果
        RemoveKnockWallTriggerEffect();//加入撞牆觸發效果
        RemoveKnockWallTriggerDamage();//加入撞牆觸發傷害
        RemoveKnockWallTriggerCure();//加入撞牆觸發治癒
        if (EStatusEffect.TargetIsSelf)
        {
            Attacker.SetBaseProperty(
                -DamagePlus, -DamageTimes,
                -DefensePlus, -DefenseTimes, -ReduceDamagePlus, -ReduceDamageTimes,
                -BrutalPlus, -BrutalTimes,
                -AggressivePlus, -AggressiveTimes,
                -ResistancePlus, -ResistanceTimes,
                -SpeedPlus, -SpeedTimes
                );
            //設定擊退屬性
            Attacker.SetKnockProperty(-KnockStrengthPlus, -BeKnockStrengthPlus,
                -KnockNailTimePlus, -BeKnockNailTimePlus,
                -KnockWallDMGPlus, -KnockWallDMGTimes, -BeKnockWallDMGPlus, -BeKnockWallDMGTimes,
                -KnockWallStunPlus, -BeKnockWallStunPlus
                );
            //設定元素爆擊屬性
            Attacker.SetElementHitProperty(-CriticalDMGPlus, -CriticalDMGTimes, -BeCriticalDMGPlus, -BeCriticalDMGTimes);
            //移除免疫狀態類型清單中的一個此免疫種類
            Attacker.ImmuneTypeList.Remove(ImmuneType);
            if (IsBlessing)//如果有祝福則解除祝福
                Attacker.SetBlessingProperty(!IsBlessing, EffectClassID);
            if (IsCurse)//如果有詛咒則解除詛咒
                Attacker.SetCurseProperty(!IsCurse, EffectClassID);
            if (IsStun)//如果有暈眩則解除暈眩
                Attacker.SetStunProperty(!IsStun, EffectClassID);
            if (IsFetter)//如果有束縛則解除束縛
                Attacker.SetFetterProperty(!IsFetter, EffectClassID);
            if (IsNail)//如果有定身則解除束縛
                Attacker.SetNailProperty(!IsNail, EffectClassID);
            if (IsRigid)//如果有鋼體則解除鋼體
                Attacker.SetRigidProperty(!IsRigid, EffectClassID);
            if (IsFear)//如果有恐懼則解除恐懼
                Attacker.SetFearProperty(!IsFear, EffectClassID);

            if (!Attacker.RemoveStatusEffectList.Contains(EffectClassID))
            {
                Attacker.RemoveStatusEffectList.Add(EffectClassID);
            }

            if (!Attacker.RemoveStatusEffectStackDic.ContainsKey(EffectClassID))
                Attacker.RemoveStatusEffectStackDic.Add(EffectClassID, 1);
            else
                Attacker.RemoveStatusEffectStackDic[EffectClassID]++;
        }
        else
        {
            Receiver.SetBaseProperty(
                -DamagePlus, -DamageTimes,
                -DefensePlus, -DefenseTimes, -ReduceDamagePlus, -ReduceDamageTimes,
                -BrutalPlus, -BrutalTimes,
                -AggressivePlus, -AggressiveTimes,
                -ResistancePlus, -ResistanceTimes,
                -SpeedPlus, -SpeedTimes
                );
            //設定擊退屬性
            Receiver.SetKnockProperty(-KnockStrengthPlus, -BeKnockStrengthPlus,
                -KnockNailTimePlus, -BeKnockNailTimePlus,
                -KnockWallDMGPlus, -KnockWallDMGTimes, -BeKnockWallDMGPlus, -BeKnockWallDMGTimes,
                -KnockWallStunPlus, -BeKnockWallStunPlus
                );
            //設定元素爆擊屬性
            Receiver.SetElementHitProperty(-CriticalDMGPlus, -CriticalDMGTimes, -BeCriticalDMGPlus, -BeCriticalDMGTimes);
            //移除免疫狀態類型清單中的一個此免疫種類
            Receiver.ImmuneTypeList.Remove(ImmuneType);
            if (IsBlessing)//如果有祝福則解除祝福
                Receiver.SetBlessingProperty(!IsBlessing, EffectClassID);
            if (IsCurse)//如果有詛咒則解除詛咒
                Receiver.SetCurseProperty(!IsCurse, EffectClassID);
            if (IsStun)//如果有暈眩則解除暈眩
                Receiver.SetStunProperty(!IsStun, EffectClassID);
            if (IsFetter)//如果有束縛則解除束縛
                Receiver.SetFetterProperty(!IsFetter, EffectClassID);
            if (IsNail)//如果有定身則解除束縛
                Receiver.SetNailProperty(!IsNail, EffectClassID);
            if (IsRigid)//如果有鋼體則解除鋼體
                Receiver.SetRigidProperty(!IsRigid, EffectClassID);
            if (IsFear)//如果有恐懼則解除恐懼
                Receiver.SetFearProperty(!IsFear, EffectClassID);

            if (!Receiver.RemoveStatusEffectList.Contains(EffectClassID))
            {
                Receiver.RemoveStatusEffectList.Add(EffectClassID);
            }


            if (!Receiver.RemoveStatusEffectStackDic.ContainsKey(EffectClassID))
                Receiver.RemoveStatusEffectStackDic.Add(EffectClassID, 1);
            else
                Receiver.RemoveStatusEffectStackDic[EffectClassID]++;
        }
        StopEffect();
        Remover.Destroy(this);
    }
    void Excute()
    {
        StartEffect();
        if (!FirstTrigger)
            CircleTimer = CircleTime;
        DuringTimer = DuringTime;
    }

    void SpawnEffect(Status _target)
    {
        if (EffectAniID != 0)
        {
            EffectSpawner selfES = new EffectSpawner(EffectAniID);
            effectAniUnqueID = selfES.SpawnTargetLoopEffect(_target);
        }
    }
    void StopEffect()
    {
        //Debug.Log("effectAniUnqueID=" + effectAniUnqueID);
        if (EffectAniID != 0 && EffectSpawner.AnimatorDic.ContainsKey(effectAniUnqueID))
        {
            EffectSpawner.AnimatorDic[effectAniUnqueID].SetTrigger("Idle");
        }
    }
 
}
