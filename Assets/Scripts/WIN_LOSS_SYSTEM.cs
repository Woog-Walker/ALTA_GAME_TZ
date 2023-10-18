using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class WIN_LOSS_SYSTEM : MonoBehaviour
{
    [SerializeField] Player_Controller player_controller;
    [SerializeField] Touch_Controller touch_controller;
    [Space]
    [SerializeField] Transform player_gfx;
    [SerializeField] ParticleSystem vfx_defeat;

    [Inject] private Canvas_Manager canvas_manager;

    public void Game_Win()
    {
        StartCoroutine(Win_Delay());
    }

    IEnumerator Win_Delay()
    {
        touch_controller.Disable_Interactions();

        yield return new WaitForSeconds(0.5f);

        player_controller.velocity = 0;
        canvas_manager.Show_Panel_Win();
    }

    public void Game_Loss()
    {
        StartCoroutine(Loss_Delay());
    }

    IEnumerator Loss_Delay()
    {
        player_gfx.DOScale(Vector3.zero, 0.5f);
        vfx_defeat.Play();

        touch_controller.Disable_Interactions();
        player_controller.velocity = 0;

        yield return new WaitForSeconds(0.5f);

        canvas_manager.Show_Panel_Loss();
    }
}