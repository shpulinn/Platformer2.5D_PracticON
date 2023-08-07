using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] private SoundSettings soundsSettings;
    
    //private PlayerInputActions playerInputActions;
    private GameInput _gameInput;

    private bool _isPaused = false;

    private void Awake()
    {
        Instance = this;
        //playerInputActions.Player.Pause.performed += PauseOnperformed;
    }

    // private void PauseOnperformed(InputAction.CallbackContext obj)
    // {
    //     if (_isPaused)
    //     {
    //         Unpause();
    //     }
    //     else
    //     {
    //         Pause();
    //     }
    // }

    private void Start()
    {
        _gameInput = FindObjectOfType<GameInput>();
        
        _gameInput.OnPauseAction += GameInputOnOnPauseAction;
        
        gameObject.SetActive(false);
    }

    private void GameInputOnOnPauseAction(object sender, EventArgs e)
    {
        if (soundsSettings.settingsScreenShown)
        {
            soundsSettings.gameObject.SetActive(false);
            return;
        }
        if (_isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        gameObject.SetActive(true);
        _isPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameObject.SetActive(false);
        _isPaused = false;
    }
    
    public void LoadMenu()
    {
        Loader.Load(Loader.Scenes.MainMenuScene);
    }
}
