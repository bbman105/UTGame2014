using UnityEngine;
using System.Collections;

public class ActiveFunction : MonoBehaviour {

    public GameObject ActiveTarget;
    public GameObject InActiveTarget;
    public Component InActivityComponent;

    public void ActiveTargetAction()
    {
        if (ActiveTarget!=null)
            ActiveTarget.SetActive(true);
    }

    public void InActiveTargetAction()
    {
        if (InActiveTarget != null)
        {
            InActiveTarget.SetActive(false);
        }
    }
    public void InActiveComponent()
    {
        
    }
}
