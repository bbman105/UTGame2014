using UnityEngine;
using System.Collections;

public class BattleEnd : MonoBehaviour
{

    Animator animBattleEnd;


    public void PlayBattleEndAni(string doEvent)
    {
        animBattleEnd = transform.GetComponent<Animator>();
        animBattleEnd.SetTrigger(doEvent);
    }

    // Use this for initialization
    void Start()
    {
        animBattleEnd = transform.GetComponent<Animator>();
    }

}
