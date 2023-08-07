using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public void PlayButtonClick()
    {
        Loader.Load(Loader.Scenes.MainScene);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
