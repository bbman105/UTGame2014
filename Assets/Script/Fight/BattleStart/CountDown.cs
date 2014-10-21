using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour
{


    //文字及倒數
    static Transform myTransform;
    static Animator numberAni;
    static bool startCount;
    SpriteRenderer NumberSprites;
    Sprite[] numberSprite;

    int CountNumber;
    //戰鬥開始動畫
    Transform targetTransform;
    Animator targetAnimator;

    public static void StartCount()
    {
        startCount = true;
    }
    void Update()
    {
        if (startCount)
        {
            StartCoroutine("TimerFunction");
            startCount = false;
        }
    }
    void BattleStart()
    {
        MemberArrow.RestrictCondition = 1;//結束倒數，也代表不能再控制預設出戰怪物
        MemberArrow.MonsterGoStay();//未設定為預設出戰的怪物取消顯示
        targetAnimator.SetTrigger("battle_start1");
    }
    IEnumerator TimerFunction()
    {
        while (CountNumber > 0)
        {
            yield return new WaitForSeconds(1);
            CountNumber--;
            if (CountNumber > 0)
            {
                NumberSprites.sprite = numberSprite[CountNumber];
                numberAni.SetTrigger("StartCount");
            }
            else
            {
                NumberSprites.enabled = false;
                BattleStart();
            }
        }
    }

    void Start()
    {
        myTransform = transform;
        numberAni = myTransform.GetComponent<Animator>();
        //讀取數字sprite
        NumberSprites = myTransform.GetComponent<SpriteRenderer>();
        numberSprite = new Sprite[10];
        for (int i = 0; i < numberSprite.Length; i++)
        {
            numberSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/Font/Number/combo{0}", i));
        }
        CountNumber = 3;
        NumberSprites.sprite = numberSprite[3];
        //戰鬥開始動畫
        targetTransform = GameObject.FindGameObjectWithTag("ReadyToBattle").transform;
        targetAnimator = targetTransform.GetComponent<Animator>();

    }

}
