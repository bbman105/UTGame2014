using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageControler
{
    public static List<Status> RemainMemberList;
    static List<Status> OnFightMemberList;
    private static int onFightMinNumber;
    public static int OnFightMinNumber//出戰怪最小數量，不可低於1
    {
        get { return onFightMinNumber; }
        set
        {
            if (value < 1)
                onFightMinNumber = 1;
            else
                onFightMinNumber = value;
        }
    }
    public static int OnFightMaxNumber { get; set; }//出戰怪最大數量
    private static int AlreadyOnFightMember;//目前出戰數量

    public static void SetRemainMember()
    {
        //將AddNpcMonsterCheck方法加入有人死亡的委託事件委派
        ForceCondition.UpdateNPCDeath += AddNpcMonsterCheck;//"檢查是否要補怪獸"加到委託事件中
        OnFightMemberList = new List<Status>();
        RemainMemberList = new List<Status>();
        AlreadyOnFightMember = 0;
        for (int i = 0; i < ForceCondition.CharaStatusDic["NPCMonster"].Length; i++)
        {
            Status status = ForceCondition.CharaStatusDic["NPCMonster"][i];
            if (AlreadyOnFightMember < OnFightMaxNumber)
            {
                OnFightMemberList.Add(status);//將此隊員狀態加入字典
                AlreadyOnFightMember++;
            }
            else
            {
                status.gameObject.SetActive(false);
                RemainMemberList.Add(status);
            }
        }
    }
    public static void AddNpcMonsterCheck()//補怪獸
    {
        if (RemainMemberList.Count > 0)//如果還有怪物沒有出
        {
            for (int i = 0; i < OnFightMemberList.Count; i++)
            {
                if (!OnFightMemberList[i].Alive)
                {
                    OnFightMemberList.Remove(OnFightMemberList[i]);
                    int rndNum = Random.Range(0, RemainMemberList.Count);
                    OnFightMemberList.Add(RemainMemberList[rndNum]);
                    RemainMemberList[rndNum].gameObject.SetActive(true);
                    SetCantCollideToWall(RemainMemberList[rndNum]);
                    RemainMemberList[rndNum].transform.position = new Vector3(4.5f, 3, 0);//設定為出場位置
                    RemainMemberList.Remove(RemainMemberList[rndNum]);
                    break;
                }
            }
        }
    }
    static void SetCantCollideToWall(Status _status)//設定怪獸進場，不受到周圍碰撞影響，且擁有剛體狀態，不會被擊退
    {

        _status.rigidStatus.SetEnterRigid();//給予鋼體狀態
        _status.gameObject.layer = LayerMask.NameToLayer("P2CharaEnter");//設定圖層為可穿透牆壁
    }
}
