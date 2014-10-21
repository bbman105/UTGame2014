using UnityEngine;
using System.Collections;

public class GetPlayerMonsterSpellRank : RandomSpellGetor
{


    //亂數抽出玩家怪物的第幾排序技能，50%機率施放第一招，30%機率施放第二招，20%機率施放第3招
    public override int GetNumber()
    {
        /*//暫時改為100%觸發第一招
        RandomNum = Random.Range(1, 11);
        if (RandomNum < 6)
        {
            SelectSpellRank = 0;
        }
        else if (RandomNum > 5 && RandomNum < 9)
        {
            SelectSpellRank = 1;
        }
        else if (RandomNum > 8 && RandomNum < 11)
        {
            SelectSpellRank = 2;
        }
        */
        SelectSpellRank = 0;
        return SelectSpellRank;
    }

}
