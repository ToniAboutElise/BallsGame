using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _velocity;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _auxForward;
    private SceneObject _sceneObject;
    public bool _isRotating = false;
    private bool _hasRotated = false;

    private CurrentRotationDegrees currentRotationDegrees = CurrentRotationDegrees._0;

    private enum CurrentRotationDegrees
    {
        _0,
        _90,
        _180,
        _270
    }

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
        { 
            _rigidBody.velocity = _auxForward.forward * _velocity;
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
                StartCoroutine(PerformPlayerRotation(rotationType));
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

    private IEnumerator PerformPlayerRotation(RotationType rotationType)
    {
        _isRotating = true;
        string triggerString = "";
        switch (rotationType)
        {
            case RotationType.Left:
                switch (currentRotationDegrees)
                {
                    case CurrentRotationDegrees._0:
                        _animator.SetTrigger("270");
                        triggerString = "270";
                        currentRotationDegrees = CurrentRotationDegrees._270;
                        break;
                    case CurrentRotationDegrees._90:
                        _animator.SetTrigger("0");
                        triggerString = "0";
                        currentRotationDegrees = CurrentRotationDegrees._0;
                        break;
                    case CurrentRotationDegrees._180:
                        _animator.SetTrigger("90");
                        triggerString = "90";
                        currentRotationDegrees = CurrentRotationDegrees._90;
                        break;
                    case CurrentRotationDegrees._270:
                        _animator.SetTrigger("180");
                        triggerString = "180";
                        currentRotationDegrees = CurrentRotationDegrees._180;
                        break;
                }
                break;
            case RotationType.Right:
                switch (currentRotationDegrees)
                {
                    case CurrentRotationDegrees._0:
                        _animator.SetTrigger("90");
                        triggerString = "90";
                        currentRotationDegrees = CurrentRotationDegrees._90;
                        break;
                    case CurrentRotationDegrees._90:
                        _animator.SetTrigger("180");
                        triggerString = "180";
                        currentRotationDegrees = CurrentRotationDegrees._180;
                        break;
                    case CurrentRotationDegrees._180:
                        _animator.SetTrigger("270");
                        triggerString = "270";
                        currentRotationDegrees = CurrentRotationDegrees._270;
                        break;
                    case CurrentRotationDegrees._270:
                        _animator.SetTrigger("0");
                        triggerString = "0";
                        currentRotationDegrees = CurrentRotationDegrees._0;
                        break;
                }
                break;
        }
        yield return new WaitForSeconds(0.55f);
        _animator.ResetTrigger(triggerString);
        _isRotating = false;
        _hasRotated = true;
    }

    void LateUpdate()
    {
        GetRotationInput();
        Velocity();
        Rotation();
    }
}