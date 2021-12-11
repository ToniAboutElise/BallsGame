using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    private SceneObject _sceneObject;
    private bool _isRotating = false;

    private void Velocity()
    {
        if(_isRotating == false)
        _rigidBody.velocity = Vector3.forward;
    }

    private void Rotation()
    {
        if(_sceneObject != null && Vector3.Distance(transform.localPosition, _sceneObject.transform.position) < 0.07f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = _sceneObject.transform.position;
                _isRotating = true;
                _rigidBody.velocity = Vector3.zero;
                Quaternion currentRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y-90, transform.localRotation.z, transform.localRotation.w);
                Debug.Log(transform.localRotation + " " + currentRotation);
                //StartCoroutine()
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SceneObject>() == true)
        {
            _sceneObject = other.GetComponent<SceneObject>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SceneObject>() == true)
        {
            _sceneObject = null;
        }
    }

    void Update()
    {
        Velocity();
        Rotation();
    }
}
