using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Text scoreLabel;
    public GameObject winnerUI;
    public GameObject gameoverUI;
    public string nextStage;

    public AudioSource stageBGM;
    public AudioSource gameoverBGM;
    public AudioSource stageClearBGM;

    private ItemController[] items;

    public bool IsWin { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsEnd => IsWin || IsGameOver;

    private void Awake()
    {
        //�����l��ݒ�
        IsWin = false;
        IsGameOver = false;

        //�A�C�e����S�Ď擾
        items = FindObjectsOfType<ItemController>();

        //UI��������
        winnerUI.SetActive(false);
        gameoverUI.SetActive(false);

        //BGM���Đ�
        stageBGM.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        //�v���O���������₷���悤�ɁA�������֐��ŕ���
        GameUpdate();
        WaitContinue();
    }

    /// <summary>
    /// �Q�[���v���C�Ɋւ���X�V
    /// </summary>
    private void GameUpdate()
    {
        //�Q�[�����I����Ă����牽�����Ȃ�
        if (IsEnd) return;

        //UI�X�V
        var count = items.Count(i => i.IsGetted);
        scoreLabel.text = $"{count} / {items.Length}";

        //�Q�[���N���A����
        if (!IsWin && count == items.Length)
        {
            IsWin = true;
            //UI�̕\��
            winnerUI.SetActive(true);

            //BGM�ύX
            stageBGM.Stop();
            stageClearBGM.Play();
        }
    }

    /// <summary>
    /// ���X�^�[�g��X�e�[�W�ړ��Ɋւ���X�V
    /// </summary>
    private void WaitContinue()
    {
        //�Q�[�����I����Ă��Ȃ������牽�����Ȃ�
        if (!IsEnd) return;

        //���葀�삪��������
        if (Input.GetButton("Submit"))
        {
            //�Q�[���I�[�o�[�܂��͎��̃X�e�[�W���ݒ肳��Ă��Ȃ���΁A���X�^�[�g
            if (IsGameOver || string.IsNullOrWhiteSpace(nextStage))
            {
                var index = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(index);
            }
            else //�����łȂ��ꍇ�A���̃X�e�[�W��
            {
                SceneManager.LoadScene(nextStage);
            }
        }
    }

    /// <summary>
    /// �O������Q�[���I�[�o�[��m�点��ׂ̊֐�
    /// </summary>
    public void GameOver()
    {
        //�Q�[�����I�����Ă����牽�����Ȃ�
        if (IsEnd) return;

        IsGameOver = true;

        //BGM�؂�ւ�
        stageBGM.Stop();
        gameoverBGM.Play();

        //UI�̕\��
        gameoverUI.SetActive(true);
    }
}
