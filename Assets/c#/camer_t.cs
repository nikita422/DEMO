using UnityEngine;
using System.Collections;

public class camer_t : MonoBehaviour
{
   
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Diference;
        }
      
      


        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0)
        {
           GetComponent<Camera>().orthographicSize *= 1 - zoom;
        }

    }
}