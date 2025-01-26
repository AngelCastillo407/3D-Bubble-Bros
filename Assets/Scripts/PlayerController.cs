using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        public bool isGrounded;
        public Transform groundCheck;

        private Rigidbody rigidbody;
        private Animator animator;
        private GameManager gameManager;
        private bool isWalking= false;
        private int counter = 1;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            gameManager = GameObject.Find("GameManagerEmpty").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            if (counter == 50)
            {
                counter = 0;
                animator.ResetTrigger("pressedJump");
                //animator.ResetTrigger("pressedAttack");

            }
            if (counter == 55)
            {
                animator.ResetTrigger("pressedAttack");
            }
            counter += 1;
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) 
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.forward * moveInput;
                if (transform.rotation.y > 0 && moveInput < 0)
                {
                    transform.Rotate(0.0f, -180f, 0.0f, Space.Self);
                    direction = transform.forward * moveInput;
                }
                if (transform.rotation.y < 0 && moveInput > 0)
                {
                    transform.Rotate(0.0f, 180f, 0.0f, Space.Self);
                    direction = transform.forward * moveInput;
                }

                if (moveInput < 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position - direction, movingSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                }
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                animator.ResetTrigger("touchingGround");
                animator.SetTrigger("pressedJump");
                counter = 0;
            }

            if(facingRight == false && moveInput > 0)
            {
                Flip();
                gameManager.facingRight = true;
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
                gameManager.facingRight = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                counter = 51;
                animator.SetTrigger("pressedAttack");
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true; // Say to GameManager that player is dead
                Debug.Log("collided with enemy!");
            }
            else if (other.gameObject.tag == "Bubble")
            {
                rigidbody.velocity *= 0;
                rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
            if (other.gameObject.tag == "floor")
            {
                isGrounded = true;
                animator.SetTrigger("touchingGround");
                counter = 1;
            }
            if(other.gameObject.tag == "deathFloor")
            {
                deathState = true;
            }
        }

    }
}
