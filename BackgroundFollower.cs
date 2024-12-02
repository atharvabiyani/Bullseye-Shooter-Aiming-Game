using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    public Transform cameraTransform; 
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - cameraTransform.position;
    }

    private void LateUpdate()
    {
        //Background moves relatively to the camera
        transform.position = cameraTransform.position + offset;
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0); 
    }
}
