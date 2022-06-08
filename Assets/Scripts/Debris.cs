using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Rigidbody2D debrisBody;
    private Vector3 scalar;
    // Start is called before the first frame update
    private float life = 0f;
    void Start()
    {
        debrisBody = GetComponent<Rigidbody2D>();
        debrisBody.AddForce(new Vector2(0,Random.Range(0f,1f)) * 15, ForceMode2D.Impulse);
        debrisBody.AddForce(new Vector2(Random.Range(-1f,1f),0) * 5, ForceMode2D.Impulse);

        scalar = transform.localScale / 30f;
        StartCoroutine("ScaleOut");

    }

    IEnumerator ScaleOut()
    {
        debrisBody.AddTorque(10,ForceMode2D.Impulse);
        yield return null;

        for (int step = 0; step < 30; step++)
        {
            this.transform.localScale = this.transform.localScale - scalar;
            yield return null;
        }

        Destroy(gameObject);
    }
}
