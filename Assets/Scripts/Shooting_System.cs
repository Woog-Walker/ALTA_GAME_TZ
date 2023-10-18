using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shooting_System : MonoBehaviour
{
    [SerializeField] Transform player_gfx;
    [SerializeField] Transform road_gfx;
    [SerializeField] Transform eject_point;
    [SerializeField] GameObject current_ball;

    [Space]
    [Header("RESIZE_SHOT_POWER")]
    [SerializeField] float shoot_force;
    [SerializeField] float size_to_defeat;
    [Space]
    [SerializeField] float resize_value_player;
    [SerializeField] float resize_value_road;
    [SerializeField] float shooting_power;

    float start_size_road;
    float start_size_player;
    float size_road;
    float size_player;

    bool is_dead = false;

    public static Shooting_System instance;
    [SerializeField] Camera_Controller camera_controller;

    [Inject] Canvas_Manager canvas_manager;
    [Inject] WIN_LOSS_SYSTEM win_lose_system;
    [Inject] Pool_Bullets pool_bullets;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        Count_Start_Size();
    }

    private void Count_Start_Size()
    {
        start_size_player = player_gfx.localScale.x;
        start_size_road = road_gfx.localScale.x;

        size_road = start_size_road;
        size_player = start_size_player;
    }

    public void Whie_Pressed()
    {
        if (shoot_force > 1) return;

        shoot_force += shooting_power;

        // count objects size
        size_road -= resize_value_road;
        size_player -= resize_value_player;
        
        // player size
        player_gfx.localScale = new Vector3(
            size_player,
            size_player,
            size_player);

        // road size
        road_gfx.localScale = new Vector3(
            size_road,
            road_gfx.localScale.y,
            road_gfx.localScale.z);

        Check_Defeat_Size();

        current_ball.GetComponent<Bullet_Ball>().charge_force = shoot_force;

        canvas_manager.Repaint_Charge_Bar(shoot_force);
        canvas_manager.Repaint_Health_Bar(size_player / start_size_player);
    }

    public void While_UnPressed()
    {
        shoot_force = 0;
        Release_Bullet();
        camera_controller.Shake_Camera();
    }

    public void Create_Bullet()
    {
        var _ejection_ball = pool_bullets.Get_Bullet_From_Pull();
        _ejection_ball.transform.parent = eject_point;

        _ejection_ball.transform.position = new Vector3(
            eject_point.transform.position.x,
            eject_point.transform.position.y,
            eject_point.transform.position.z);

        _ejection_ball.transform.localScale = new Vector3(
            _ejection_ball.transform.localScale.x, _ejection_ball.transform.localScale.y,  _ejection_ball.transform.localScale.z)
            * (1 + shoot_force);

        current_ball = _ejection_ball;
    }

    public void Release_Bullet()
    {
        canvas_manager.Repaint_Charge_Bar(0);

        current_ball.transform.parent = null;
        current_ball.GetComponent<Bullet_Ball>().Launch_Bullet();
    }

    private void Check_Defeat_Size()
    {
        if (size_player < size_to_defeat && !is_dead)
        {

            is_dead = true;
            win_lose_system.Game_Loss();

            if (shoot_force > 0)
                Release_Bullet();
        }
    }
}