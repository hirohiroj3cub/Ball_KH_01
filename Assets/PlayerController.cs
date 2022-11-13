using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public new Transform camera;
    private new Rigidbody rigidbody;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //ゲームが終了してなければ、操作可能、そうでなければ空気抵抗を大きくして動きを止める
        if (gameController == null || !gameController.IsEnd)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            //カメラの向きから移動の向きを計算
            var xv = camera.right;
            var yv = camera.forward;
            yv.y = xv.y = 0;
            xv.Normalize();
            yv.Normalize();

            rigidbody.AddForce(speed * (xv * x + yv * y));
        }
        else
        {
            rigidbody.drag = 10;
        }
    }
}
