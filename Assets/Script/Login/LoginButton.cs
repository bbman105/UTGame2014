using UnityEngine;
using System.Collections;

public class LoginButton : MonoBehaviour
{

    ConnectToPhotonServer ConnectToServer;
    UILabel IDLAbel;
    UILabel PWLabel;
    string memberID = ""; // 輸入會員帳號的儲存變數
    string memberPW = ""; // 輸入會員密碼的儲存變數

    void Awake()
    {
        ConnectToServer = GameObject.FindGameObjectWithTag("ConnectToServer").GetComponent<ConnectToPhotonServer>();
        IDLAbel = transform.parent.FindChild("ID").FindChild("InputID").GetComponent<UILabel>();
        PWLabel = transform.parent.FindChild("PW").FindChild("InputPW").GetComponent<UILabel>();
    }
    void OnClick()
    {
        memberID = IDLAbel.text;
        memberPW = PWLabel.text;
        ConnectToServer.Login(memberID, memberPW);
    }
}
