using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    public Transform startLimit;
    public Transform ceiling;
    private float offsetX; // initial x-offset between camera and Mario
    private float offsetY; // initial y-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float startY;
    private float endY;
    private float viewportHalfWidth;
    private float viewportHalfHeight;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        viewportHalfHeight = Mathf.Abs(bottomLeft.y - this.transform.position.y);

        offsetX = this.transform.position.x - player.position.x;
        offsetY = this.transform.position.y - player.position.y;

        startX = startLimit.transform.position.x + viewportHalfWidth;
        endX = endLimit.transform.position.x - viewportHalfWidth;

        startY = this.transform.position.y;
        endY = ceiling.transform.position.y - viewportHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offsetX;
        float desiredY = player.position.y + offsetY;

        desiredX = Mathf.Clamp(desiredX, startX, endX);

        // Look Down Feature
        if(Input.GetKey("s"))
        {
            desiredY = Mathf.Clamp(desiredY, startY, endY);
            desiredY -= 3;
        }
        desiredY = Mathf.Clamp(desiredY, startY, endY);

        if(desiredX >= startX && desiredX <= endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }

        if(desiredY >= startY && desiredY <= endY)
        {
            this.transform.position = new Vector3(this.transform.position.x, desiredY, this.transform.position.z);
        }
        
    }
}
