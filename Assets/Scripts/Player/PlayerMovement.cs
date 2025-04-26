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
    private bool isFacingRight = true;

    public Animator animator;

    [SerializeField] private float speed = 100f; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;


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
            animator.SetFloat("speed", Mathf.Abs(horizontal));
            Flip();

        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed *Time.deltaTime, rb.velocity.y);
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
