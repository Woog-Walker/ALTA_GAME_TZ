using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Zenject;

public class Canvas_Manager : MonoBehaviour
{
    [Header("BARS")]
    [SerializeField] Image bar_charge_fill;
    [SerializeField] Image bar_health_fill;
    [Space]
    [SerializeField] GameObject bar_charge;
    [SerializeField] GameObject bar_health;

    [Space]
    [Header("GAME FINISH STUFF")]
    [SerializeField] GameObject pannel_over;
    [SerializeField] TMP_Text text_status;

    float transition_duration = 0.5f;
    const string text_win = "YOU WIN";
    const string text_loss = "GAME OVER";

    [Inject] private Other_Settings other_settings;

    #region UI BARS
    public void Repaint_Charge_Bar(float _tmp_amount)
    {
        bar_charge_fill.fillAmount = _tmp_amount;
    }

    public void Repaint_Health_Bar(float _tmp_amount)
    {
        bar_health_fill.fillAmount = _tmp_amount;
    }
    #endregion

    #region GAME OVER PANELS
    [Button]
    public void Show_Panel_Win()
    {
        pannel_over.transform.DOLocalMove(new Vector3(0, 0, 0), transition_duration).SetEase(Ease.OutBack);
        pannel_over.transform.DOScale(new Vector3(1, 1, 1), transition_duration).SetEase(Ease.OutBack);

        text_status.text = text_win;
        pannel_over.SetActive(true);

        Hide_Bars();
    }         

    public void Show_Panel_Loss()
    {
        pannel_over.transform.DOLocalMove(new Vector3(0, 0, 0), transition_duration).SetEase(Ease.OutBack);
        pannel_over.transform.DOScale(new Vector3(1, 1, 1), transition_duration).SetEase(Ease.OutBack);

        text_status.text = text_loss;
        pannel_over.SetActive(true);

        Hide_Bars();
    }

    private void Hide_Bars()
    {
        bar_charge.transform.DOMove(new Vector3(bar_charge.transform.position.x, -400, 0), transition_duration).SetEase(Ease.InOutQuad);
        bar_health.transform.DOMove(new Vector3(bar_health.transform.position.x, -400, 0), transition_duration).SetEase(Ease.InOutQuad);
    }
    #endregion

    #region BUTTONS
    public void Button_Replay()
    {
        other_settings.Replay_Scene();
    }

    public void Button_Exit()
    {
        other_settings.Exit_App();
    }
    #endregion
}