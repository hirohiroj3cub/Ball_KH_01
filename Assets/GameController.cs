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
        //初期値を設定
        IsWin = false;
        IsGameOver = false;

        //アイテムを全て取得
        items = FindObjectsOfType<ItemController>();

        //UIを初期化
        winnerUI.SetActive(false);
        gameoverUI.SetActive(false);

        //BGMを再生
        stageBGM.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        //プログラムが見やすいように、処理を関数で分割
        GameUpdate();
        WaitContinue();
    }

    /// <summary>
    /// ゲームプレイに関する更新
    /// </summary>
    private void GameUpdate()
    {
        //ゲームが終わっていたら何もしない
        if (IsEnd) return;

        //UI更新
        var count = items.Count(i => i.IsGetted);
        scoreLabel.text = $"{count} / {items.Length}";

        //ゲームクリア処理
        if (!IsWin && count == items.Length)
        {
            IsWin = true;
            //UIの表示
            winnerUI.SetActive(true);

            //BGM変更
            stageBGM.Stop();
            stageClearBGM.Play();
        }
    }

    /// <summary>
    /// リスタートやステージ移動に関する更新
    /// </summary>
    private void WaitContinue()
    {
        //ゲームが終わっていなかったら何もしない
        if (!IsEnd) return;

        //決定操作があった時
        if (Input.GetButton("Submit"))
        {
            //ゲームオーバーまたは次のステージが設定されていなければ、リスタート
            if (IsGameOver || string.IsNullOrWhiteSpace(nextStage))
            {
                var index = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(index);
            }
            else //そうでない場合、次のステージへ
            {
                SceneManager.LoadScene(nextStage);
            }
        }
    }

    /// <summary>
    /// 外部からゲームオーバーを知らせる為の関数
    /// </summary>
    public void GameOver()
    {
        //ゲームが終了していたら何もしない
        if (IsEnd) return;

        IsGameOver = true;

        //BGM切り替え
        stageBGM.Stop();
        gameoverBGM.Play();

        //UIの表示
        gameoverUI.SetActive(true);
    }
}
