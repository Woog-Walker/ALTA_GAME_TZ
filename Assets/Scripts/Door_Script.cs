using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Zenject;

public class Door_Script : MonoBehaviour
{
    [Header("DISTANCE")]
    [SerializeField] float current_distance;
    [SerializeField] float distance_to_open;

    [Space]
    [Header("TIME")]
    [SerializeField] float time_to_open;
    [SerializeField] float open_offset;
    [SerializeField] float time_to_check;

    [Space]
    [Header("TRANSFORMS")]
    [SerializeField] Transform player;
    [SerializeField] Transform door;

    Sequence sequence;

    [Inject]
    private void Construct(Player_Controller player_controller)
    {
        player = player_controller.transform;
    }

    private void Awake()
    {
        DOTween.KillAll();

        sequence = DOTween.Sequence();
    }

    private void Start()
    {
        StartCoroutine(Distance_Checker_Delay());
    }

    IEnumerator Distance_Checker_Delay()
    {
        current_distance = Vector3.Distance(transform.position, player.position);

        if (current_distance < distance_to_open)
        {
            Open_Door();
        }
        else
        {
            yield return new WaitForSeconds(time_to_check);
            StartCoroutine(Distance_Checker_Delay());
        }
    }

    [Button]
    void Open_Door()
    {
        sequence.Append(door.DOLocalMove(new Vector3(
            door.transform.localPosition.x,
            door.transform.localPosition.y - open_offset,
            door.transform.localPosition.z),
            time_to_open))
            .SetEase(Ease.InOutQuad);
    }
}