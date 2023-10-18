using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System;
using Zenject;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem _vfx_death;
    [SerializeField] MeshRenderer mesh_renderer;
    [SerializeField] Color color_to;

    bool is_dead = false;
    float transition_duration = 0.5f;
    Collider enemy_collider;

    [Inject] WIN_LOSS_SYSTEM win_lose_system;

    private void Awake()
    {        
        enemy_collider = GetComponent<Collider>();
    }

    public void Enemy_Death()
    {
        if (!is_dead)
            StartCoroutine(Delay_And_Death());
    }

    IEnumerator Delay_And_Death()
    {
        is_dead = true;
        enemy_collider.enabled = false;
        mesh_renderer.material.DOColor(color_to, transition_duration);

        _vfx_death.Play();

        yield return new WaitForSeconds(transition_duration);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_GFX"))
            win_lose_system.Game_Defeat();
    }
}