using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameController gameController;
    private PlayerController player;
    private Rigidbody playerRB;
    private new Rigidbody rigidbody;
    private float awakeTime;
    private Vector3 startPos;

    //����ʂ̌W��
    [Min(0f)]
    public float overRun = 1;

    //�ړ����x
    [Min(0f)]
    public float speed = 0.05f;

    //�ǔ����
    public bool IsAttack { get; set; }

    private void Awake()
    {
        //�V�[������v���C���[��GameController���擾
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();

        //�v���C���[��Rigidbody���擾
        playerRB = player.GetComponent<Rigidbody>();
        //���g��Rigidbody���擾
        rigidbody = GetComponent<Rigidbody>();

        //�N�����莞�Ԓ�~����ׂɁA�N������������Ă����B
        awakeTime = Time.time;

        //�ǔ�OFF���ɖ߂�ꏊ�Ƃ��āA�����ʒu������Ă����B
        startPos = transform.position;

        //�����͒ǔ�ON
        IsAttack = true;
    }

    private void FixedUpdate()
    {
        //�N�����b�҂��āA�ʒu���X�V����B�v���C���[���������Ă�����A�X�V����߂�B
        if (!gameController.IsWin && Time.time > awakeTime + 1f)
        {
            //�ǔ���
            if (IsAttack)
            {
                //����ł���ʒu���v�Z���Ĉړ��ʂ�����
                var def = player.transform.position - transform.position;
                var dist = def.magnitude;
                var v = def + overRun * dist * playerRB.velocity;

                v = v.normalized * speed;
                rigidbody.position += v;
            }
            else//�ǔ�OFF��
            {
                //�X�^�[�gposition����A�ړ��ʂ�����
                var v = startPos - transform.position;

                if (v.sqrMagnitude > 0.1f * 0.1f)
                {
                    v = v.normalized * speed;
                    rigidbody.position += v;
                }
            }

        }
    }

    private void Update()
    {
        //�v���C���[�������A���������ȓ�����������
        if (gameController.IsWin)
        {
            transform.LookAt(player.transform);
            transform.Rotate(0, Mathf.Sin(Time.time * 80f) * 4f + Mathf.Sin(Time.time * 7f) * 20f, 0);
        }
        else
        {
            //�i�s�����������B
            if (IsAttack)
            {
                var def = player.transform.position - transform.position;
                var dist = def.magnitude;

                var pos = player.transform.position + dist * overRun * playerRB.velocity;
                transform.LookAt(pos);
            }
            else
            {
                transform.LookAt(startPos);
            }
        }
    }
}
