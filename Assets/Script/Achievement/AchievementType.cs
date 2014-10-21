using UnityEngine;
using System.Collections;

public class AchievementType {

    public int AchievementTypeID { get; set; }//成就種類ID
    public string AchievementName { get; set; }//成就種類名稱
    public string AchievementDetail { get; set; }//成就種類說明細節

    public AchievementType(int _achievementTypeID,string _achievementName,string _achievementDetail)
    {
        AchievementTypeID = _achievementTypeID;
        AchievementName = _achievementName;
        AchievementDetail = _achievementDetail;
    }
}
