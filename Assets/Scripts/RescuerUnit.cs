using System.Collections;
using UnityEngine;

public class RescuerUnit : Unit
{
    public void StartRescueTask(Rubble rubble)
    {
        StartCoroutine(PerformRescueTask(rubble));
    }

    private IEnumerator PerformRescueTask(Rubble rubble)
    {
        Debug.Log("Start Rescue!");
        // 消耗行动力并播放挖掘动画
        ActionPoints -= 2;
        // Play digging animation

        // 等待挖掘动画完成
        yield return new WaitForSeconds(2f); // 假设挖掘动画持续2秒

        rubble.OnRescueCompleted();

        _timeSinceLastAction = 0f;
    }

    // 保留Unit脚本中的Update方法来处理行动力恢复
}