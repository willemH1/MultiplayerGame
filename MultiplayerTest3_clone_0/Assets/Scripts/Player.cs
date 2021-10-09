using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
    }
    void Update()
    {
        HandleMovement();
    }
}
