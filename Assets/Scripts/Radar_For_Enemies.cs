using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_For_Enemies : MonoBehaviour
{
    [SerializeField] SphereCollider collider_explo;
    [SerializeField] List<GameObject> enemies_list = new List<GameObject>();

    [Space]
    [Header("RADIUS AND MULTIPLICATOR")]
    [SerializeField] float compare_power_small;
    [SerializeField] float compare_power_mid;
    [SerializeField] float compare_power_big;
    [Space]
    [SerializeField] float multiplicator_small;
    [SerializeField] float multiplicator_mid;
    [SerializeField] float multiplicator_big;

    float start_radius;

    private void Awake()
    {
        start_radius = collider_explo.radius;
    }

    public void Recount_Radius(float _tmp_value)
    {
        // explo radius

        if (_tmp_value < compare_power_small)        
            collider_explo.radius += _tmp_value;        

        if (_tmp_value >= compare_power_small && _tmp_value < compare_power_mid)        
            collider_explo.radius = (collider_explo.radius + _tmp_value) * multiplicator_small;

        if (_tmp_value >= compare_power_mid && _tmp_value < compare_power_big)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * multiplicator_mid;

        if (_tmp_value >= compare_power_big)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * multiplicator_big;
    }

    public void Perform_Explosion()
    {
        foreach (var _tmp_enemy in enemies_list)
            _tmp_enemy.GetComponent<Enemy_Controller>().Enemy_Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies_list.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies_list.Remove(other.gameObject);
    }

    public void Clear_Before_Pool()
    {
        collider_explo.radius = start_radius;
        enemies_list = new List<GameObject>();
    }
}