using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitDetection;

public class Rubble : MonoBehaviour
{
    [SerializeField] private GameObject survivorPrefab;
    [SerializeField] private float survivorProbability = 0.5f;

    private bool hasSurvivor;

    public GameObject rescuerPrefab;
    private GameObject instantiatedRescuer;
    private RescuerUnit rescuerUnit;

    private void Start()
    {
        hasSurvivor = UnityEngine.Random.value < survivorProbability;
        RescuerUnit rescuerUnit = new RescuerUnit();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log("按Q有用");

            if (hit.collider != null)
            {
                Rubble rubble = hit.collider.GetComponent<Rubble>();
                //RescuerUnit rescuerUnit = new RescuerUnit();
                if (rubble != null)
                {
                    UnitControlSystem unitControlSystem = UnitControlSystem.Instance;
                    foreach (Unit unit in unitControlSystem.selectedUnitList)
                    {
                        if (unit is rescuerUnit || unit.ToString() == "Fireman(Clone) (UnitDetection.Unit)")
                        {
                            //instantiatedRescuer = Instantiate(rescuerPrefab);
                            //rescuerUnit = instantiatedRescuer.AddComponent<RescuerUnit>();
                            rescuerUnit.StartRescueTask(this);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnRescue()
    {
        Debug.Log("正常启动OnRescue");
        if (UnitControlSystem.Instance.IsRescuerSelected())
        {
            Debug.Log("开始挖掘！");
            foreach (Unit unit in UnitControlSystem.Instance.selectedUnitList)
            {
                if (unit.ToString() == "Fireman(Clone) (UnitDetection.Unit)")
                {
                    instantiatedRescuer = Instantiate(rescuerPrefab);
                    rescuerUnit = instantiatedRescuer.AddComponent<RescuerUnit>();
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