using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameConstants gameConstants;
    private Vector3 rotator;

    // Start is called before the first frame update
    void Start()
    {
        rotator = new Vector3(0, gameConstants.rotatorSpeed,0);
    }

    // Update is called once per frame
    void Update()
    {
        // Spin baby spin
        this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles - rotator);
    }
}
