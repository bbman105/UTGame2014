using UnityEngine;
using System.Collections;

public partial class ExcuteEffect : MonoBehaviour
{

    void AddKnockWallTriggerEffect()//加入撞牆觸發效果
    {
        if (BeKnockWallTriggerEffect != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.AddKnockWallTriggerEffectList(BeKnockWallTriggerEffect);
            }
            else
            {
                Receiver.AddKnockWallTriggerEffectList(BeKnockWallTriggerEffect);
            }
        }
    }
    void AddKnockWallTriggerDamage()//加入撞牆觸發傷害
    {
        if (BeKnockWallTriggerDamage != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.AddKnockWallTriggerDamageList(BeKnockWallTriggerDamage);
            }
            else
            {
                Receiver.AddKnockWallTriggerDamageList(BeKnockWallTriggerDamage);
            }
        }
    }
    void AddKnockWallTriggerCure()//加入撞牆觸發治癒
    {
        if (BeKnockWallTriggerCure != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.AddKnockWallTriggerCureList(BeKnockWallTriggerCure);
            }
            else
            {
                Receiver.AddKnockWallTriggerCureList(BeKnockWallTriggerCure);
            }
        }
    }
    void RemoveKnockWallTriggerEffect()//移除撞牆觸發效果
    {
        if (BeKnockWallTriggerEffect != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.RemoveKnockWallTriggerEffectList(BeKnockWallTriggerEffect);
            }
            else
            {
                Receiver.RemoveKnockWallTriggerEffectList(BeKnockWallTriggerEffect);
            }
        }
    }
    void RemoveKnockWallTriggerDamage()//移除撞牆觸發傷害
    {
        if (BeKnockWallTriggerDamage != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.RemoveKnockWallTriggerDamageList(BeKnockWallTriggerDamage);
            }
            else
            {
                Receiver.RemoveKnockWallTriggerDamageList(BeKnockWallTriggerDamage);
            }
        }
    }
    void RemoveKnockWallTriggerCure()//移除撞牆觸發治癒
    {
        if (BeKnockWallTriggerCure != null)
        {
            if (EStatusEffect.TargetIsSelf)
            {
                Attacker.RemoveKnockWallTriggerCureList(BeKnockWallTriggerCure);
            }
            else
            {
                Receiver.RemoveKnockWallTriggerCureList(BeKnockWallTriggerCure);
            }
        }
    }




    void AddTriggerDamage()//加入撞擊觸發傷害
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockSelfDamage != null)
            {
                Attacker.AddKnockTriggerTargetDamageList(KnockSelfDamage);
            }
            if (KnockTargetDamage != null)
            {
                Attacker.AddKnockTriggerTargetDamageList(KnockTargetDamage);
            }
        }
        else
        {
            if (KnockSelfDamage != null)
            {
                Receiver.AddKnockTriggerTargetDamageList(KnockSelfDamage);
            }
            if (KnockTargetDamage != null)
            {
                Receiver.AddKnockTriggerTargetDamageList(KnockTargetDamage);
            }
        }
    }
    void AddTriggerEffect()//加入撞擊觸發效果
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockTargetEffect != null)
            {
                if (KnockTargetEffect.IsPreATK)
                {
                    Attacker.AddPreKnockTriggerTargetEffectList(KnockTargetEffect);
                }
                else
                {
                    Attacker.AddReKnockTriggerTargetEffectList(KnockTargetEffect);
                }
            }
            if (KnockSelfEffect != null)
            {
                if (KnockSelfEffect.IsPreATK)
                {
                    Attacker.AddPreKnockTriggerSelfEffectList(KnockSelfEffect);
                }
                else
                {
                    Attacker.AddReKnockTriggerSelfEffectList(KnockSelfEffect);
                }
            }
        }
        else
        {
            if (KnockTargetEffect != null)
            {
                if (KnockTargetEffect.IsPreATK)
                {
                    Receiver.AddPreKnockTriggerTargetEffectList(KnockTargetEffect);
                }
                else
                {
                    Receiver.AddReKnockTriggerTargetEffectList(KnockTargetEffect);
                }
            }
            if (KnockSelfEffect != null)
            {
                if (KnockSelfEffect.IsPreATK)
                {
                    Receiver.AddPreKnockTriggerSelfEffectList(KnockSelfEffect);
                }
                else
                {
                    Receiver.AddReKnockTriggerSelfEffectList(KnockSelfEffect);
                }
            }
        }
    }
    void AddTriggerCure()//加入撞擊觸發治癒
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockTargetCure != null)
            {
                if (KnockTargetCure.IsPreATK)
                {
                    Attacker.AddPreKnockTriggerTargetCureList(KnockTargetCure);
                }
                else
                {
                    Attacker.AddReKnockTriggerTargetCureList(KnockTargetCure);
                }
            }
            if (KnockSelfCure != null)
            {
                if (KnockSelfCure.IsPreATK)
                {
                    Attacker.AddPreKnockTriggerSelfCureList(KnockSelfCure);
                }
                else
                {
                    Attacker.AddReKnockTriggerSelfCureList(KnockSelfCure);
                }
            }
        }
        else
        {
            if (KnockTargetCure != null)
            {
                if (KnockTargetCure.IsPreATK)
                {
                    Receiver.AddPreKnockTriggerTargetCureList(KnockTargetCure);
                }
                else
                {
                    Receiver.AddReKnockTriggerTargetCureList(KnockTargetCure);
                }
            }
            if (KnockSelfCure != null)
            {
                if (KnockSelfCure.IsPreATK)
                {
                    Receiver.AddPreKnockTriggerSelfCureList(KnockSelfCure);
                }
                else
                {
                    Receiver.AddReKnockTriggerSelfCureList(KnockSelfCure);
                }
            }
        }
    }


    void RemoveTriggerDamage()//加入撞擊觸發傷害
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockSelfDamage != null)
            {
                Attacker.RemoveKnockTriggerTargetDamageList(KnockSelfDamage);
            }
            if (KnockTargetDamage != null)
            {
                Attacker.RemoveKnockTriggerTargetDamageList(KnockTargetDamage);
            }
        }
        else
        {
            if (KnockSelfDamage != null)
            {
                Receiver.RemoveKnockTriggerTargetDamageList(KnockSelfDamage);
            }
            if (KnockTargetDamage != null)
            {
                Receiver.RemoveKnockTriggerTargetDamageList(KnockTargetDamage);
            }
        }
    }
    void RemoveTriggerEffect()//加入撞擊觸發效果
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockTargetEffect != null)
            {
                if (KnockTargetEffect.IsPreATK)
                {
                    Attacker.RemovePreKnockTriggerTargetEffectList(KnockTargetEffect);
                }
                else
                {
                    Attacker.RemoveReKnockTriggerTargetEffectList(KnockTargetEffect);
                }
            }
            if (KnockSelfEffect != null)
            {
                if (KnockSelfEffect.IsPreATK)
                {
                    Attacker.RemovePreKnockTriggerSelfEffectList(KnockSelfEffect);
                }
                else
                {
                    Attacker.RemoveReKnockTriggerSelfEffectList(KnockSelfEffect);
                }
            }
        }
        else
        {
            if (KnockTargetEffect != null)
            {
                if (KnockTargetEffect.IsPreATK)
                {
                    Receiver.RemovePreKnockTriggerTargetEffectList(KnockTargetEffect);
                }
                else
                {
                    Receiver.RemoveReKnockTriggerTargetEffectList(KnockTargetEffect);
                }
            }
            if (KnockSelfEffect != null)
            {
                if (KnockSelfEffect.IsPreATK)
                {
                    Receiver.RemovePreKnockTriggerSelfEffectList(KnockSelfEffect);
                }
                else
                {
                    Receiver.RemoveReKnockTriggerSelfEffectList(KnockSelfEffect);
                }
            }
        }
    }
    void RemoveTriggerCure()//加入撞擊觸發治癒
    {
        if (EStatusEffect.TargetIsSelf)
        {
            if (KnockTargetCure != null)
            {
                if (KnockTargetCure.IsPreATK)
                {
                    Attacker.RemovePreKnockTriggerTargetCureList(KnockTargetCure);
                }
                else
                {
                    Attacker.RemoveReKnockTriggerTargetCureList(KnockTargetCure);
                }
            }
            if (KnockSelfCure != null)
            {
                if (KnockSelfCure.IsPreATK)
                {
                    Attacker.RemovePreKnockTriggerSelfCureList(KnockSelfCure);
                }
                else
                {
                    Attacker.RemoveReKnockTriggerSelfCureList(KnockSelfCure);
                }
            }
        }
        else
        {
            if (KnockTargetCure != null)
            {
                if (KnockTargetCure.IsPreATK)
                {
                    Receiver.RemovePreKnockTriggerTargetCureList(KnockTargetCure);
                }
                else
                {
                    Receiver.RemoveReKnockTriggerTargetCureList(KnockTargetCure);
                }
            }
            if (KnockSelfCure != null)
            {
                if (KnockSelfCure.IsPreATK)
                {
                    Receiver.RemovePreKnockTriggerSelfCureList(KnockSelfCure);
                }
                else
                {
                    Receiver.RemoveReKnockTriggerSelfCureList(KnockSelfCure);
                }
            }
        }
    }
}
