using UnityEngine;
using System.Collections;

public abstract class BaseExcute
{
    //ExtendProperty
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (value < 0)
            {
                maxHealth = 0;
            }
            else { maxHealth = value; }
        }
    }
    private int curHealth;
    public int CurHealth
    {
        get { return curHealth; }
        set
        {
            if (value < 0)
            {
                curHealth = 0;
            }
            else if (value > MaxHealth)
            {
                curHealth = MaxHealth;
            }
            else { curHealth = value; }
        }
    }

    public abstract void Excute(int _numberAni);

}
