
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject cubePrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(cubePrefab, transform.position, Quaternion.identity);
        }
    }
}