using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    Vector3 _mouseScrollScrollStartPos;
    Camera _camera;
    // [SerializeField] int _borderSize = 100;
    // [SerializeField] float ScrollSpeed = 0.1f; 
    [SerializeField] private float _zoomSpeed = 50f;
    public float minCameraSize = 2f, maxCameraSize = 10f;

    public Transform target; // The object the camera should follow
    public Vector2 maxPosition; // The maximum position the camera can reach

    private void UpdateMaxPosition() {
        float sizeRatio = _camera.orthographicSize / minCameraSize;
        maxPosition = new Vector2(maxPosition.x * sizeRatio, maxPosition.y);
    }
    
    void LateUpdate()
    {
        UpdateMaxPosition();
        
        if (target != null)
        {
            // Get the current position of the camera
            Vector3 newPosition = transform.position;

            // Clamp the camera's position within the maximum thresholds
            newPosition.x = Mathf.Clamp(target.position.x, -maxPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(target.position.y, -maxPosition.y, maxPosition.y);

            // Update the camera's position
            transform.position = newPosition;
        }
    }

    // Start is called before the first frame update Unity Message | 0 references
    void Start()
    { 
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMiddleMouseScrolling();

        HandleWheelZoom();

        // RestrictToMapRect();
    }

    private void HandleWheelZoom() 
    {
        if (Input.mouseScrollDelta.y != 0) 
        { 
            _camera.orthographicSize -= Input.mouseScrollDelta.y * Time.deltaTime * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minCameraSize, maxCameraSize);
        }
    }
    
    //  This would fix the camera movement for the border thingy, but you need a "Map" which is based on that game the dude is making.
    //  See https://www.youtube.com/watch?v=IfbMKe6p9nM&t=510s 

    /* private void RestrictToMapRect() {

        Vector2 margins = myCamera.ScreenToWorldPoint(Vector2.zero) - myCamera.ScreenToWorldPoint (new Vector2(myCamera.pixelWidth, myCamera.pixelHeight));

        Rect boundaries = Map.main.GetMapRect();
        boundaries.width -= margins.x;
        boundaries.x += margins.x/2;
        boundaries.height -= margins.y;
        boundaries.y += margins.y/2;
        if (boundaries.xMin > myCamera.transform.position.x)
        myCamera.transform.position= new Vector3(boundaries.xMin, myCamera.transform.position.y, myCamera.transform.position.z); 
        if (boundaries.xMax < myCamera.transform.position.x)
        myCamera.transform.position= new Vector3(boundaries.xMax, myCamera.transform.position.y, myCamera.transform.position.z); 
        if (boundaries.yMin > myCamera.transform.position.y)
        myCamera.transform.position = new Vector3(myCamera.transform.position.x, boundaries.yMin, myCamera.transform.position.z); 
        if (boundaries.yMax < myCamera.transform.position.y)
        myCamera.transform.position = new Vector3(myCamera.transform.position.x, boundaries.yMax, myCamera.transform.position.z);
    } */


    private void HandleMiddleMouseScrolling() {
        if (Input.GetMouseButtonDown(2))
        {
            _mouseScrollScrollStartPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 movement= Vector3.zero;
            movement = _camera.ScreenToWorldPoint(Input.mousePosition) - _mouseScrollScrollStartPos;
            _camera.transform.position -= movement;
        }
    }
}
