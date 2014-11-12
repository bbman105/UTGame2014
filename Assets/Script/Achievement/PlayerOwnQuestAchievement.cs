using UnityEngine;
using System.Collections;

public class PlayerOwnQuestAchievement
{

    public bool[] Achieve;//達成了那些成就，[0]=完成成就，[1]=探索成就....
    public int[] AchievementItemID;
    public PlayerOwnQuestAchievement(int _achievementItemID, int _achievementTypeID)
    {
        Achieve = new bool[5];
        AchievementItemID = new int[5];
        AchievementItemID[_achievementTypeID] = _achievementItemID;
        Achieve[_achievementTypeID] = true;
    }

}
