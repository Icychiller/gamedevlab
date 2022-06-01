using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float originalX;
    private float maxLeft = -5f;
    private float maxRight = 5f;
    private float speed = 4f;
    private float enemyPatrolTime = 2f;
    private int moveRight = -1;
    private float speedMultiplier = 1f;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    private BoxCollider2D enemyCollider;

    public GameObject mario;
    private PlayerController marioControl;
    private Rigidbody2D marioBody;
    public BoxCollider2D patrolRegion;
    public bool detectPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();

        // Call PlayerController for score counter
        // TODO: Change in future.
        marioControl = mario.GetComponent<PlayerController>();
        marioBody = mario.GetComponent<Rigidbody2D>();
        // Get Starting Pos
        originalX = transform.position.x;
        // Get Patrol Region Limits
        maxLeft = patrolRegion.bounds.min.x + enemyCollider.size.x/2;
        maxRight = patrolRegion.bounds.max.x - enemyCollider.size.x/2;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight)*speed, 0 );
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

        if ((enemyBody.position.x < maxLeft && moveRight<0) || (enemyBody.position.x > maxRight && moveRight>0))
        {
            FlipGoomba();
        }
        else {
            MoveGoomba();
        }

        // Chase Mario if in patrol range and on the ground
        // Edit: Chases Mario if in patrol region and on the ground
        // Edit: Goomba Speeds up when mario is in range rather than with score.

        /* if(marioControl.onGroundState && marioBody.position.x > maxLeft && marioBody.position.x < maxRight && 
        (enemyBody.position.x < marioBody.position.x && moveRight == -1 || 
        enemyBody.position.x > marioBody.position.x && moveRight == 1)) */
        if(detectPlayer)
        {
            if(((marioBody.position.x - enemyBody.position.x)*moveRight) < 0)
            {
                Debug.Log("Detect Mario and Wrong Way");
                FlipGoomba();
            }
            speedMultiplier = 1.5f;
        }
        else
        {
            speedMultiplier = 1f;
        }

    }
}
