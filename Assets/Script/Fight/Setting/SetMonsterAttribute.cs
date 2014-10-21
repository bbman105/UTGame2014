using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public abstract class SetMonsterAttribute
{
    protected Status status;//怪獸屬性
    public abstract void SetAttribute(int _monsterID, Status _status);
    protected static int tmpMonsterID = 0;
}
