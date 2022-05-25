using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float originalX;
    private float maxLeft = -5f;
    private float maxRight = 5f;
    private float maxOffset = 10f;
    private float enemyPatrolTime = 2f;
    private int moveRight = -1;
    private float speedUpScore = 0f;
    private float speedMultiplier = 1f;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public GameObject mario;
    private PlayerController marioControl;
    private Rigidbody2D marioBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // Call PlayerController for score counter
        // TODO: Change in future.
        marioControl = mario.GetComponent<PlayerController>();
        marioBody = mario.GetComponent<Rigidbody2D>();
        // Get Starting Pos
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        maxOffset = maxRight - maxLeft;
        velocity = new Vector2((moveRight)*maxOffset / enemyPatrolTime, 0 );
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime * speedMultiplier);
    }

    void FlipGoomba()
    {
        moveRight *= -1;
        ComputeVelocity();
        MoveGoomba();
    }

    // Update is called once per frame
    void Update()
    {
        /* // Original Code for reference (in quiz)
        // Not at end yet
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset){
            MoveGoomba();
        } 
        // at the end and flip
        else{
            moveRight *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
        */
        // Update with Mario's Position. Expanding Patrol Path

        if (enemyBody.position.x < maxLeft || enemyBody.position.x > maxRight)
        {
            FlipGoomba();
        }
        else {
            MoveGoomba();
        }

        // Chase Mario if in patrol range and on the ground

        if(marioControl.onGroundState && marioBody.position.x > maxLeft && marioBody.position.x < maxRight && 
        (enemyBody.position.x < marioBody.position.x && moveRight == -1 || 
        enemyBody.position.x > marioBody.position.x && moveRight == 1))
        {
            Debug.Log("1 Mario Detected");
            FlipGoomba();
        }

        // Speed Up Goomba every 5 points
        if (Mathf.Floor(marioControl.score/5) != speedUpScore)
        {
            speedUpScore += 1f;
            if (speedMultiplier < 10f)
            {
                Debug.Log("Goomba Speedup");
                speedMultiplier = 1 + speedUpScore/5;
            }
        }

    }
}
