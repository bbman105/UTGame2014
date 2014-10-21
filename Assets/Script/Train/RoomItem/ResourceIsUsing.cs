using UnityEngine;
using System.Collections;

public class ResourceIsUsing : MonoBehaviour
{

    RoomResource rooomResource;
    GameObject parentGameobject;
    bool isAnimationCompleted;
    void Awake()
    {
        rooomResource = transform.parent.FindChild("trigger").GetComponent<RoomResource>();
        parentGameobject = transform.parent.gameObject;
    }

    public void SetNotUsing()//設定金幣已經不再場上
    {
        rooomResource.InUsing = false;
        isAnimationCompleted = true;
    }
    void Update()
    {
        if (isAnimationCompleted)
        {
            isAnimationCompleted = false;
            parentGameobject.SetActive(false);
        }
    }
}
