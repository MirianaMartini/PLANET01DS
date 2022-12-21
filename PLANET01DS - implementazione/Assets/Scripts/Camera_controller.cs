using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    private float size;
    private float zoom;
    private Vector3 third_pos;
    private Quaternion third_rot;
    private Vector3 first_pos = new Vector3(0f, 0.36f, 0.2f);
    private Quaternion first_rot = Quaternion.Euler(0f,0f,0f);
    // Start is called before the first frame update
    void Start()
    {
        third_pos = transform.localPosition;
        third_rot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        size = GetComponent<Camera>().orthographicSize;
        zoom = Input.mouseScrollDelta.y * 0.06f;

        if (size < 0.18 && zoom < 0)
        {
            transform.localPosition = first_pos;
            transform.localRotation = first_rot;

            GetComponent<Camera>().orthographic = false;
        }
        else if ( size < 0.18f && zoom > 0f)
        {
            transform.localPosition = third_pos;
            transform.localRotation = third_rot;
            GetComponent<Camera>().orthographic = true;
            GetComponent<Camera>().orthographicSize = 0.23f;
        }
        else if((size > 1.8f && zoom < 0f) || (size > 0.18f && size < 1.8f))
        {
            GetComponent<Camera>().orthographicSize += zoom;
        }

    }
}
