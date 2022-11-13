using UnityEngine;

public class ItemController : MonoBehaviour
{
    public AudioSource se1;
    public AudioSource se2;
    private GameController gameController;
    private MeshRenderer meshRenderer;
    public bool IsGetted { get; private set; }

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        //���g��MeshRenderer�i�`��@�\�j���擾
        meshRenderer = GetComponent<MeshRenderer>();
        IsGetted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //���Ɏ擾����Ă�����Ȃɂ����Ȃ�
        if (IsGetted) return;

        //�Q�[�����I�����Ă��Ȃ��A���v���C���[�ƏՓ˂�����擾����
        if (!gameController.IsEnd && other.CompareTag("Player"))
        {
            IsGetted = true;

            //���b�V�����\����
            meshRenderer.enabled = false;

            //SE���Đ�
            se1.Play();
            se2.Play();
        }
    }
}
