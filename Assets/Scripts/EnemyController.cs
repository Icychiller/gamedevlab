using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5f;
    private float enemyPatrolTime = 2f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // Get Starting Pos
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight)*maxOffset / enemyPatrolTime, 0 );
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
