using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private CharacterController controller;
    private SpriteRenderer spriteRenderer;

    private float horizontal;
    private float vertical;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;
    private bool isGrounded;
    public Animator animator;

    [SerializeField] private float speed = 100f; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    void Update()
    {
        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
        {
            speed = 0f;
           
            animator.SetFloat("speed", 0f);
            return;

        } else
        {
            speed = 100f;
            horizontal = Input.GetAxisRaw("Horizontal");

            if (isGrounded){
              animator.SetBool("isJumping", false);
            }
            if (!isGrounded){

              animator.SetBool("isJumping", true);
            }
          
            if (Input.GetButtonDown("Jump") && isGrounded){
              jump();
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f){

              rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            animator.SetFloat("speed", Mathf.Abs(horizontal));
            Flip();

        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed *Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.tag == "Ground"){
        isGrounded = true;
      }

    }
    private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Ground")
    {
        isGrounded = false;
    }
}

    private void jump(){
      rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playePosition;
        
    }

    public void SaveData(ref GameData data)
    {
        
        data.playePosition = this.transform.position;
        
    }

}
