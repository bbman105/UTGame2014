using UnityEngine;
using System.Collections;

public class TrainSceneUICtrl : TrainSceneInterfaceUI
{
    

    void Start()
    {
        //StartSetUI();
    }


    void LoadScripts()
    {
        gameObject.AddComponent<RoomCtrl>();
    }
}
