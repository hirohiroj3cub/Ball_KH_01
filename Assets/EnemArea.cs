using UnityEngine;

public class EnemArea : MonoBehaviour
{
    private EnemyController[] enemies;

    private void Awake()
    {
        //エリアの対象となる、子になっているEnemyControllerを取得
        enemies = GetComponentsInChildren<EnemyController>();
    }

    private void Start()
    {
        //対象EnemyControllerの追尾をOFF
        foreach (var e in enemies)
        {
            e.IsAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Playerがエリアに入ったら、対象EnemyControllerの追尾をON
        if (other.CompareTag("Player"))
        {
            foreach (var e in enemies)
            {
                e.IsAttack = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Playerがエリアから出たら、対象EnemyControllerの追尾をOFF
        if (other.CompareTag("Player"))
        {
            foreach (var e in enemies)
            {
                e.IsAttack = false;
            }
        }
    }
}
