using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour
{
    [SerializeField] private GameObject survivorPrefab;
    [SerializeField] private float survivorProbability = 0.5f;

    private bool hasSurvivor;

    private void Start()
    {
        hasSurvivor = UnityEngine.Random.value < survivorProbability;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log("按Q有用");

            if (hit.collider != null)
            {
                Rubble rubble = hit.collider.GetComponent<Rubble>();
                if (rubble != null)
                {
                    Debug.Log("检测到了rubble");
                    OnRescue();
                }
            }
        }
    }

    private void OnRescue()
    {
        if (UnitControlSystem.Instance.IsRescuerSelected())
        {
            foreach (Unit unit in UnitControlSystem.Instance.selectedUnitList)
            {
                if (unit is RescuerUnit rescuerUnit)
                {
                    rescuerUnit.StartRescueTask(this);
                    break;
                }
            }
        }
    }

    public void OnRescueCompleted()
    {
        if (hasSurvivor)
        {
            // 生成幸存者并设置其位置
            GameObject survivor = Instantiate(survivorPrefab, transform.position, Quaternion.identity);
        }

        // 销毁废墟
        Destroy(gameObject);
    }
}