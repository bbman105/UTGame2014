using UnityEngine;
using System.Collections;

public class PlayerOwnQuestAchievement  {

    public bool[] Achieve;//達成了那些成就，[0]=完成成就，[1]=探索成就....
    public PlayerOwnQuestAchievement(int _achievementTypeID, bool[] _achieve)
    {
        Achieve = _achieve;
        Achieve[_achievementTypeID-1] = true;
    }

}
