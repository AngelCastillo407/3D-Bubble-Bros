
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerScript : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector3 vector = new Vector3(1, 1, 0);
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(cubePrefab, Vector3.Scale(transform.position, vector), Quaternion.identity);
        }
    }
}