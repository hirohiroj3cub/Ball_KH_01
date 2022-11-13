using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    private PlayerController player;
    private Vector3 offset;
    private Vector3 center;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        offset = transform.position - player.transform.position;
        center = player.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //カメラの位置を回しながら、カメラの向きをセンターに向ける
        transform.position = center + offset;
        transform.LookAt(center);
        offset = Quaternion.Euler(0, 0.1f, 0) * offset;
    }
}
