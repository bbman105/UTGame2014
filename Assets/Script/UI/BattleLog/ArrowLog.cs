using UnityEngine;
using System.Collections;

public class ArrowLog : MonoBehaviour
{


    Animator animArrow;
    Animator animRightArrow;
    Animator animLeftArrow;

    public void StartSet()
    {
        animRightArrow = transform.FindChild("AniRight").GetComponent<Animator>();
        animLeftArrow = transform.FindChild("AniLeft").GetComponent<Animator>();
        animArrow = GetComponent<Animator>();
    }

    public void PlayAni(string doEvent)
    {
        animArrow.SetTrigger(doEvent);
    }
    public void PlayArrowAni(string doEvent)
    {
        animRightArrow.SetTrigger(doEvent);
        animLeftArrow.SetTrigger(doEvent);
    }
}
