using UnityEngine;
using DG.Tweening;

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
    public float engineStartSpeed;
    public float EngineLift = 0.0075f;

    public float ForwardForce;
    public float BackwardForce;
    public float TurnForce;
    public float ForwardTiltForce;
    public float TurnTiltForce;

    private float turning = 0f;
    private float TurnForceHelper = 1.5f;

    public LayerMask groundLayer;
    public bool isOnGround;
    private float distanceToGround;


    private Vector2 Movement = Vector2.zero;
    private Vector2 TILTING = Vector2.zero;

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
        HandleEngine();
    }

    protected void FixedUpdate()
    {
        HelicopterHover();
        HelicopterMovements();
        HelicopterTilting();
    }

    void HandleInputs()
    {
        if (!isOnGround)
        {
            Movement.x = Input.GetAxis("Horizontal");
            Movement.y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                EnginePower -= EngineLift;

                if (!isOnGround && EnginePower < 7f)
                {
                    EnginePower = 0;
                }
            }
        }
        
        if (Input.GetAxis("Gas") > 0)
        {
            EnginePower += EngineLift;
        }
        else if (Input.GetAxis("Vertical") > 0 && !isOnGround)
        {
            EnginePower = Mathf.Lerp(EnginePower, 12.5f, 0.003f);
        }
        else if (Input.GetAxis("Gas") < 0.5f && !isOnGround)
        {
            EnginePower = Mathf.Lerp(EnginePower, 10f, 0.003f);
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

    void HandleEngine()
    {
        if (Input.GetKeyDown(KeyCode.X) && isOnGround)
        {
            StartEngine();
        } 
        else if (Input.GetKeyDown(KeyCode.C) && isOnGround)
        {
            StopEngine();
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
            helicopterRigid.AddRelativeForce(Vector3.forward * Mathf.Max(0f, Movement.y * ForwardForce * helicopterRigid.mass));
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            helicopterRigid.AddRelativeForce(Vector3.back * Mathf.Max(0f, -Movement.y * BackwardForce * helicopterRigid.mass));
        }

        float turn = TurnForce * Mathf.Lerp(Movement.x, Movement.x * (TurnForceHelper - Mathf.Abs(Movement.y)), Mathf.Max(0f, Movement.y));
        turning = Mathf.Lerp(turning, turn, Time.fixedDeltaTime * TurnForce);
        helicopterRigid.AddRelativeTorque(0f, turning * helicopterRigid.mass, 0f);
    }

    void HelicopterTilting()
    {
        TILTING.y = Mathf.Lerp(TILTING.y, Movement.y * ForwardTiltForce, Time.deltaTime);
        TILTING.x = Mathf.Lerp(TILTING.x, Movement.x * TurnTiltForce, Time.deltaTime);
        helicopterRigid.transform.localRotation = Quaternion.Euler(TILTING.y, helicopterRigid.transform.localEulerAngles.y, -TILTING.x);
    }

    public void StartEngine()
    {
        DOTween.To(Starting, 0, 12.0f, engineStartSpeed);
    }

    void Starting(float value)
    {
        EnginePower = value;
    }
    public void StopEngine()
    {
        DOTween.To(Stopping, EnginePower, 0.0f, engineStartSpeed);
    }

    void Stopping(float value)
    {
        EnginePower = value;
    }

}
