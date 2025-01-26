using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f; 
        public LayerMask ground;
        public LayerMask wall;

        private Rigidbody rigidbody; 
        public Collider triggerCollider;
        private SpriteRenderer sprite;
        private MeshRenderer myMesh;

        public int health;

        private bool justDamaged = false;
        private float timeElapsed = 0;
        private Color originalColor;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            
            if (gameObject.name == "Ghost")
            {
                sprite = GetComponent<SpriteRenderer>();
            }
            else
            {
                myMesh = GetComponent<MeshRenderer>();
                originalColor = myMesh.material.color;
            }
            
        }

        void Update()
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        }

        void FixedUpdate()
        {
            if (justDamaged)
            {
                timeElapsed += Time.deltaTime;

            }

            if ( sprite && timeElapsed >= 0.125f)
            {
                justDamaged = false;
                sprite.color = new Color(255, 255, 255);
            }
            else if (timeElapsed >= 0.125f)
            {
                justDamaged = false;
                myMesh.material.color = originalColor;
            }

        }
        
        private void Flip()
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            moveSpeed *= -1;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Bubble Projectile")
            {
                health -= 1;
                if (sprite)
                {
                    sprite.color = new Color(255, 0, 0);
                }
                else
                {
                    myMesh.material.color = new Color(0f, 0f, 0f, 0f);
                }
                justDamaged = true;
                timeElapsed = 0;

                if (health == 0)
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject, 0);
                }
            }

            else if (other.gameObject.tag == "wall")
            {
                Flip();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == "floor")
            {
                Flip();
            } 
        }


    }
}
