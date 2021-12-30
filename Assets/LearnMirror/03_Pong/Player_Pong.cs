using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player_Pong : NetworkBehaviour
{
    private float speed = 1500;
    public Rigidbody2D rigidbody2d;

    private Rigidbody2D rigidbody2d_follow;

    private void Start()
    {
        Debug.Log("Start:" + name);

    }
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            // rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime);
            rigidbody2d.velocity = new Vector2(0, Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);
        }
    }
}
