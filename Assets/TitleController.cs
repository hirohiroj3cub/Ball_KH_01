using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public string stageName;

    // Update is called once per frame
    private void Update()
    {
        //ゲーム開始！
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(stageName);
        }
    }
}
