using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet_Ball : MonoBehaviour
{
    [SerializeField] float speed;
    float start_speed;

    public float charge_force;
    [SerializeField] ParticleSystem vfx_explo;

    [Header("WHAT DO DISABLE")]
    [SerializeField] SphereCollider sphere_collider;
    [SerializeField] MeshRenderer mesh_renderer;

    float size_multiplicator = 1.5f;
    float explo_delay = 0.75f;
    float start_size;
    float vfx_start_size;
    private bool is_locked = true;

    Radar_For_Enemies enemies_radar;

    [Inject] private Other_Settings other_settings;
    [Inject] private Pool_Bullets pool_bullets;

    private void Awake()
    {
        vfx_start_size = vfx_explo.gameObject.transform.localScale.x;
        enemies_radar = GetComponentInChildren<Radar_For_Enemies>();

        start_size = transform.localScale.x;
        start_speed = speed;
    }

    private void FixedUpdate()
    {
        Set_Size();
        Move_Forward();
    }

    public void Set_Size()
    {
        transform.localScale = new Vector3(
            start_size + charge_force,
            start_size + charge_force,
            start_size + charge_force) 
            * size_multiplicator;
    }

    private void Move_Forward()
    {
        if (is_locked) return;

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            StartCoroutine(Explo_Delay());
    }

    public void Launch_Bullet()
    {
        sphere_collider.enabled = true;
        speed = start_speed;
        is_locked = false;

        enemies_radar.Recount_Radius(charge_force);
    }

    IEnumerator Explo_Delay()
    {
        speed = 0;

        sphere_collider.enabled = false;
        mesh_renderer.enabled = false;

        vfx_explo.Play();
        other_settings.Explo_Vibro();
        enemies_radar.Perform_Explosion();
        enemies_radar.Perform_Explosion();

        float vfx_radius = Count_VFX_Size();
        vfx_explo.gameObject.transform.localScale = new Vector3(vfx_radius, vfx_radius, vfx_radius);

        yield return new WaitForSeconds(explo_delay);

        Clear_Before_Pool();
        pool_bullets.Return_Object_To_Pool(gameObject);

        gameObject.SetActive(false);
    }

    private float Count_VFX_Size()
    {
        vfx_explo.gameObject.transform.localScale = new Vector3(vfx_start_size, vfx_start_size, vfx_start_size);

        float final_size = vfx_explo.gameObject.transform.localScale.x + charge_force;
        return final_size;
    }

    public void Clear_Before_Pool()
    {
        is_locked = true;
        mesh_renderer.enabled = true;
        sphere_collider.enabled = true;

        transform.localScale = new Vector3(start_size, start_size, start_size);

        enemies_radar.Clear_Before_Pull();
    }

    private void OnEnable()
    {
        sphere_collider.enabled = false;
    }
}