using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        // ----- Load menu scene
        Loader.Load(Loader.Scenes.MainScene);
    }
}
