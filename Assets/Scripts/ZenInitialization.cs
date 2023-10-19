using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class ZenInitialization : MonoInstaller
{
    [SerializeField] private Canvas_Manager canvas_manager;
    [SerializeField] private WIN_LOSS_SYSTEM win_lose_system;
    [SerializeField] private Other_Settings other_settings;
    [SerializeField] private Pool_Bullets pool_bullets;
    [SerializeField] private Shooting_System shooting_system;
    [Space]
    [SerializeField] Transform start_point;
    [SerializeField] GameObject player_prefab;

    public override void InstallBindings()
    {
        Container.Bind<Canvas_Manager>().FromInstance(canvas_manager);
        Container.Bind<WIN_LOSS_SYSTEM>().FromInstance(win_lose_system);
        Container.Bind<Other_Settings>().FromInstance(other_settings);
        Container.Bind<Pool_Bullets>().FromInstance(pool_bullets);
        Container.Bind<Shooting_System>().FromInstance(shooting_system);

        Player_Controller player_controller = Container
            .InstantiatePrefabForComponent<Player_Controller>(player_prefab, start_point.position, Quaternion.identity, null);

        Container.Bind<Player_Controller>()
            .FromInstance(player_controller);
    }
}