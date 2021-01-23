namespace Assets.Scripts
{
    using System;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float playerMaxSpeed;

        /// <summary>
        /// 
        /// </summary>
        private Animator playerAnimator;

        /// <summary>
        /// 
        /// </summary>
        private Rigidbody2D playeRigidbody;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Transform groundChecker;

        /// <summary>
        /// 
        /// </summary>
        private bool facingRight;


        /// <summary>
        /// 
        /// </summary>
        private bool isJumping;

        /// <summary>
        /// 
        /// </summary>
        private float horizontalMove;


        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            this.isJumping = false;
            this.playeRigidbody = this.GetComponent<Rigidbody2D>();
            this.playerAnimator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            this.horizontalMove = Input.GetAxis("Horizontal");
        }

        /// <summary>
        /// 
        /// </summary>
        private void FixedUpdate()
        {
            
            if (Physics2D.Linecast(this.transform.position, this.groundChecker.position,
                1 << LayerMask.NameToLayer("Ground")))
            {
                isJumping = false;
                Debug.Log("isGrounded");
            }


            this.playeRigidbody.velocity = new Vector2(this.horizontalMove * this.playerMaxSpeed, this.playeRigidbody.velocity.y);

            if (Input.GetKey("space"))
            {
                isJumping = true;
                this.playeRigidbody.velocity = new Vector2(this.playeRigidbody.velocity.x, 2);
            }


            if (this.playeRigidbody.velocity.x > 0 && !this.facingRight)
            {
                this.Flip();
            }
            else if (this.playeRigidbody.velocity.x < 0 && this.facingRight)
            {
                this.Flip();
            }

            this.CheckAnimation(Math.Abs(horizontalMove), isJumping);
        }

        private void Flip()
        {
            this.facingRight = !this.facingRight;
            var scale = this.transform.localScale;
            scale.x *= -1;
            this.transform.localScale = scale;
        }

        private void CheckAnimation(float speed, bool isJumping)
        {
            this.playerAnimator.SetFloat("Speed", speed);
            this.playerAnimator.SetBool("IsJump", isJumping);
        }

    }
}
