using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameController gameController;
    public Transform target;

    /// <summary>
    /// 移動量の係数。0に近いとカメラはゆっくり近づく。
    /// </summary>
    [Range(0f, 1f)]
    public float par = 0.5f;
    /// <summary>
    /// 目標となる距離
    /// </summary>
    private float offsetDist;
    /// <summary>
    /// 目標となる高さ
    /// </summary>
    private float offsetHeight;

    private void Awake()
    {
        //ゲームオーバー検知の為に、GameControllerをシーンから取得
        gameController = FindObjectOfType<GameController>();

        //初期状態のカメラとプレイヤーの位置関係から、目標となる高さと距離を取得。
        var offset = transform.position - target.position;
        offsetDist = offset.magnitude;
        offsetHeight = offset.y;
    }

    private void FixedUpdate()
    {
        //目標の高さと距離から、カメラの移動量を計算
        var def = transform.position - target.position;
        def.y = 0;
        def = def.normalized * Mathf.Sqrt(offsetDist * offsetDist - offsetHeight * offsetHeight);
        var targetPos = target.position + Vector3.up * offsetHeight + def;
        transform.position += (targetPos - transform.position) * par;
    }

    private void Update()
    {
        //カメラの向きをターゲットに向ける。
        transform.LookAt(target);

        //ゲームオーバー時、カメラを自動回転させる。
        if (gameController.IsEnd)
        {
            transform.position += transform.right * 0.05f;
        }
    }
}
