using UnityEngine;
using System.Collections;

public class DamageCalculator : Calculator, IViewDebugLog
{

    private int lethality;
    protected int Lethality
    {
        get { return lethality; }
        set
        {
            if (value < 0)
            {
                lethality = 0;
            }
            else { lethality = value; }
        }
    }
    private int resistance;
    protected int Resistance
    {
        get { return resistance; }
        set
        {
            if (value < 0)
            {
                resistance = 0;
            }
            else { resistance = value; }
        }
    }
    private float resistanceRatio;
    protected float ResistanceRatio
    {
        get { return resistanceRatio; }
        set
        {
            if (value < 0)
            {
                resistanceRatio = 0;
            }
            else if (value > 1)
            {
                resistanceRatio = 1;
            }
            else { resistanceRatio = value; }
        }
    }
    protected const int HalfFactor = 100;
    private int damage;
    protected int Damage
    {
        get { return damage; }
        set
        {
            if (value < 0)
            {
                damage = 0;
            }
            else { damage = value; }
        }
    }
    //傷害計算
    public int GetDamage(bool _absoluteDamage,
        int _attack, float _attackTimes, float _spellAttackTimes, int _attackPlus, float _penetration, int _elementDamagePlus, float _elementDamageTimes,
        int _defense, float _defenseTimes, int _defensePlus,int _reduceDamagePlus,float _reduceDamageTimes)
    {
        if (!_absoluteDamage)//計算減防傷害
        {
            Lethality = (int)((_attack * _spellAttackTimes) * _attackTimes) + _attackPlus;
            Resistance = (int)(_defense * _defenseTimes) + _defensePlus;
            ResistanceRatio = 1 - _penetration;
            Damage = (int)(((Lethality * (float)(HalfFactor / (float)(HalfFactor + (int)(Resistance * ResistanceRatio))) * _elementDamageTimes) + _elementDamagePlus) * (1 - _reduceDamageTimes) - _reduceDamagePlus);
            //ViewDebugLog();//查看Log
        }
        else//絕對傷害
        {
            Lethality = (int)((_attack * _spellAttackTimes) * _attackTimes) + _attackPlus;
            Damage = Lethality;
        }
        return Damage;
    }

    //撞牆傷害計算
    public int GetKnockWallDamage(int _attackPlus)
    {
        Lethality = _attackPlus;
        Damage = Lethality;
        //ViewDebugLog();//查看Log
        return Damage;
    }

    public void ViewDebugLog()
    {
        Debug.Log("Lethality=" + Lethality);
        Debug.Log("Resistance=" + Resistance);
        Debug.Log("ResistanceRatio=" + ResistanceRatio);
        Debug.Log("Damage=" + Damage);
    }
}
