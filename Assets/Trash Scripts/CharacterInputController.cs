using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour {

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;
    private float filteredStrafeInput = 0f;

    public bool InputMapToCircular = true;
    public float cameraYSpeed = 3f;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;

    private float speed = 10f;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }
    public float Strafe
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    private float mouseYaw;
    public float MouseYaw
    {
        get
        {
            return mouseYaw;
        }
        private set
        {
            mouseYaw = Mathf.Clamp(value, -10f, 10f);
            if (Mathf.Abs(mouseYaw) < 0.01f)
                mouseYaw = 0f;
        }
    }


    private float mousePitch;
    public float MousePitch
    {
        get
        {
            return mousePitch;
        }
        private set
        {
            mousePitch = Mathf.Clamp(value, -30f, 10f);
            if (Mathf.Abs(mousePitch) < 0.01f)
                mousePitch = 0f;
        }
    }
        

	void Update () {
		
        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float strafe = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
        //Debug.Log(string.Format("h:{0}", h));
        //Debug.Log(string.Format("v:{0}", v));

        float xAxis, yAxis;
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");
        yAxis = yAxis * cameraYSpeed;
        // Now turn using "Mouse"

        if (Mathf.Abs(xAxis) < 0.01f)
        {
            MouseYaw = Mathf.Lerp(MouseYaw, 0f, 0.1f);
            xAxis = 0f;
        }
        else
            MouseYaw += speed * xAxis;

        if (Mathf.Abs(yAxis) < 0.01f)
        {
            MousePitch = Mathf.Lerp(MousePitch, 0f, 0.1f);
            yAxis = 0f;
        }
        else 
            MousePitch -= speed * yAxis;

        float h = xAxis * 5f * turnInputFilter;

        //MouseYaw += speed * Input.GetAxis("Yaw");
        //MousePitch -= speed * Input.GetAxis("Pitch");
        //Debug.Log("Yaw: " + MouseYaw.ToString());
        //Debug.Log("Pitch: " + MousePitch.ToString());
        //Debug.Log("xAxis: " + xAxis.ToString());

        //if (InputMapToCircular)
        //{
        //    // make coordinates circular
        //    //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
        //    h = h * Mathf.Sqrt(1f - 0.5f * v * v);
        //    v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        //}


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        if (Input.GetKeyUp(KeyCode.Alpha1))
            forwardSpeedLimit = 0.1f;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            forwardSpeedLimit = 0.2f;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            forwardSpeedLimit = 0.3f;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            forwardSpeedLimit = 0.4f;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            forwardSpeedLimit = 0.5f;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            forwardSpeedLimit = 0.6f;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            forwardSpeedLimit = 0.7f;
        else if (Input.GetKeyUp(KeyCode.Alpha8))
            forwardSpeedLimit = 0.8f;
        else if (Input.GetKeyUp(KeyCode.Alpha9))
            forwardSpeedLimit = 0.9f;
        else if (Input.GetKeyUp(KeyCode.Alpha0))
            forwardSpeedLimit = 1.0f;
        //END ANALOG ON KEYBOARD DEMO CODE  


        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v, Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);
        filteredStrafeInput = Mathf.Clamp(Mathf.Lerp(filteredStrafeInput, strafe, Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);
        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h, Time.deltaTime * turnInputFilter);


        Forward = filteredForwardInput;
        Turn = filteredTurnInput;
        Strafe = filteredStrafeInput;


        //Capture "fire" button for action event
        Action = Input.GetButtonDown("Fire1");
        //if (Action)
        //    Debug.Log("Fire1");

        Jump = Input.GetButtonDown("Jump");
        //if (Jump)
        //    Debug.Log("Jump");
    }
}
