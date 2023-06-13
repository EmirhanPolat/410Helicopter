using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform targetObject; // the object to follow
    public Vector3 offset = new Vector3(0, 5, -10); // offset from the object

    public float followSpeed = 5f; // speed at which the camera follows the target
    public float lookAtSpeed = 2f; // speed at which the camera looks at the target

    private void FixedUpdate()
    {
        if (targetObject != null)
        {
            // Rotate the offset by the target's y-rotation
            Vector3 rotatedOffset = targetObject.rotation * offset;

            // Calculate the target position based on the target object and offset
            Vector3 targetPosition = targetObject.position + rotatedOffset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Smoothly rotate the camera to look at the target
            Vector3 directionToTarget = targetObject.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
        }
    }
}
