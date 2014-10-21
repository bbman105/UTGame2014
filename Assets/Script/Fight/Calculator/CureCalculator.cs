using UnityEngine;
using System.Collections;

public class CureCalculator : Calculator {

    int cure;
    public int Cure
    {
        get { return cure; }
        set
        {
            if (value < 0)
            {
                cure = 0;
            }
            else { cure = value; }
        }
    }

    //治癒計算器
    public int GetCure(int _baseCure, int _curePlus,float _cureTimes)
    {
        Cure = (int)((float)(_baseCure + _curePlus) * _cureTimes);
        return Cure;
    }


}
