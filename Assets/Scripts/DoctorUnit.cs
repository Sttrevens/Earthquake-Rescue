using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorUnit : Unit
{
    public float healRange = 2f; // 治疗范围
    public float healDuration = 2f; // 治疗时长

    public void StartHealTask(Survivor survivor)
    {
        if (Vector3.Distance(transform.position, survivor.transform.position) <= healRange)
        {
            StartCoroutine(Heal(survivor));
        }
    }

    private IEnumerator Heal(Survivor survivor)
    {
        // 播放治疗动画
        // animator.SetTrigger("Heal");

        // 治疗过程
        yield return new WaitForSeconds(healDuration);

        // 治疗结束，改变幸存者状态
        survivor.Healed();

        // 恢复行动力属性值
        // RestoreActionPoints();
    }
}