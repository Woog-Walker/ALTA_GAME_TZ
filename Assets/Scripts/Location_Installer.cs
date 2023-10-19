using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class Location_Installer : MonoInstaller
{
    public Transform start_point;
    public GameObject player_prefab;

    public override void InstallBindings()
    {
        Player_Controller player_controller = Container
            .InstantiatePrefabForComponent<Player_Controller>(player_prefab, start_point.position, Quaternion.identity, null);

        Container.Bind<Player_Controller>()
            .FromInstance(player_controller)
            .AsSingle();
    }
}