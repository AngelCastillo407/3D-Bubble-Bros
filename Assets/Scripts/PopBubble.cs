using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBubble : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
