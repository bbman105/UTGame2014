using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour {
    KnockBackCalculator knockCalculator;//擊飛計算器
    ElementHitCalculator elementCalculator;//元素相剋計算器
    DominantSizeCalculator dominantSizeCalculator;//體型優勢計算器
    SizeReduceSpeedCalculator sizeReduceSpeedCalculator;//體型降低速度計算器
    public void StartSetCharaCalculator()//初始化計算器
    {
        knockCalculator = new KnockBackCalculator();//取得擊飛力道計算器
        elementCalculator = new ElementHitCalculator();//建立元素打擊計算器
        dominantSizeCalculator = new DominantSizeCalculator();//建立體型優勢計算器
        sizeReduceSpeedCalculator = new SizeReduceSpeedCalculator();//建立體型降低速度計算器
    }
}
