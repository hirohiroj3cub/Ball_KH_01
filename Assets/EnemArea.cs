using UnityEngine;

public class EnemArea : MonoBehaviour
{
    private EnemyController[] enemies;

    private void Awake()
    {
        //�G���A�̑ΏۂƂȂ�A�q�ɂȂ��Ă���EnemyController���擾
        enemies = GetComponentsInChildren<EnemyController>();
    }

    private void Start()
    {
        //�Ώ�EnemyController�̒ǔ���OFF
        foreach (var e in enemies)
        {
            e.IsAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player���G���A�ɓ�������A�Ώ�EnemyController�̒ǔ���ON
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
        //Player���G���A����o����A�Ώ�EnemyController�̒ǔ���OFF
        if (other.CompareTag("Player"))
        {
            foreach (var e in enemies)
            {
                e.IsAttack = false;
            }
        }
    }
}
