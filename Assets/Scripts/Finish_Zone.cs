using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Finish_Zone : MonoBehaviour
{
    [Inject] WIN_LOSS_SYSTEM win_lose_system;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_GFX"))
            win_lose_system.Game_Win();

    }
}