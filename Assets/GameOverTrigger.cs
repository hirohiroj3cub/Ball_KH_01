using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        //GameControllerを取得しておいて
        gameController = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Playerと衝突したら、GameControllerにゲームオーバーを知らせる。
            gameController.GameOver();
        }
    }
}
