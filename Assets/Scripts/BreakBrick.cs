using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool broken = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        foreach(ContactPoint2D contactPoint in col.contacts)
        {
            //Debug.Log(contactPoint.normal);
            if(contactPoint.normal.y > 0 && col.gameObject.CompareTag("Player"))
            {
                for (int x = 0; x < 6; x++)
                {
                    Instantiate(prefab, transform.position, Quaternion.identity);
                    broken = true;
                }
                break;
            }
            
        }
    }
}
