using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consumamablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    public Sprite normQuestionBox;
    public GameObject coin;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;
            rigidBody.AddForce(new Vector2(0,rigidBody.mass*20), ForceMode2D.Impulse);
            Instantiate(consumamablePrefab, 
            new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), 
            Quaternion.identity);
            StartCoroutine(DisableHittable());
            // Respawn assigned coin
            if (coin != null)
            {
                coin.GetComponent<CoinEffectController>().ShowCoin(true);
            }
            
        }
    }

    public void ResetQuestionBox()
    {
        hit = false;
        springJoint.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.sprite = normQuestionBox;
    }

    bool  ObjectMovedAndStopped(){
        return  Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator  DisableHittable(){
        if (!ObjectMovedAndStopped()){
            yield  return  new  WaitUntil(() =>  ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite  =  usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType  =  RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition  =  Vector3.zero;
        springJoint.enabled = false; // disable spring
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
