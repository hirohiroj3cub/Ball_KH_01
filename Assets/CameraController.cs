using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameController gameController;
    public Transform target;

    /// <summary>
    /// �ړ��ʂ̌W���B0�ɋ߂��ƃJ�����͂������߂Â��B
    /// </summary>
    [Range(0f, 1f)]
    public float par = 0.5f;
    /// <summary>
    /// �ڕW�ƂȂ鋗��
    /// </summary>
    private float offsetDist;
    /// <summary>
    /// �ڕW�ƂȂ鍂��
    /// </summary>
    private float offsetHeight;

    private void Awake()
    {
        //�Q�[���I�[�o�[���m�ׂ̈ɁAGameController���V�[������擾
        gameController = FindObjectOfType<GameController>();

        //������Ԃ̃J�����ƃv���C���[�̈ʒu�֌W����A�ڕW�ƂȂ鍂���Ƌ������擾�B
        var offset = transform.position - target.position;
        offsetDist = offset.magnitude;
        offsetHeight = offset.y;
    }

    private void FixedUpdate()
    {
        //�ڕW�̍����Ƌ�������A�J�����̈ړ��ʂ��v�Z
        var def = transform.position - target.position;
        def.y = 0;
        def = def.normalized * Mathf.Sqrt(offsetDist * offsetDist - offsetHeight * offsetHeight);
        var targetPos = target.position + Vector3.up * offsetHeight + def;
        transform.position += (targetPos - transform.position) * par;
    }

    private void Update()
    {
        //�J�����̌������^�[�Q�b�g�Ɍ�����B
        transform.LookAt(target);

        //�Q�[���I�[�o�[���A�J������������]������B
        if (gameController.IsEnd)
        {
            transform.position += transform.right * 0.05f;
        }
    }
}
