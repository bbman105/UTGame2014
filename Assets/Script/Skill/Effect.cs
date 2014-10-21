using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class StatusEffect
{
    //狀態效果ID
    private int effectID;
    public int EffectID
    {
        get { return effectID; }
        set { effectID = value; }
    }
    //狀態種類0=一般,1=持續傷害,2=虛弱,3=緩速,4=暈眩,5=恐懼,6=束縛,7=定身,8=不朽(不會被覆蓋及清除的效果種類)
    private int effectType;
    public int EffectType
    {
        get { return effectType; }
        set { effectType = value; }
    }
    private bool targetIsSelf;
    public bool TargetIsSelf//目標是否為自身
    {
        get { return targetIsSelf; }
        set { targetIsSelf = value; }
    }
    //效果類別ID
    private int effectClassID;
    public int EffectClassID
    {
        get { return effectClassID; }
        set { effectClassID = value; }
    }
    //效果類別等級
    private int effectClassLevel;
    public int EffectClassLevel
    {
        get { return effectClassLevel; }
        set { effectClassLevel = value; }
    }
    //正面狀態效果
    bool isBuff;
    public bool IsBuff
    {
        get { return isBuff; }
        set { isBuff = value; }
    }
    //負面狀態效果
    bool isDebuff;
    public bool IsDebuff
    {
        get { return isDebuff; }
        set { isDebuff = value; }
    }
    //先制效果
    private bool isPreATK;
    public bool IsPreATK
    {
        get { return isPreATK; }
        set { isPreATK = value; }
    }
    //速度
    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    //觸發機率
    private float probability;
    public float Probability
    {
        get { return probability; }
        set { probability = value; }
    }
    //常駐狀態法術
    private bool isPermanent;
    public bool IsPermanent
    {
        get { return isPermanent; }
        set { isPermanent = value; }
    }
    //持續時間
    private float duringTime;
    public float DuringTime
    {
        get { return duringTime; }
        set { duringTime = value; }
    }
    //時間內觸發週期
    private float circleTime;
    public float CircleTime
    {
        get { return circleTime; }
        set
        {
            if (value < 0.1f)
                circleTime = 0;
            else
                circleTime = value;
        }
    }
    bool firstTrigger;
    public bool FirstTrigger
    {
        get { return firstTrigger; }
        set { firstTrigger = value; }
    }
    //週期觸發治癒
    int triggerCure;
    public int TriggerCure
    {
        get { return triggerCure; }
        set { triggerCure = value; }
    }
    //週期觸發傷害
    int triggerDamage;
    public int TriggerDamage
    {
        get { return triggerDamage; }
        set { triggerDamage = value; }
    }
    //週期觸發狀態效果
    int triggerEffect;
    public int TriggerEffect
    {
        get { return triggerEffect; }
        set { triggerEffect = value; }
    }
    //傷害倍率改變
    float damageTimes;
    public float DamageTimes
    {
        get { return damageTimes; }
        set { damageTimes = value; }
    }
    //傷害加值改變
    private int damagePlus;
    public int DamagePlus
    {
        get { return damagePlus; }
        set { damagePlus = value; }
    }
    //防禦倍率改變
    float defenseTimes;
    public float DefenseTimes
    {
        get { return defenseTimes; }
        set { defenseTimes = value; }
    }
    //防禦加值改變
    int defensePlus;
    public int DefensePlus
    {
        get { return defensePlus; }
        set { defensePlus = value; }
    }
    //速度倍率改變
    float speedTimes;
    public float SpeedTimes
    {
        get { return speedTimes; }
        set { speedTimes = value; }
    }
    //速度加值改變
    private int speedPlus;
    public int SpeedPlus
    {
        get { return speedPlus; }
        set { speedPlus = value; }
    }
    //命中倍率改變
    private float accurateTimes;
    public float AccurateTimes
    {
        get { return accurateTimes; }
        set { accurateTimes = value; }
    }
    //殘暴倍率改變
    float brutalTimes;
    public float BrutalTimes
    {
        get { return brutalTimes; }
        set { brutalTimes = value; }
    }
    //殘暴加值改變
    private int brutalPlus;
    public int BrutalPlus
    {
        get { return brutalPlus; }
        set { brutalPlus = value; }
    }
    //侵略倍率改變
    float aggressiveTimes;
    public float AggressiveTimes
    {
        get { return aggressiveTimes; }
        set { aggressiveTimes = value; }
    }
    //侵略加值改變
    private int aggressivePlus;
    public int AggressivePlus
    {
        get { return aggressivePlus; }
        set { aggressivePlus = value; }
    }
    //抵抗倍率改變
    float resistanceTimes;
    public float ResistanceTimes
    {
        get { return resistanceTimes; }
        set { resistanceTimes = value; }
    }
    //抵抗加值改變
    private int resistancePlus;
    public int ResistancePlus
    {
        get { return resistancePlus; }
        set { resistancePlus = value; }
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
    //暈眩效果
    private bool isStun;
    public bool IsStun
    {
        get { return isStun; }
        set { isStun = value; }
    }
    private bool isFear;
    public bool IsFear
    {
        get { return isFear; }
        set { isFear = value; }
    }
    //沉默效果
    private bool isSilence;
    public bool IsSilence
    {
        get { return isSilence; }
        set { isSilence = value; }
    }
    //束縛效果
    private bool isFetter;
    public bool IsFetter
    {
        get { return isFetter; }
        set { isFetter = value; }
    }
    //定身效果
    private bool isNail;
    public bool IsNail
    {
        get { return isNail; }
        set { isNail = value; }
    }
    //剛體效果
    private bool isRigid;
    public bool IsRigid
    {
        get { return isRigid; }
        set { isRigid = value; }
    }
    //無敵效果
    bool isInvincible;
    public bool IsInvincible
    {
        get { return isInvincible; }
        set { isInvincible = value; }
    }
    //狀態特效ID
    private int effectAni;
    public int EffectAni
    {
        get { return effectAni; }
        set { effectAni = value; }
    }
    //碰撞觸發效果目標傷害
    int knockTriggerTargetDamage;
    public int KnockTriggerTargetDamage
    {
        get { return knockTriggerTargetDamage; }
        set { knockTriggerTargetDamage = value; }
    }
    //碰撞觸發效果目標狀態效果
    int knockTriggerTargetEffect;
    public int KnockTriggerTargetEffect
    {
        get { return knockTriggerTargetEffect; }
        set { knockTriggerTargetEffect = value; }
    }
    //碰撞觸發效果目標治癒
    int knockTriggerTargetCure;
    public int KnockTriggerTargetCure
    {
        get { return knockTriggerTargetCure; }
        set { knockTriggerTargetCure = value; }
    }
    //碰撞觸發自身傷害
    int knockTriggerSelfDamage;
    public int KnockTriggerSelfDamage
    {
        get { return knockTriggerSelfDamage; }
        set { knockTriggerSelfDamage = value; }
    }
    //碰撞觸發效果自身治癒
    int knockTriggerSelfCure;
    public int KnockTriggerSelfCure
    {
        get { return knockTriggerSelfCure; }
        set { knockTriggerSelfCure = value; }
    }
    //碰撞觸發效果自身狀態效果
    int knockTriggerSelfEffect;
    public int KnockTriggerSelfEffect
    {
        get { return knockTriggerSelfEffect; }
        set { knockTriggerSelfEffect = value; }
    }
    //被逼牆觸發自身狀態效果
    int beKnockWallTriggerSelfEffect;
    public int BeKnockWallTriggerSelfEffect
    {
        get { return beKnockWallTriggerSelfEffect; }
        set { beKnockWallTriggerSelfEffect = value; }
    }
    //被逼牆觸發自身傷害效果
    int beKnockWallTriggerSelfDamage;
    public int BeKnockWallTriggerSelfDamage
    {
        get { return beKnockWallTriggerSelfDamage; }
        set { beKnockWallTriggerSelfDamage = value; }
    }
    //被逼牆觸發自身治癒效果
    int beKnockWallTriggerSelfCure;
    public int BeKnockWallTriggerSelfCure
    {
        get { return beKnockWallTriggerSelfCure; }
        set { beKnockWallTriggerSelfCure = value; }
    }
    bool stackable;
    public bool Stackable
    {
        get { return stackable; }
        set { stackable = value; }
    }
    int maxStackTimes;
    public int MaxStackTimes
    {
        get { return maxStackTimes; }
        set { maxStackTimes = value; }
    }
    bool resetDuringTime;
    public bool ResetDuringTime
    {
        get { return resetDuringTime; }
        set { resetDuringTime = value; }
    }
    int aniID;
    public int AniID
    {
        get { return aniID; }
        set { aniID = value; }
    }

    public StatusEffect(int _effectID, bool _targetIsSelf)
    {
        if (_effectID != 0)
        {
            //依照法術傷害編號讀取法術傷害資料中的各屬性
            TextAsset XMLFlie = Resources.Load<TextAsset>("String/Effect");
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFlie.text);
            XmlNode root = doc.DocumentElement;









            //抓取此技能的node區段
            XmlNode SpellNode = root.SelectSingleNode("EID" + _effectID);
            EffectID = int.Parse(SpellNode.SelectSingleNode("EffectID").InnerText);
            TargetIsSelf = _targetIsSelf;
            EffectClassID = int.Parse(SpellNode.SelectSingleNode("EffectClassID").InnerText);
            EffectClassLevel = int.Parse(SpellNode.SelectSingleNode("EffectClassLevel").InnerText);
            IsBuff = bool.Parse(SpellNode.SelectSingleNode("IsBuff").InnerText);
            IsDebuff = bool.Parse(SpellNode.SelectSingleNode("IsDebuff").InnerText);
            EffectType = int.Parse(SpellNode.SelectSingleNode("Type").InnerText);
            IsPreATK = bool.Parse(SpellNode.SelectSingleNode("PreATK").InnerText);

            //觸發機率
            Probability = float.Parse(SpellNode.SelectSingleNode("Probability").InnerText);
            IsPermanent = bool.Parse(SpellNode.SelectSingleNode("IsPermanent").InnerText);
            DuringTime = float.Parse(SpellNode.SelectSingleNode("Time").InnerText);
            //判斷是什麼類型的狀態
            string durringTimeTriggerStr = SpellNode.SelectSingleNode("DurringTimeTrigger").InnerText;
            if (durringTimeTriggerStr != "0")
            {
                string triggerType = SpellNode.SelectSingleNode("DurringTimeTrigger").InnerText.Substring(0, 1);
                string triggerID = SpellNode.SelectSingleNode("DurringTimeTrigger").InnerText.Substring(1);
                switch (triggerType)
                {
                    case "D":
                        TriggerDamage = int.Parse(triggerID);
                        break;
                    case "C":
                        TriggerCure = int.Parse(triggerID);
                        break;
                    case "E":
                        TriggerEffect = int.Parse(triggerID);
                        break;
                    default:
                        Debug.Log("狀態效果觸發類型無法辨識");
                        break;
                }
            }
            CircleTime = float.Parse(SpellNode.SelectSingleNode("Circle").InnerText);
            //初始觸發
            FirstTrigger = bool.Parse(SpellNode.SelectSingleNode("FirstTrigger").InnerText);
            //傷害改變
            DamagePlus = int.Parse(SpellNode.SelectSingleNode("DamagePlus").InnerText);
            DamageTimes = float.Parse(SpellNode.SelectSingleNode("DamageTimes").InnerText);
            //防禦
            DefensePlus = int.Parse(SpellNode.SelectSingleNode("DefensePlus").InnerText);
            DefenseTimes = float.Parse(SpellNode.SelectSingleNode("DefenseTimes").InnerText);
            //速度改變
            SpeedPlus = int.Parse(SpellNode.SelectSingleNode("SpeedPlus").InnerText);
            SpeedTimes = float.Parse(SpellNode.SelectSingleNode("SpeedTimes").InnerText);
            //命中率改變
            AccurateTimes = float.Parse(SpellNode.SelectSingleNode("AccurateTimes").InnerText);
            //殘暴改變
            BrutalPlus = int.Parse(SpellNode.SelectSingleNode("BrutalPlus").InnerText);
            BrutalTimes = float.Parse(SpellNode.SelectSingleNode("BrutalTimes").InnerText);
            //侵略改變
            AggressivePlus = int.Parse(SpellNode.SelectSingleNode("AggressivePlus").InnerText);
            AggressiveTimes = float.Parse(SpellNode.SelectSingleNode("AggressiveTimes").InnerText);
            //抵抗改變
            ResistancePlus = int.Parse(SpellNode.SelectSingleNode("ResistancePlus").InnerText);
            ResistanceTimes = float.Parse(SpellNode.SelectSingleNode("ResistanceTimes").InnerText);
            //傷害減免值
            ReduceDamagePlus = int.Parse(SpellNode.SelectSingleNode("ReduceDamagePlus").InnerText);
            //傷害減免率
            ReduceDamageTimes = float.Parse(SpellNode.SelectSingleNode("ReduceDamageTimes").InnerText);
            //逼牆傷害加值改變
            KnockWallDMGPlus = int.Parse(SpellNode.SelectSingleNode("KnockWallDMGPlus").InnerText);
            //逼牆傷害倍率改變
            KnockWallDMGTimes = float.Parse(SpellNode.SelectSingleNode("KnockWallDMGTimes").InnerText);
            //被逼牆害加值改變
            BeKnockWallDMGPlus = int.Parse(SpellNode.SelectSingleNode("BeKnockWallDMGPlus").InnerText);
            //被逼牆傷害倍率改變
            BeKnockWallDMGTimes = float.Parse(SpellNode.SelectSingleNode("BeKnockWallDMGTimes").InnerText);
            //逼牆暈眩加值改變
            KnockWallStunPlus = float.Parse(SpellNode.SelectSingleNode("KnockWallStunPlus").InnerText);
            //被逼牆暈眩加值改變
            BeKnockWallStunPlus = float.Parse(SpellNode.SelectSingleNode("BeKnockWallStunPlus").InnerText);
            //擊退力道加值改變
            KnockStrengthPlus = int.Parse(SpellNode.SelectSingleNode("KnockStrengthPlus").InnerText);
            //被擊退力道加值改變
            BeKnockStrengthPlus = int.Parse(SpellNode.SelectSingleNode("BeKnockStrengthPlus").InnerText);
            //擊退定身時間加值
            KnockNailTimePlus = float.Parse(SpellNode.SelectSingleNode("KnockNailTimePlus").InnerText);
            //被擊退定身時間加值
            BeKnockNailTimePlus = float.Parse(SpellNode.SelectSingleNode("BeKnockNailTimePlus").InnerText);
            //重擊傷害加值改變
            CriticalDMGPlus = int.Parse(SpellNode.SelectSingleNode("CriticalDMGPlus").InnerText);
            //重擊傷害倍率改變
            CriticalDMGTimes = float.Parse(SpellNode.SelectSingleNode("CriticalDMGTimes").InnerText);
            //重擊傷害加值改變
            BeCriticalDMGPlus = int.Parse(SpellNode.SelectSingleNode("BeCriticalDMGPlus").InnerText);
            //重擊傷害倍率改變
            BeCriticalDMGTimes = float.Parse(SpellNode.SelectSingleNode("BeCriticalDMGTimes").InnerText);
            //免疫的效果類型
            ImmuneType = int.Parse(SpellNode.SelectSingleNode("ImmuneType").InnerText);
            IsCurse = bool.Parse(SpellNode.SelectSingleNode("Curse").InnerText);
            IsBlessing = bool.Parse(SpellNode.SelectSingleNode("Blessing").InnerText);
            IsStun = bool.Parse(SpellNode.SelectSingleNode("Stun").InnerText);
            IsFear = bool.Parse(SpellNode.SelectSingleNode("Fear").InnerText);
            IsSilence = bool.Parse(SpellNode.SelectSingleNode("Silence").InnerText);
            IsFetter = bool.Parse(SpellNode.SelectSingleNode("Fetter").InnerText);
            IsNail = bool.Parse(SpellNode.SelectSingleNode("Nail").InnerText);
            IsRigid = bool.Parse(SpellNode.SelectSingleNode("Rigid").InnerText);
            IsInvincible = bool.Parse(SpellNode.SelectSingleNode("Invincible").InnerText);
            EffectAni = int.Parse(SpellNode.SelectSingleNode("EffectAni").InnerText);
            //判斷碰撞觸發目標的是什麼類型的狀態
            string knockTriggerTargetStr = SpellNode.SelectSingleNode("KnockTriggerTarget").InnerText;
            if (knockTriggerTargetStr != "0")
            {
                string knockTriggerTargetType = knockTriggerTargetStr.Substring(0, 1);
                string knockTriggerTargetID = knockTriggerTargetStr.Substring(1);
                switch (knockTriggerTargetType)
                {
                    case "D":
                        KnockTriggerTargetDamage = int.Parse(knockTriggerTargetID);
                        break;
                    case "C":
                        KnockTriggerTargetCure = int.Parse(knockTriggerTargetID);
                        break;
                    case "E":
                        KnockTriggerTargetEffect = int.Parse(knockTriggerTargetID);
                        break;
                    default:
                        Debug.Log("碰撞觸發目標類型無法辨識");
                        break;
                }
            }

            //判斷碰撞觸發目標的是什麼類型的狀態
            string knockTriggerSelfStr = SpellNode.SelectSingleNode("KnockTriggerSelf").InnerText;
            if (knockTriggerSelfStr != "0")
            {
                string knockTriggerSelfType = knockTriggerSelfStr.Substring(0, 1);
                string knockTriggerSelfID = knockTriggerSelfStr.Substring(1);
                switch (knockTriggerSelfType)
                {
                    case "D":
                        KnockTriggerSelfDamage = int.Parse(knockTriggerSelfID);
                        break;
                    case "C":
                        KnockTriggerSelfCure = int.Parse(knockTriggerSelfID);
                        break;
                    case "E":
                        KnockTriggerSelfEffect = int.Parse(knockTriggerSelfID);
                        break;
                    default:
                        Debug.Log("碰撞觸發自身類型無法辨識");
                        break;
                }
            }
            //判斷碰撞觸發目標的是什麼類型的狀態
            string beKnockWallTriggerSelfStr = SpellNode.SelectSingleNode("BeKnockWallTriggerSelf").InnerText;
            if (beKnockWallTriggerSelfStr != "0")
            {
                string beKnockWallTriggerSelfType = beKnockWallTriggerSelfStr.Substring(0, 1);
                string beKnockWallTriggerSelfID = beKnockWallTriggerSelfStr.Substring(1);
                switch (beKnockWallTriggerSelfType)
                {
                    case "D":
                        BeKnockWallTriggerSelfDamage = int.Parse(beKnockWallTriggerSelfID);
                        break;
                    case "C":
                        BeKnockWallTriggerSelfCure = int.Parse(beKnockWallTriggerSelfID);
                        break;
                    case "E":
                        BeKnockWallTriggerSelfEffect = int.Parse(beKnockWallTriggerSelfID);
                        break;
                    default:
                        Debug.Log("被逼牆觸發自身類型無法辨識");
                        break;
                }
            }
            Stackable = bool.Parse(SpellNode.SelectSingleNode("Stackable").InnerText);
            MaxStackTimes = int.Parse(SpellNode.SelectSingleNode("MaxStackTimes").InnerText);
            ResetDuringTime = bool.Parse(SpellNode.SelectSingleNode("ResetDuringTime").InnerText);

            try
            {

            }
            catch
            {
                Debug.Log("讀取Effect.xml資料時發生錯誤");
            }
        }

    }


}
