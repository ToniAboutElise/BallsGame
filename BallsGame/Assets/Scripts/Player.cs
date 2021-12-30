using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _mapTransform;
    [SerializeField] private Rigidbody _rigidBody;
    private SceneObject _sceneObject;
    private bool _isRotating = false;
    private bool _hasRotated = false;

    private RotationType rotationType = RotationType.Null; 

    private enum RotationType
    {
        Null,
        Left,
        Right
    }

    private void Velocity()
    {
        if(_isRotating == false)
        _rigidBody.velocity = Vector3.forward;
    }

    private void Rotation()
    {
        if(_sceneObject != null && Vector3.Distance(transform.localPosition, _sceneObject.transform.position) < 0.07f && _hasRotated == false)
        {
            transform.position = _sceneObject.transform.position;

            if(rotationType != RotationType.Null)
            {
                //Start rotation here
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
            _hasRotated = false;
        }
    }

    private void SetNewMapRotation(RotationType rotationType)
    {
        Vector3 currentEulerAngles = _mapTransform.localEulerAngles;
        Vector3 targetEulerAngles = new Vector3(0,0,0);

        switch (rotationType)
        {
            case RotationType.Left:
                targetEulerAngles = new Vector3(_mapTransform.localEulerAngles.x, _mapTransform.localEulerAngles.y + 90, _mapTransform.localEulerAngles.z);
                break;
            case RotationType.Right:
                targetEulerAngles = new Vector3(_mapTransform.localEulerAngles.x, _mapTransform.localEulerAngles.y - 90, _mapTransform.localEulerAngles.z);
                break;
        }

        _mapTransform.transform.eulerAngles = Vector3.Lerp(currentEulerAngles, targetEulerAngles, Time.deltaTime*1.2f);
        _isRotating = false;
        _hasRotated = true;
    }

    private void GetRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotationType = RotationType.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotationType = RotationType.Right;
        }
        else
        {
            rotationType = RotationType.Null;
        }
    }

    void FixedUpdate()
    {
        GetRotationInput();
        //Velocity();
        Rotation();
        Debug.Log(_mapTransform.localEulerAngles);
    }
}
