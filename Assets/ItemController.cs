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
        //自身のMeshRenderer（描画機能）を取得
        meshRenderer = GetComponent<MeshRenderer>();
        IsGetted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //既に取得されていたらなにもしない
        if (IsGetted) return;

        //ゲームが終了していない、かつプレイヤーと衝突したら取得判定
        if (!gameController.IsEnd && other.CompareTag("Player"))
        {
            IsGetted = true;

            //メッシュを非表示に
            meshRenderer.enabled = false;

            //SEを再生
            se1.Play();
            se2.Play();
        }
    }
}
