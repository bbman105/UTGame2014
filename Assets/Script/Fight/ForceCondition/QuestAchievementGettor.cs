using UnityEngine;
using System.Collections;

public class QuestAchievementGettor  {


    AchievementGettor AchievementGettor;
    public QuestAchievementGettor(AchievementGettor _achievementType)
    {
        AchievementGettor = _achievementType;//設定成就取得種類
    }
    public void CheckAchieveCondition()
    {
        AchievementGettor.GetAchievement();
    }
    public void SetAchievementType(AchievementGettor _achievementType)//設定成就取得種類
    {
        AchievementGettor = _achievementType;//設定成就取得種類
    }
}
