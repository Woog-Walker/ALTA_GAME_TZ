using UnityEngine;
using Zenject;

public class Player_Controller : MonoBehaviour
{
    public float velocity;

    public Transform player_gfx;
    public Transform player_obj;
    public Transform camera_holder;
    public Transform camera_main;
    public Transform ejection_point;
    [Space]
    public ParticleSystem vfx_defeat;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * velocity);
    }

    public class Factory : PlaceholderFactory<Player_Controller> { }
}