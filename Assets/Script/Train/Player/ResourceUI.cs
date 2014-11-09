using UnityEngine;
using System.Collections;

public class ResourceUI : TrainSceneInterfaceUI
{
    static bool UISet;

    void Start()
    {
        //StartSetUI();
        UISet = true;
        UpdateResourceUI();
    }

    public static void UpdateResourceUI()
    {
        if (UISet)
        {
            goldLabel.text = Player.OwnResource.Gold.ToString();
            dimandLabel.text = Player.OwnResource.Dimand.ToString();
            energyLabel.text = Player.OwnResource.CurEnergy.ToString();
        }

    }

}
