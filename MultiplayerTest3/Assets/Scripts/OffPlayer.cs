using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffPlayer : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    [SerializeField]
    private AudioClip[] walkingSounds;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    [SerializeField]
    private AudioSource WalkingSoundSnow;
    bool isWalking = true;
    void HandleMovement()
    {
        
        
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                //physics formula of jump
                velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if ((x > 0.01 || z > 0.01 || x < 0 || z < 0) && isGrounded == true)
            {
                if(isWalking == true)
                {
                    isWalking = false;

                   
                   
                WalkingSoundSnow.Play();
                }
            
            }
            else
            {
            WalkingSoundSnow.Stop();
            isWalking = true;
            }

    }
    void UpdateWalkingSounds()
    {
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, 2f, groundMask);

        if (hitColliders[0].tag == "Wood")
        {
            if(WalkingSoundSnow.clip == walkingSounds[0])
            {
                WalkingSoundSnow.Stop();
                isWalking = true;
                WalkingSoundSnow.volume = 1f;
                WalkingSoundSnow.clip = walkingSounds[1];

            }
           
        }
        else if (hitColliders[0].tag == "terrain")
        {
            if (WalkingSoundSnow.clip == walkingSounds[1])
            {
                WalkingSoundSnow.Stop();
                WalkingSoundSnow.volume = 0.3f;
                isWalking = true;
                WalkingSoundSnow.clip = walkingSounds[0];

            }
        }
    }
    
    void Update()
    {

 
        UpdateWalkingSounds();
        HandleMovement();
    }
}
    

