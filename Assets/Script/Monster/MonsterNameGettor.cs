using UnityEngine;
using System.Collections;

public class MonsterNameGettor
{

    PlayerMonster pm;
    public MonsterNameGettor(PlayerMonster _pm)
    {
        this.pm = _pm;
    }

    public void GetName(string _value)
    {
        if (_value.Length < 7)
        {
            pm.MonsterName = _value;
        }
        else if (_value.Length < 0)
        {
            Debug.Log("修改怪物名稱時，輸入的字元數目為空，導致修改失敗");
        }
        else
        {
            Debug.Log("修改怪物名稱時，輸入的字元數目大於7，導致修改失敗");
        }
    }
}
