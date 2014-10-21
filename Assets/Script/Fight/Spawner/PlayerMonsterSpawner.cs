using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMonsterSpawner : MonoBehaviour, ISpawn
{

    GameObject[] chara;
    GameObject charaObj;
    const byte CharaMaxNum = 3;
    int teamID;
    Vector3[] monsterPos;
    Quaternion monsterRot;
    string monsterTag;
    int monsterLayer;
    public int SpawnMonsterID { get; set; }

    public void PlayerMonsterSpawnerSet(int _teamID, bool _p1)
    {
        if (_p1)
        {
            //設定怪物建立位置
            monsterPos = new Vector3[]{
            new Vector3(-1,4.5f,0),
            new Vector3(-2,4.5f,0),
            new Vector3(-3,4.5f,0)
            };
            //設定怪物建立角度
            monsterRot = Quaternion.Euler(0f, 0f, 0f);
            //設定讀取怪物物件
            charaObj = Resources.Load<GameObject>("GameObject/FightScene/Chara/chara");
            //建立怪物GameObject列表
            chara = new GameObject[3];
            monsterTag = "PlayerMonster";
            monsterLayer = 13;//13 is PlayerMonster
            teamID = _teamID;
        }

    }


    public void Spawn()
    {
        for (byte i = 0; i < CharaMaxNum; i++)
        {
            if (Player.TeamDic[teamID].Member[i] != 0)
            {
                //建立怪物GameObject                
                chara[i] = Instantiate(charaObj, monsterPos[i], monsterRot) as GameObject;
                //設定怪物標籤
                chara[i].tag = monsterTag;
                //設定怪物圖層
                chara[i].layer = monsterLayer;
                //給予物件名稱
                chara[i].name = "PMonster" + (i + 1);
                //設定怪物的的ID並讀取怪物資料
                SpawnMonsterID = Player.TeamDic[teamID].Member[i];
                chara[i].GetComponent<Status>().SetMonsterAttribute(SpawnMonsterID,new SetPlayerMonsterAttribute());
                chara[i].GetComponent<Status>().MovSetup();
                //加入腳色清單
                if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                {
                    ForceCondition.CharaDic.Add(monsterTag, new List<GameObject>());
                }
                ForceCondition.CharaDic[monsterTag].Add(chara[i]);
                if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                {
                    ForceCondition.CharaDic.Add(monsterTag, ForceCondition.CharaDic[monsterTag]);
                }

            }
        }
    }

}
