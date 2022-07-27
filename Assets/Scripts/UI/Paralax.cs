using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float parallaxMultiplayer;

    private Transform CameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWith, startPosition;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = Camera.main.transform;
        previousCameraPosition = CameraTransform.position;
        spriteWith = GetComponent<SpriteRenderer>().bounds.size.x;

        startPosition = transform.position.x;
    }

    private void LateUpdate()
    {
        float deltaX = (CameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplayer;
        float moveAmount = CameraTransform.position.x * (1 - parallaxMultiplayer);
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = CameraTransform.position;

        if(moveAmount > startPosition + spriteWith)
        {
            transform.Translate(new Vector3(spriteWith,0,0));
            startPosition += spriteWith;
        }
        else if (moveAmount < startPosition - spriteWith)
        {
            transform.Translate(new Vector3(-spriteWith, 0, 0));
            startPosition -= spriteWith;
        }
    }
}
