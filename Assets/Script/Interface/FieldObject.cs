using UnityEngine;
using System.Collections;

public class FieldObject : MonoBehaviour
{
    [SerializeField] MonoBlock DropCube = null;

    public void Drop()
    {
        var newCube = GameObject.Instantiate(DropCube);
        newCube.transform.position = this.transform.position;

        var collider= newCube.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
    }
}
