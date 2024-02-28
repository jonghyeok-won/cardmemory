using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if(sceneName == "StartScene")
        {
            ScoreManager.Instance.ResetScore();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
