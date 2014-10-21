using UnityEngine;
using System.Collections;

public partial class RoomCharaStatus : MonoBehaviour
{


    public void Action(int _action, int _actionCase)
    {
        switch (_action)
        {
            case 0:
                FearCom.SetAction(_actionCase);
                ((CharaTrainFear)FearCom).enabled = false;
                NervousCom.SetAction(_actionCase);
                ((CharaTrainNervous)NervousCom).enabled = false;
                LieCom.SetAction(_actionCase);
                ((CharaTrainLie)LieCom).enabled = false;
                JumpCom.SetAction(_actionCase);
                ((CharaTrainJump)JumpCom).enabled = false;
                break;
            case 1:
                ((CharaTrainFear)FearCom).enabled = true;
                FearCom.SetAction(_actionCase);
                break;
            case 2:
                ((CharaTrainNervous)NervousCom).enabled = true;
                NervousCom.SetAction(_actionCase);
                break;
            case 3:
                ((CharaTrainLie)LieCom).enabled = true;
                LieCom.SetAction(_actionCase);
                break;
            case 4:
                ((CharaTrainJump)JumpCom).enabled = true;
                JumpCom.SetAction(_actionCase);
                break;
        }
    }

}
