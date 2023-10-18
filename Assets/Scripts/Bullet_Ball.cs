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

    float start_size;
    private bool is_locked = true;

    Radar_For_Enemies enemies_radar;

    [Header("WHAT DO DISABLE")]
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] MeshRenderer meshRenderer;

    [Inject] private Other_Settings other_settings;
    [Inject] private Pool_Bullets pool_bullets;

    private void Awake()
    {
        start_size = transform.localScale.x;
        enemies_radar = GetComponentInChildren<Radar_For_Enemies>();

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
            start_size + charge_force);
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
        sphereCollider.enabled = true;
        speed = start_speed;
        is_locked = false;

        enemies_radar.Recount_Radius(charge_force);
    }

    IEnumerator Explo_Delay()
    {
        speed = 0;

        float vfx_radius = Count_VFX_Size();
        vfx_explo.gameObject.transform.localScale = new Vector3(vfx_radius, vfx_radius, vfx_radius);

        vfx_explo.Play();

        enemies_radar.Perform_Explosion();

        sphereCollider.enabled = false;
        meshRenderer.enabled = false;

        enemies_radar.Perform_Explosion();
        other_settings.Explo_Vibro();

        yield return new WaitForSeconds(0.75f);

        Clear_Before_Pool();
        pool_bullets.Return_Object_To_Pool(gameObject);


        gameObject.SetActive(false);
    }

    private float Count_VFX_Size()
    {
        // count vfx size by shot power
        vfx_explo.gameObject.transform.localScale = new Vector3(2, 2, 2);



        return vfx_explo.gameObject.transform.localScale.x + charge_force;
    }

    public void Clear_Before_Pool()
    {
        is_locked = true;
        meshRenderer.enabled = true;
        sphereCollider.enabled = true;

        transform.localScale = new Vector3(start_size, start_size, start_size);

        enemies_radar.Clear_Before_Pull();
    }

    private void OnEnable()
    {
        sphereCollider.enabled = false;
    }
}