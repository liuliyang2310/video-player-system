using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Spawner_Ball : NetworkBehaviour
{
    public  Rigidbody2D rigidbody2D_ball;
    private float speed = 40;
    public override void OnStartServer()
    {
        base.OnStartServer();
        rigidbody2D_ball.simulated = true;
        rigidbody2D_ball.velocity = Vector2.right * speed;
    }


    private float HitFactor(Vector2 ballPos, Vector2 playerPos, float reckedHeight)
    {
        return (ballPos.y - playerPos.y) / reckedHeight;
    }

  //  [ServerCallback]
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.GetComponent<Player_Pong>())
        {
            float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

            Debug.Log(col.collider.bounds.size.y);
            Debug.Log(y);
            float x = col.relativeVelocity.x > 0 ? 1 : -1;
            Vector2 dir = new Vector2(x, y).normalized;
            rigidbody2D_ball.velocity = dir * speed;
        }
    }
}
