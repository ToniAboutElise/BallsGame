using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _auxForward;
    private SceneObject _sceneObject;
    public bool _isRotating = false;
    private bool _hasRotated = false;
    public Vector3 currentEulerAngles;
    public Vector3 targetEulerAngles;

    private RotationType rotationType = RotationType.Null; 

    private enum RotationType
    {
        Null,
        Left,
        Right
    }

    private void Start()
    {
        currentEulerAngles = transform.eulerAngles;
        targetEulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y - 90, currentEulerAngles.z);
    }

    private void Velocity()
    {
        if(_isRotating == false) 
        { 
            _rigidBody.velocity = _auxForward.forward;
        }
        else
        {
            _rigidBody.velocity = new Vector3(0,0,0);
        }
    }

    private void Rotation()
    {
        if(_sceneObject != null && Vector3.Distance(transform.localPosition, _sceneObject.transform.position) < 0.07f && _hasRotated == false)
        {   
            if(rotationType != RotationType.Null && _isRotating == false && _hasRotated == false)
            {
                _isRotating = true;
                _hasRotated = false;
                transform.position = _sceneObject.transform.position;
                StartCoroutine(SetNewMapRotation(rotationType));
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

    private IEnumerator SetNewMapRotation(RotationType rotationType)
    {
        currentEulerAngles = transform.localEulerAngles;
        targetEulerAngles = new Vector3(0,0,0);
        switch (rotationType)
        {
            case RotationType.Left:
                targetEulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y - 90, currentEulerAngles.z);
                break;
            case RotationType.Right:
                targetEulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y + 90, currentEulerAngles.z);
                break;
        }
        _isRotating = true;
        //transform.eulerAngles = Vector3.Lerp(currentEulerAngles, targetEulerAngles, Time.deltaTime*1.2f);
        //transform.eulerAngles = targetEulerAngles;
        yield return new WaitForSeconds(3);
        _isRotating = false;
        _hasRotated = true;
    }

    private void GetRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationType = RotationType.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationType = RotationType.Right;
        }
        else
        {
            rotationType = RotationType.Null;
        }
    }

    private IEnumerator PerformPlayerRotation()
    {
        //if(_isRotating == true)
        transform.eulerAngles -= new Vector3(0,0.1f,0);
        yield return new WaitForSeconds(0.001f);
        Debug.Log(transform.eulerAngles.y + " "+ targetEulerAngles.y);
        if(transform.eulerAngles.y > targetEulerAngles.y)
        {
            StartCoroutine(PerformPlayerRotation());
        }
        else
        {
            transform.eulerAngles = targetEulerAngles;
            targetEulerAngles -= new Vector3(0, 90, 0);
        }
    }

    void LateUpdate()
    {
        //GetRotationInput();
        //Velocity();
        //Rotation();
        //PerformPlayerRotation();
        //Debug.Log(transform.localEulerAngles);
        Debug.Log(transform.eulerAngles.y);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(PerformPlayerRotation());
        }
    }
}
