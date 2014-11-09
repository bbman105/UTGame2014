using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnlockQuest
{
    public int PlanetID { get; set; }//這是哪一個星球的解鎖任務
    public int UnlockMap { get; set; }//解鎖的是哪個地圖
    public Dictionary<int, int> MapQuestLevel;//地圖解鎖到第幾關
    public Dictionary<int, int> MapQuesItem;//冒險Item的ID字典
    public UnlockQuest(int _ventureID, int _planetID, int _unlockMap, int _unlockQuestLevel)
    {
        MapQuestLevel = new Dictionary<int, int>();
        MapQuesItem = new Dictionary<int, int>();
        PlanetID = _planetID;
        UnlockMap = _unlockMap;
        MapQuestLevel.Add(_unlockMap, _unlockQuestLevel);
        MapQuesItem.Add(_unlockMap, _ventureID);
    }
}
