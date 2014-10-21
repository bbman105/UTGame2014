using UnityEngine;
using System.Collections;

public class ItemInUsing : MonoBehaviour
{

    ItemTake itemTake;
    GameObject parentGameobject;
    bool isAnimationCompleted;
    void Awake()
    {
        itemTake = transform.parent.FindChild("trigger").GetComponent<ItemTake>();
        parentGameobject = transform.parent.gameObject;
    }

    public void SetNotUsing()//設定金幣已經不再場上
    {
        itemTake.InUsing = false;
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
