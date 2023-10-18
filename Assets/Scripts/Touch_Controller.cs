using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Controller : MonoBehaviour
{
    [SerializeField] bool is_pressed = false;

    [HideInInspector] public bool is_game_over { get; private set; }

    private void Start()
    {
        is_game_over = false;
    }

    private void Update()
    {
        if (is_game_over) return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Input_Pressed();

        if (Input.GetKeyUp(KeyCode.Space))
            Input_UnPressed();
#endif

        if (is_pressed)
            Shooting_System.instance.Whie_Pressed();

#if PLATFORM_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Input_Pressed();
                    break;

                case TouchPhase.Ended:
                    Input_UnPressed();
                    break;
            }
        }
#endif
    }

    #region INPUTS
    private void Input_Pressed()
    {
        is_pressed = true;
        Shooting_System.instance.Create_Bullet();
    }

    private void Input_UnPressed()
    {
        is_pressed = false;
        Shooting_System.instance.While_UnPressed();
    }
    #endregion

    public void Disable_Interactions()
    {
        is_game_over = true;
    }
}