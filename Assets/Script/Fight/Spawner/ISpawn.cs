using UnityEngine;
using System.Collections;

public interface ISpawn
{

    void Spawn();
    int SpawnMonsterID { get; set; }
}
