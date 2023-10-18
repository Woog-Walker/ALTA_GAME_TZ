using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool_Bullets : MonoBehaviour
{
    [SerializeField] List<GameObject> bullets_pool = new List<GameObject>();
    [SerializeField] GameObject prefab_bullet;

    public GameObject Get_Bullet_From_Pull()
    {
        foreach (var obj in bullets_pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab_bullet);
        bullets_pool.Add(newObj);
        return newObj;
    }

    // Return an object to the pool
    public void Return_Object_To_Pool(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.GetComponent<Bullet_Ball>().Clear_Before_Pool();
        obj.SetActive(false);
    }
}