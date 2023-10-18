using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform camera_main;
    [SerializeField] Transform camera_holder;

    [Space]
    [Header("OFFSETS")]
    [SerializeField] float offset_x;
    [SerializeField] float offset_y;
    [SerializeField] float offset_z;

    [Space]
    [Header("CAMERA")]
    [SerializeField] float shake_power;
    [SerializeField] float shake_duration;


    private void FixedUpdate()
    {
        Camera_Follower();
    }

    void Camera_Follower()
    {
        camera_holder.transform.position = new Vector3(
            player.transform.position.x + offset_x,
            player.transform.position.y + offset_y,
            player.transform.position.z + offset_z);
    }

    [Button]
    public void Shake_Camera()
    {
        camera_main.DOShakePosition(shake_duration, shake_power);
    }
}