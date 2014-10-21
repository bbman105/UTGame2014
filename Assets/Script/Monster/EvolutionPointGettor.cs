using UnityEngine;
using System.Collections;

public class EvolutionPointGettor
{

    PlayerMonster pm;
    public EvolutionPointGettor(PlayerMonster _pm)
    {
        this.pm = _pm;
    }

    public void GetEvolutionPoint(int _type, int _value)
    {
        switch (_type)
        {
            case 0:
                pm.Natural += _value;
                break;
            case 1:
                pm.Mutation += _value;
                break;
            case 2:
                pm.Wild += _value;
                break;
            default:
                Debug.Log("傳入的進化點數種類無法辨識");
                break;
        }
    }
}
