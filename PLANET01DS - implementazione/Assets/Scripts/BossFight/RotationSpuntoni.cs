using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpuntoni : MonoBehaviour
{
    [SerializeField] private float rotationVerse = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, rotationVerse * 120f * Time.deltaTime);
        //transform.Rotate(transform.forward, rotationVerse * 120f * Time.deltaTime);
        //transform.Rotate(new Vector3(rotationVerse * 120f, 0f, 0f) * Time.deltaTime);
    }
}
