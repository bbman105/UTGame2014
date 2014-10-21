using UnityEngine;
using System.Collections;

public interface IShowText
{

    void ShowText();
    void ShowText(int choseCase);
    void ShowText(int choseCase, int _feedBackType);
    void UpdateTextDirection();
}
