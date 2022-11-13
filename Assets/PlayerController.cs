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
        //�Q�[�����I�����ĂȂ���΁A����\�A�����łȂ���΋�C��R��傫�����ē������~�߂�
        if (gameController == null || !gameController.IsEnd)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            //�J�����̌�������ړ��̌������v�Z
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
