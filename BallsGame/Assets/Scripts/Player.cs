using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Collider _collider;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private float _velocity;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Transform _auxForward;
    private Collectable _collectable;
    public bool canRun = true;
    public bool canRotate = true;
    public bool isRotating = false;
    private bool _hasRotated = false;
    private PlayerInputActions playerInputActions;
    private float _rotationFloat = 0;

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

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Velocity()
    {
        if(isRotating == false && canRun == true) 
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
        if(_collectable != null && Vector3.Distance(transform.localPosition, _collectable.transform.position) < 0.5f && _hasRotated == false && canRotate == true)
        {   
            if(rotationType != RotationType.Null && isRotating == false)
            {
                isRotating = true;
                StartCoroutine(PerformPlayerRotation(rotationType));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Lose();
    }

    private void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collectable>() == true)
        {
            _collectable = other.GetComponent<Collectable>();

            if(other.GetComponent<Collectable>().GetCollectableState() == Collectable.CollectableState.Collected)
            {
                Lose();
                return;
            }

            if(other.GetComponent<Collectable>().GetCollectableState() == Collectable.CollectableState.ProtectedByAdditionalEffect)
            {
                other.GetComponent<Collectable>().AdditionalEffect();
                return;
            }

            if (other.GetComponent<Collectable>().GetCollectableState() == Collectable.CollectableState.NonCollected) 
            { 
                _levelManager.CollectableGrabbed(_collectable);
            }
        }
        else if (other.tag == "GoalStar")
        {
            LevelFinished();
        }
        else if (other.tag == "Portal")
        {
            other.GetComponent<Portal>().Teleport(_camera.transform, this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collectable>() == true)
        {
            _collectable = null;
            _hasRotated = false;
        }
    }

    private void GetRotationInput()
    {
        if (_rotationFloat < -0.2f)
        {
            rotationType = RotationType.Left;
        }
        else if (_rotationFloat > 0.2f)
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
        isRotating = true;
        string triggerString = "";
        switch (rotationType)
        {
            case RotationType.Left:
                switch (currentRotationDegrees)
                {
                    case CurrentRotationDegrees._0:
                        _cameraAnimator.SetTrigger("270");
                        triggerString = "270";
                        currentRotationDegrees = CurrentRotationDegrees._270;
                        break;
                    case CurrentRotationDegrees._90:
                        _cameraAnimator.SetTrigger("0");
                        triggerString = "0";
                        currentRotationDegrees = CurrentRotationDegrees._0;
                        break;
                    case CurrentRotationDegrees._180:
                        _cameraAnimator.SetTrigger("90");
                        triggerString = "90";
                        currentRotationDegrees = CurrentRotationDegrees._90;
                        break;
                    case CurrentRotationDegrees._270:
                        _cameraAnimator.SetTrigger("180");
                        triggerString = "180";
                        currentRotationDegrees = CurrentRotationDegrees._180;
                        break;
                }
                break;
            case RotationType.Right:
                switch (currentRotationDegrees)
                {
                    case CurrentRotationDegrees._0:
                        _cameraAnimator.SetTrigger("90");
                        triggerString = "90";
                        currentRotationDegrees = CurrentRotationDegrees._90;
                        break;
                    case CurrentRotationDegrees._90:
                        _cameraAnimator.SetTrigger("180");
                        triggerString = "180";
                        currentRotationDegrees = CurrentRotationDegrees._180;
                        break;
                    case CurrentRotationDegrees._180:
                        _cameraAnimator.SetTrigger("270");
                        triggerString = "270";
                        currentRotationDegrees = CurrentRotationDegrees._270;
                        break;
                    case CurrentRotationDegrees._270:
                        _cameraAnimator.SetTrigger("0");
                        triggerString = "0";
                        currentRotationDegrees = CurrentRotationDegrees._0;
                        break;
                }
                break;
        }
        yield return new WaitForSeconds(0.55f);
        _cameraAnimator.ResetTrigger(triggerString);
        isRotating = false;
        _hasRotated = true;
    }

    public void SetHorizontalRotationValue()
    {
        _rotationFloat = playerInputActions.Player.Move.ReadValue<Vector2>().x;
    }
    private void FixPositionWhileRotating()
    {
        if (isRotating == true)
        {
            transform.localPosition = _collectable.transform.position;
        }
    }
    private void LevelFinished()
    {
        _velocity = 0;
        _collider.enabled = false;
        _characterAnimator.SetTrigger("dance");
        StartCoroutine(LevelFinishedCR());
    }

    private IEnumerator LevelFinishedCR()
    {
        LevelEndSaveFile.SaveFileEndLevel(_levelManager.nextLevel);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("LevelSelection", LoadSceneMode.Single);
    }

    void LateUpdate()
    {
        SetHorizontalRotationValue();
        GetRotationInput();
        Velocity();
        Rotation();
        FixPositionWhileRotating();
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelection", LoadSceneMode.Single);
        }
        */
    }
}