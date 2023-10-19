using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class WIN_LOSS_SYSTEM : MonoBehaviour
{
    [SerializeField] Player_Controller player_controller;
    [SerializeField] Touch_Controller touch_controller;
    [Space]
    [SerializeField] Transform player_gfx;
    [SerializeField] ParticleSystem vfx_defeat;

    float delay = 0.5f;

    [Inject] private Canvas_Manager canvas_manager;
    [Inject]
    private void Construct(Player_Controller _player_controller)
    {
        player_controller = _player_controller;
        player_gfx = _player_controller.player_gfx;
        vfx_defeat = _player_controller.vfx_defeat;
    }

    #region PART WIN     
    public void Game_Win()
    {
        StartCoroutine(Win_Delay());
    }

    IEnumerator Win_Delay()
    {
        touch_controller.Disable_Interactions();

        yield return new WaitForSeconds(delay);

        player_controller.velocity = 0;
        canvas_manager.Show_Panel_Win();
    }

    #endregion

    #region PART DEFEAT     
    public void Game_Defeat()
    {
        StartCoroutine(Loss_Delay());
    }

    IEnumerator Loss_Delay()
    {
        player_gfx.DOScale(Vector3.zero, delay);
        vfx_defeat.Play();

        touch_controller.Disable_Interactions();
        player_controller.velocity = 0;

        yield return new WaitForSeconds(delay);

        canvas_manager.Show_Panel_Loss();
    }
    #endregion
}