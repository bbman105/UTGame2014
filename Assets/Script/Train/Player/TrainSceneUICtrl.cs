using UnityEngine;
using System.Collections;

public class TrainSceneUICtrl : TrainSceneInterfaceUI
{

    void LoadScripts()
    {
        gameObject.AddComponent<RoomCtrl>();
    }
}
