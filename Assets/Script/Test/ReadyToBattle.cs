using UnityEngine;
using System.Collections;

public class ReadyToBattle : MonoBehaviour
{

    Transform targetTransform;
    Animator targetAnimator;
    void OnClick()
    {
        Debug.Log("StartBattle");
        targetTransform = GameObject.FindGameObjectWithTag("ReadyToBattle").transform;
        targetAnimator = targetTransform.GetComponent<Animator>();
        targetAnimator.SetTrigger("battle_start1");
    }
}
