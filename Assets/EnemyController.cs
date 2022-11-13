using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameController gameController;
    private PlayerController player;
    private Rigidbody playerRB;
    private new Rigidbody rigidbody;
    private float awakeTime;
    private Vector3 startPos;

    //先回り量の係数
    [Min(0f)]
    public float overRun = 1;

    //移動速度
    [Min(0f)]
    public float speed = 0.05f;

    //追尾状態
    public bool IsAttack { get; set; }

    private void Awake()
    {
        //シーンからプレイヤーとGameControllerを取得
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();

        //プレイヤーのRigidbodyを取得
        playerRB = player.GetComponent<Rigidbody>();
        //自身のRigidbodyを取得
        rigidbody = GetComponent<Rigidbody>();

        //起動後一定時間停止する為に、起動時刻を取っておく。
        awakeTime = Time.time;

        //追尾OFF時に戻る場所として、初期位置を取っておく。
        startPos = transform.position;

        //初期は追尾ON
        IsAttack = true;
    }

    private void FixedUpdate()
    {
        //起動後一秒待って、位置を更新する。プレイヤーが勝利していたら、更新をやめる。
        if (!gameController.IsWin && Time.time > awakeTime + 1f)
        {
            //追尾時
            if (IsAttack)
            {
                //先回りできる位置を計算して移動量を決定
                var def = player.transform.position - transform.position;
                var dist = def.magnitude;
                var v = def + overRun * dist * playerRB.velocity;

                v = v.normalized * speed;
                rigidbody.position += v;
            }
            else//追尾OFF時
            {
                //スタートpositionから、移動量を決定
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
        //プレイヤー勝利時、悔しそうな動きをさせる
        if (gameController.IsWin)
        {
            transform.LookAt(player.transform);
            transform.Rotate(0, Mathf.Sin(Time.time * 80f) * 4f + Mathf.Sin(Time.time * 7f) * 20f, 0);
        }
        else
        {
            //進行方向を向く。
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
