using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliScript : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float responsiveness = 500f;
    [SerializeField] private float gasAmount = 25f;
    [SerializeField] private float rotorSpeedModifier = 10f;
    [SerializeField] private Transform rotorTransform;
    private float gas;

    private float horizontal;
    private float vertical;
    private float diagonal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandeInputs();

        rotorTransform.Rotate(Vector3.up * gas * rotorSpeedModifier);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.up * gas, ForceMode.VelocityChange);

        _rigidbody.AddTorque(transform.right * horizontal * responsiveness);
        _rigidbody.AddTorque(transform.forward * vertical * responsiveness);
        _rigidbody.AddTorque(transform.up * diagonal * responsiveness);
    }

    private void HandeInputs()
    {
        horizontal = Input.GetAxis("Vertical");
        vertical = Input.GetAxis("Horizontal"); 
        diagonal = Input.GetAxis("Diagonal");

        if (Input.GetKey(KeyCode.Space))
        {
            gas += gasAmount * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.LeftShift))
        {
            gas -= gasAmount * Time.deltaTime;
        }
        


        gas = Mathf.Clamp(gas ,0f, 100f);
    }
}
