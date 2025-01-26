using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerAnimatorSpeaker : MonoBehaviour
    {

        public Animator animator;

        // Update is called once per frame
        void Update()
        {

            if (Input.GetButtonDown("Horizontal"))
            {
                animator.SetTrigger("walking");
            }

            if (Input.GetButtonUp("Horizontal") && !Input.GetButton("Horizontal"))
            {
                animator.ResetTrigger("walking");
            }

        }
    }

}
