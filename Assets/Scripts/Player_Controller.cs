using UnityEngine;
using Zenject;

public class Player_Controller : MonoBehaviour
{
    public float velocity;

    [SerializeField] Transform player_gfx;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * velocity);
    }

    public class Factory: PlaceholderFactory<Player_Controller>{ }
}