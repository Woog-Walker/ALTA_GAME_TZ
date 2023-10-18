using System.Collections; // odin questionz array sort by
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Other_Settings : MonoBehaviour
{
    [SerializeField] int target_fps;

    private void Start()
    {
        Set_App_FPS();
    }

    private void Set_App_FPS()
    {
        Application.targetFrameRate = target_fps;
    }

    public void Exit_App()
    {
        Application.Quit();
    }

    public void Replay_Scene()
    {
        SceneManager.LoadScene(0);
    }

    public void Explo_Vibro()
    {
        if (SystemInfo.supportsVibration)
            Handheld.Vibrate();
    }
}