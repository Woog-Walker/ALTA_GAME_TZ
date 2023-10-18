using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_For_Enemies : MonoBehaviour
{
    [SerializeField] SphereCollider collider_explo;
    [SerializeField] List<GameObject> enemies_list = new List<GameObject>();

    float start_radius;

    float compare_step_1 = 0.5f;
    float compare_step_2 = 0.75f;
    float compare_step_3 = 0.9f;

    float radius_explo_1 = 1.1f;
    float radius_explo_2 = 1.2f;
    float radius_explo_3 = 1.4f;

    private void Awake()
    {
        start_radius = collider_explo.radius;
    }

    public void Recount_Radius(float _tmp_value)
    {
        if (_tmp_value < compare_step_1)        
            collider_explo.radius += _tmp_value;        

        if (_tmp_value >= compare_step_1 && _tmp_value < compare_step_2)        
            collider_explo.radius = (collider_explo.radius + _tmp_value) * radius_explo_1;

        if (_tmp_value >= compare_step_2 && _tmp_value < compare_step_3)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * radius_explo_2;

        if (_tmp_value >= compare_step_3)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * radius_explo_3;
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

    public void Clear_Before_Pull()
    {
        collider_explo.radius = start_radius;
        enemies_list = new List<GameObject>();
    }
}