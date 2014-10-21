
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class ConnectToPhotonServer : MonoBehaviour//, IPhotonPeerListener
{
    //Server位置與名稱設定
    protected string ServerAddress = "scozirge.no-ip.biz:5055";
    protected string ServerApplication = "UTG2014Server";
    //protected PhotonPeer peer;
    public static bool ServerConnected;
    string memberID = ""; // 輸入會員帳號的儲存變數
    string memberPW = ""; // 輸入會員密碼的儲存變數

    // 登入失敗的回傳字串
    public string LoginResult = "";

    void SetConnect()
    {

        ServerConnected = false;//連線初始化
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);//切場景時不清除物件
    }
    // Use this for initialization
    void Start()
    {
        SetConnect();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator wwwGETLogin(string _memberID, string _memberPW)
    {
        string receiveStr = "";
        this.memberID = _memberID;
        this.memberPW = _memberPW;
        WWW www = new WWW(string.Format("http://220.136.60.109:8080/UTG2014Login.aspx?id={0}&LogginID={1}", this.memberID, this.memberPW));
        yield return www;
        if (www.error != null)
        {
            receiveStr = www.error;
            yield return null;
        }
        receiveStr = www.text;
        Debug.Log(receiveStr);
    }

    IEnumerator wwwPOSTLogin(string _memberID, string _memberPW)
    {
        string receiveStr = "";
        this.memberID = _memberID;
        this.memberPW = _memberPW;

        System.Collections.Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");
        string loginData = string.Format("LogginID={0}", this.memberPW);
        byte[] binStr = System.Text.UTF8Encoding.UTF8.GetBytes(loginData);
        WWW www = new WWW("http://scozirge.no-ip.biz:8080/UTG2014Login.aspx", binStr,
        headers);

        yield return www;
        if (www.error != null)
        {
            receiveStr = www.error;
            yield return null;
        }
        receiveStr = www.text;
    }
    public void Login(string _memberID, string _memberPW)
    {
        StartCoroutine(wwwPOSTLogin(_memberID, _memberPW));
    }
}

