using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectSpawner
{

    Status receiverStatus;
    Status attackerStatus;
    Transform targetTransform;
    Quaternion rot;
    Vector3 locScale;
    Vector3 pos;
    int effectID;
    GameObject effectSourceObj;
    GameObject effectListObj;
    GameObject areaEffectListObj;
    GameObject effectObj;
    Animator[] animator;
    EffectWork[] effectWrok;
    static int animatorUniqueID;
    public static int AnimatorUniqueID
    {
        get { return EffectSpawner.animatorUniqueID; }
        set { EffectSpawner.animatorUniqueID = value; }
    }
    public static Dictionary<int, Animator> AnimatorDic;
    public EffectSpawner(int _effectID)
    {
        effectID = _effectID;
        //讀特效物物件
        effectSourceObj = Resources.Load<GameObject>("Effect/SkillEffect/SkillEffectObj");
        if (AnimatorDic == null)
            AnimatorDic = new Dictionary<int, Animator>();
        if (effectListObj == null)
            areaEffectListObj = GameObject.FindGameObjectWithTag("AreaEffectList");
    }
    string GetFileName(int _effectID)
    {
        string fileName = "";
        if (GameDictionary.EffectDic.ContainsKey(_effectID))
        {
            fileName = GameDictionary.EffectDic[_effectID];
        }
        else
        {
            fileName = "Unknow EffectID";
            Debug.Log("嘗試讀取不存在特效字典的特效ID");
        }
        return fileName;
    }

    public void SpawnTargetEffect(Status _attacker, Status _receiver)
    {
        attackerStatus = _attacker;
        receiverStatus = _receiver;
        targetTransform = receiverStatus.transform;
        effectListObj = targetTransform.FindChild("SkillEffectList").gameObject;
        animator = effectListObj.GetComponentsInChildren<Animator>();
        effectWrok = effectListObj.GetComponentsInChildren<EffectWork>();
        rot = Quaternion.Euler(0f, 0f, 0f);
        pos = Vector3.zero;
        //設定動畫
        for (int i = 0; i < animator.Length; i++)
        {
            if (i == animator.Length - 1 && effectWrok[i].IsUsing)
            {
                effectObj = GameObject.Instantiate(effectSourceObj, pos, rot) as GameObject;
                //設定動畫
                effectObj.GetComponent<Animator>().SetTrigger(GetFileName(effectID));
                effectObj.GetComponent<EffectWork>().InstantiateUsing();
                effectObj.transform.parent = effectListObj.transform;
                AniEndSet();
                break;
            }
            else
            {
                if (!effectWrok[i].IsUsing)
                {
                    effectWrok[i].IsUsing = true;
                    animator[i].SetTrigger(GetFileName(effectID));
                    effectObj = animator[i].gameObject;
                    AniEndSet();
                    break;
                }
            }
        }
    }
    public int SpawnTargetLoopEffect(Status _receiver)
    {
        attackerStatus = _receiver;
        receiverStatus = _receiver;
        targetTransform = receiverStatus.transform;
        effectListObj = targetTransform.FindChild("SkillEffectList").gameObject;
        animator = effectListObj.GetComponentsInChildren<Animator>();
        effectWrok = effectListObj.GetComponentsInChildren<EffectWork>();
        rot = Quaternion.Euler(0f, 0f, 0f);
        pos = Vector3.zero;
        //設定動畫
        for (int i = 0; i < animator.Length; i++)
        {
            if (i == animator.Length - 1 && effectWrok[i].IsUsing)
            {
                effectObj = GameObject.Instantiate(effectSourceObj, pos, rot) as GameObject;
                //設定動畫
                effectObj.GetComponent<Animator>().SetTrigger(GetFileName(effectID));
                effectObj.GetComponent<EffectWork>().InstantiateUsing();
                effectObj.transform.parent = effectListObj.transform;
                AniEndSet();
                return AnimatorUniqueID;
            }
            else
            {
                if (!effectWrok[i].IsUsing)
                {
                    effectWrok[i].IsUsing = true;
                    animator[i].SetTrigger(GetFileName(effectID));
                    effectObj = animator[i].gameObject;
                    AniEndSet();
                    return AnimatorUniqueID;
                }
            }
        }
        AnimatorUniqueID++;
        return AnimatorUniqueID;
    }
    void AniEndSet()
    {
        effectListObj.transform.localPosition = new Vector3(0, -((1.7f - receiverStatus.Size) / 1.2f) * 0.2f, 0);
        effectListObj.transform.localScale = new Vector3(receiverStatus.Size, receiverStatus.Size, 1);
        effectObj.transform.localScale = new Vector3(attackerStatus.ForceDir, targetTransform.localScale.y, targetTransform.localScale.z);
        effectObj.transform.localPosition = pos;
        AnimatorUniqueID++;
        if (!AnimatorDic.ContainsKey(AnimatorUniqueID))
            AnimatorDic.Add(AnimatorUniqueID, effectObj.GetComponent<Animator>());
        else
            Debug.Log("特效獨佔ID重複!");
    }
    void AreaAniEndSet()
    {
        effectObj.transform.localScale = new Vector3(attackerStatus.ForceDir, targetTransform.localScale.y, targetTransform.localScale.z);
        effectObj.transform.position = pos;
        AnimatorUniqueID++;
        if (!AnimatorDic.ContainsKey(AnimatorUniqueID))
            AnimatorDic.Add(AnimatorUniqueID, effectObj.GetComponent<Animator>());
        else
            Debug.Log("特效獨佔ID重複!");
    }
    public void SpawnAreaEffect(Status _attacker, Status _receiver)
    {
        attackerStatus = _attacker;
        receiverStatus = _receiver;
        targetTransform = receiverStatus.transform;

        rot = Quaternion.Euler(0f, 0f, 0f);
        pos = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
        animator = areaEffectListObj.GetComponentsInChildren<Animator>();
        effectWrok = areaEffectListObj.GetComponentsInChildren<EffectWork>();

        //設定動畫
        for (int i = 0; i < animator.Length; i++)
        {
            if (i == animator.Length - 1 && effectWrok[i].IsUsing)
            {
                effectObj = GameObject.Instantiate(effectSourceObj, pos, rot) as GameObject;
                //設定動畫
                effectObj.GetComponent<Animator>().SetTrigger(GetFileName(effectID));
                effectObj.GetComponent<EffectWork>().InstantiateUsing();
                effectObj.transform.parent = areaEffectListObj.transform;
                AreaAniEndSet();
                break;
            }
            else
            {
                if (!effectWrok[i].IsUsing)
                {
                    effectWrok[i].IsUsing = true;
                    animator[i].SetTrigger(GetFileName(effectID));
                    effectObj = animator[i].gameObject;
                    AreaAniEndSet();
                    break;
                }
            }
        }
    }



    public void SpawnPointEffect(Collision2D _target)//目標地點特效
    {
        ContactPoint2D contact = _target.contacts[0];
        rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        pos = contact.point;
        animator = areaEffectListObj.GetComponentsInChildren<Animator>();
        effectWrok = areaEffectListObj.GetComponentsInChildren<EffectWork>();

        //設定動畫
        for (int i = 0; i < animator.Length; i++)
        {
            if (i == animator.Length - 1 && effectWrok[i].IsUsing)
            {
                effectObj = GameObject.Instantiate(effectSourceObj, pos, rot) as GameObject;
                //設定動畫
                effectObj.GetComponent<Animator>().SetTrigger(GetFileName(effectID));
                effectObj.GetComponent<EffectWork>().InstantiateUsing();
                effectObj.transform.parent = areaEffectListObj.transform;
                PointAniEndSet();
                break;
            }
            else
            {
                if (!effectWrok[i].IsUsing)
                {
                    effectWrok[i].IsUsing = true;
                    animator[i].SetTrigger(GetFileName(effectID));
                    effectObj = animator[i].gameObject;
                    PointAniEndSet();
                    break;
                }
            }
        }
    }
    void PointAniEndSet()
    {
        effectObj.transform.position = pos;
        AnimatorUniqueID++;
        if (!AnimatorDic.ContainsKey(AnimatorUniqueID))
            AnimatorDic.Add(AnimatorUniqueID, effectObj.GetComponent<Animator>());
        else
            Debug.Log("特效獨佔ID重複!");
    }
}
