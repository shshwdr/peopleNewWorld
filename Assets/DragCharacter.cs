using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCharacter : MonoBehaviour
{
    Vector3 mouseDownPosition;
    Vector3 originPosition;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        if (enabled)
        {

            mouseDownPosition = Input.mousePosition;
            originPosition = rb.position;
        }
    }
    private void OnMouseDrag()
    {
        if (enabled)
        {

            rb.MovePosition(originPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(mouseDownPosition));

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
