using UnityEngine;
using System.Collections;

public class CloseEvolutionUI : TrainSceneInterfaceUI
{

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(4))
        {
            EvolutionLoad.NextEvolution();//查看是否有其他進化排程，若有則進行其他怪獸的進化
        }
    }
    public static void CloseEvoUI()//關閉進化介面
    {
        Debug.Log("關閉進化介面");
        evoUI.SetActive(false);
        CtrlPower.StartCtrl(2);//將控制權設定為任務選擇介面
    }
}
