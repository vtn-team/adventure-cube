using UnityEngine;
using System.Collections;

public class Targetcube : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool is_hit = Physics.Raycast(ray, out hit);
            if (is_hit)
            {
                this.transform.position = hit.point;
            }
        }
    }
}
