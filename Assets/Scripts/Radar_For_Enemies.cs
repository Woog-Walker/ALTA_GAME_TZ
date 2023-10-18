using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_For_Enemies : MonoBehaviour
{
    [SerializeField] SphereCollider collider_explo;
    [SerializeField] List<GameObject> enemies_list = new List<GameObject>();

    float start_radius;

    private void Awake()
    {
        start_radius = collider_explo.radius;
    }

    public void Recount_Radius(float _tmp_value)
    {
        if (_tmp_value < 0.5f)        
            collider_explo.radius += _tmp_value;        

        if (_tmp_value >= 0.5f && _tmp_value < 0.75f)        
            collider_explo.radius = (collider_explo.radius + _tmp_value) * 1.1f;

        if (_tmp_value >= 0.75f && _tmp_value < 0.9f)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * 1.2f;

        if (_tmp_value >= 0.9f)
            collider_explo.radius = (collider_explo.radius + _tmp_value) * 1.4f;
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