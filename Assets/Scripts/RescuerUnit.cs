using System.Collections;
//using System.Numerics;
using UnityEngine;

namespace UnitDetection
{
    public class RescuerUnit : Unit
    {
        public GameObject rescuerPrefab;
        private GameObject instantiatedRescuer;

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
                            if (unit.ToString() == "Fireman(Clone) (UnitDetection.Unit)")
                            {
                                StartRescueTask(rubble);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}