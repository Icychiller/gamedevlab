using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    [SerializeField]
    private ParticleSystem somethingWeird;
    public Sprite deadSprite;
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
    public bool deathState = false;
    public bool godMode = false;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    public void GodMode(bool state)
    {
        CentralManager.centralManagerInstance.setGodMode(state);
        this.godMode = state;
    }

    void FixedUpdate()
    {
        if (!deathState)
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
                }
            }
            else {
                marioBody.gravityScale = 10;
            }
        }
    }

    // Added Collision Direction Detection. Ref: https://youtu.be/iXMID4Ow_l8
    void OnCollisionEnter2D(Collision2D col)
    {
        foreach(ContactPoint2D contactPoint in col.contacts)
        {
            //Debug.Log(contactPoint.normal);
            if(contactPoint.normal.y > 0)
            {
                //Debug.Log("Mario is On something. It might be shrooms");
                onGroundState = true;
                somethingWeird.Play();
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Goomba!");
            //deathState = true;
            if (godMode)
            {
                other.gameObject.SetActive(false);
            }
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

        if (Input.GetKey("z"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
        }
        if (Input.GetKey("x"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
        }
        if (Input.GetKey("c"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.C, this.gameObject);
        }
    }

    void PlayerDiesSequence()
    {
        this.deathState = true;
        this.marioBody.bodyType = RigidbodyType2D.Kinematic;
        this.marioSprite.sprite = deadSprite;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Rotator>().enabled = true;
        StartCoroutine(floatAndSpinDeath());
    }

    IEnumerator floatAndSpinDeath()
    {
        for (int i = 0; i<300; i++)
        {
            this.marioBody.MovePosition(new Vector2(this.marioBody.position.x, this.marioBody.position.y + 0.02f));
            yield return new WaitForEndOfFrame();
        }
        
    }

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
