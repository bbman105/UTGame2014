using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{

    void Logout()//登出
    {
        //執行ArcaletGame.Dispose可以讓連線中的ArcaletGame斷開連結
        //若您所宣告的ArcaletGame為null，在執行Dispose時將會出現NullException
        //我們可以先做個if來判斷
        if (arcaletGame != null)
        {
            Debug.LogWarning("Log out");
            arcaletGame.Dispose();
            arcaletGame = null;
            onLogin = false;
            //barLeagth = 0;
        }
    }
}
