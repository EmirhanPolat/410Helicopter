using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform targetObject; // the object to follow
    public Vector3 offset; // offset from the object
    private void FixedUpdate()
    {
        if (targetObject != null)
        {
            // Lerp the position of this object towards the target's position + offset
            transform.position = targetObject.position + offset;
        }
    }
}