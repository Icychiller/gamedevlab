using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public float speed;
    public float upSpeed;
    public float maxSpeed = 10;
    private bool onGroundState = true;
    private bool faceRightState = true;
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    private bool countScoreState = false;
    public bool deathState = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Accelerate Movement
        if (Mathf.Abs(moveHorizontal)>0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement*speed);
            }
        }
        
        // Stop movement
        // Change 1: Replace with GetKey as GetKeyUp is single frame bool (minimise lag issue)
        // Change 2: Replace Zero vector to maintain Y-Velocity
        if (!Input.GetKey("a") && !Input.GetKey("d")){
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }

        // Jump Impulse Physics
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")){
            onGroundState = true;
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Goomba!");
            deathState = true;
        }
    }

    // Update is called once per frame
    // Change: Simplified Flipping to match Input Vector and prevent Moonwalking when both keys are pressed (Ref: https://youtu.be/pKZ7FIL2csU?t=640)
    void Update()
    {
        faceRightState = Input.GetAxis("Horizontal")<0; //Maintained for potential future use
        if (Input.GetKey("a")||Input.GetKey("d"))
        {
            marioSprite.flipX = (Input.GetAxis("Horizontal")<0);
        }

        if (!onGroundState && countScoreState)
        {
            if(Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
        
    }
}
