using UnityEngine;

/// <summary>
/// Trivial script that fills the label's contents gradually, as if someone was typing.
/// </summary>

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class Typewriter : MonoBehaviour
{
    public int charsPerSecond = 40;

    UILabel myLabel;
    string mText;
    int mOffset = 0;
    float mNextChar = 0f;
    public bool type = false;

    void Update()
    {
        if(type)
        {
            if (myLabel == null)
            {
                myLabel = GetComponent<UILabel>();
                myLabel.supportEncoding = false;
                myLabel.symbolStyle = NGUIText.SymbolStyle.None;
                mText = myLabel.processedText;
            }

            if (mOffset < mText.Length)
            {
                if (mNextChar <= RealTime.time)
                {
                    charsPerSecond = Mathf.Max(1, charsPerSecond);

                    // Periods and end-of-line characters should pause for a longer time.
                    float delay = 1f / charsPerSecond;
                    char c = mText[mOffset];
                    if (c == '.' || c == '\n' || c == '!' || c == '?') delay *= 4f;

                    mNextChar = RealTime.time + delay;
                    myLabel.text = mText.Substring(0, ++mOffset);
                }
            }
            else
            {
                mOffset = 0;
                type = false;
            }
        }
        //else Destroy(this);
    }
}
