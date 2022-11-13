using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        //GameController���擾���Ă�����
        gameController = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player�ƏՓ˂�����AGameController�ɃQ�[���I�[�o�[��m�点��B
            gameController.GameOver();
        }
    }
}
