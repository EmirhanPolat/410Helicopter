using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliEngine : MonoBehaviour
{
    Rigidbody helicopterRigid;
    public BladesController MainBlade;
    public BladesController TailBlade;

    private float enginePower;
    public float EnginePower
    {
        get { return enginePower; }
        set { MainBlade.BladeSpeed = value * 250;
            TailBlade.BladeSpeed = value * 500;
            enginePower = value;
        }
    }

    public float effectiveHeight;
    public float EngineLift = 0.0075f;

    public LayerMask groundLayer;
    public bool isOnGround;
    private float distanceToGround;

    private Vector2 movement = Vector2.zero;
    public float ForwardForce;
    public float BackwardForce;
    public float TurnForce;
    private float TurnForceHelper;
    private float turning = 0f;
    // Start is called before the first frame update
    void Start()
    {
        helicopterRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGroundCheck();
        HandleInputs();
    }

    protected void FixedUpdate()
    {
        HelicopterHover();
        HelicopterMovements();
    }

    void HandleInputs()
    {
        if (!isOnGround)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                EnginePower -= EngineLift;

                if (EnginePower < 0)
                {
                    EnginePower = 0;
                }
            }
        }
        
        if (Input.GetAxis("Gas") > 0)
        {
            EnginePower += EngineLift;
        } 

    }

    void HandleGroundCheck()
    {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        Ray ray = new Ray(transform.position, direction);

        if(Physics.Raycast(ray, out hit, 3000, groundLayer))
        {
            distanceToGround = hit.distance;

            if(distanceToGround < 2)
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
        }
    }
    void HelicopterHover()
    {
        float upForce = 1 - Mathf.Clamp(helicopterRigid.transform.position.y / effectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0, EnginePower, upForce) * helicopterRigid.mass;
        helicopterRigid.AddRelativeForce(Vector3.up * upForce);
    }

    void HelicopterMovements()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            helicopterRigid.AddForce(Vector3.forward * Mathf.Max(0f, movement.y * ForwardForce * helicopterRigid.mass));
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            helicopterRigid.AddForce(Vector3.back * Mathf.Max(0f, -movement.y * BackwardForce * helicopterRigid.mass));
        }
        float turn = TurnForce * Mathf.Lerp(movement.y, movement.x * (TurnForceHelper - Mathf.Abs(movement.y)), Mathf.Max(0f,movement.y));
        turning = Mathf.Lerp(turning, turn, Time.fixedDeltaTime * TurnForce);
        helicopterRigid.AddRelativeTorque(0f, turning * helicopterRigid.mass, 0f);
    }
}
