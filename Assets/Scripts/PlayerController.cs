using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    [SerializeField]
    private ParticleSystem somethingWeird;
    public float speed;
    public float upSpeed;
    public float maxSpeed = 10;
    public bool onGroundState = true;
    private bool faceRightState = true;
    private bool currentFaceRight = true;
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int coins = 0;
    private bool countScoreState = false;
    public bool deathState = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
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

        // Extra: Jump Higher while spacebar is held
        if (Input.GetKey("space")){
            marioBody.gravityScale = 5;
            if (onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                countScoreState = true;
            }
        }
        else {
            marioBody.gravityScale = 10;
        }

        // Jump Impulse Physics
        /* if (Input.GetKey("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        } */
    }

    // Added Collision Direction Detection. Ref: https://youtu.be/iXMID4Ow_l8
    void OnCollisionEnter2D(Collision2D col)
    {
        
        /* if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle")){
            onGroundState = true;
            countScoreState = false;
            scoreText.text = "Score :\n" + score.ToString();
        } */
        foreach(ContactPoint2D contactPoint in col.contacts)
        {
            //Debug.Log(contactPoint.normal);
            if(contactPoint.normal.y > 0)
            {
                Debug.Log("Mario is On something. It might be shrooms");
                onGroundState = true;
                countScoreState = false;
                scoreText.text = "Score :\n" + score.ToString();
                somethingWeird.Play();
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Goomba!");
            deathState = true;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collided with Coin!");
            coins++;
            score += 10;
        }
    }

    // Update is called once per frame
    // Change: Simplified Flipping to match Input Vector and prevent Moonwalking when both keys are pressed (Ref: https://youtu.be/pKZ7FIL2csU?t=640)
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround",onGroundState);
        faceRightState = Input.GetAxis("Horizontal")<0; //Maintained for potential future use
        if (Input.GetKey("a")||Input.GetKey("d"))
        {
            marioSprite.flipX = (Input.GetAxis("Horizontal")<0);
            if (Mathf.Abs(marioBody.velocity.x)>1.0 && (currentFaceRight == marioSprite.flipX))
            {
                marioAnimator.SetTrigger("onSkid");
                currentFaceRight = !marioSprite.flipX;
            }
            else if (currentFaceRight == marioSprite.flipX)
            {
                currentFaceRight = !marioSprite.flipX;
            }
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

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
