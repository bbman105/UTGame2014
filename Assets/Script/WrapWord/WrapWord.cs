using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class WrapWord : MonoBehaviour
{

    public static string GetWarpWord(string _string, int _lineWidth)
    {
        StringBuilder sb = new StringBuilder();
        Regex punctuationRegex = new Regex(@"[，。；？~！：‘“”’【】（）]");
        int tempNum = 0;
        char[] c = _string.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] >= 0x4e00 && c[i] <= 0x9fa5)//好像是unicode的編碼範圍
            {
                tempNum += 2;
                if (tempNum > _lineWidth)
                {
                    i--;
                    sb.Append("\n");
                    tempNum = 0;
                }
                else
                {
                    sb.Append(c[i]);
                }
            }
            else
            {
                if (punctuationRegex.IsMatch(c[i].ToString()))
                {
                    tempNum++;
                    if (tempNum > _lineWidth)
                    {
                        i--;
                        sb.Append("\n");
                        tempNum = 0;
                    }
                    else
                    {
                        sb.Append(c[i]);
                    }
                }
                else
                {
                    tempNum++;
                    if (tempNum > _lineWidth)
                    {
                        i--;
                        sb.Append("\n");
                        tempNum = 0;
                    }
                    else
                    {
                        sb.Append(c[i]);
                    }
                }
            }
        }
        /*
        if (c[i] >= 0x4e00 && c[i] <= 0x9fa5)//好像是unicode的編碼範圍
        {
            tempNum += 2;
            if (tempNum > _lineWidth)
            {
                i--;
                sb.Append("\n");
                tempNum = 0;
            }
            else
            {
                sb.Append(c[i]);
            }
        }
        else
        {
            if (punctuationRegex.IsMatch(c[i].ToString()))
            {
                tempNum += 2;
                if (tempNum > _lineWidth)
                {
                    i--;
                    sb.Append("\n");
                    tempNum = 0;
                }
                else
                {
                    sb.Append(c[i]);
                }
            }
            else
            {
                tempNum++;
                if (tempNum > _lineWidth)
                {
                    i--;
                    sb.Append("\n");
                    tempNum = 0;
                }
                else
                {
                    sb.Append(c[i]);
                }
            }
        }
    }
    */
        return sb.ToString();
    }
}
