using UnityEngine;
using System.Collections;

public class HealthTactics:Tactics {

    float HealthPercent { get; set; }

    public HealthTactics(string _health)
    {
        try
        {
            HealthPercent = float.Parse(_health.Replace("%", "")) / 100;
        }
        catch
        {
            Debug.Log("關卡事件的戰術參數格式錯誤");
        }
    }
    
}
