using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public ParticleSystem teleportParticleSystem;
    [SerializeField] private Transform _otherPortal;
    public Transform respawnTransform;
    private bool _canMoveCamera = false;
    private bool _reactivatingCooldown = false;
    private Player _player;
    private Transform _cameraTransform;
    private Vector3 _originalCameraLocalPosition;
    public void Teleport(Transform camera, Player player)
    {
        StartCoroutine(TeleportCR(camera, player));
    }

    private IEnumerator TeleportCR(Transform camera, Player player)
    {
        //teleportParticleSystem.Play();

        if (_player == null)
        {
            _player = player;
        }

        _player.canRotate = false;
        _cameraTransform = camera;
        _originalCameraLocalPosition = camera.localPosition;
        _canMoveCamera = true;
        player.canRun = false;
        camera.SetParent(null);
        _otherPortal.GetComponent<BoxCollider>().enabled = false;
        player.transform.position = _otherPortal.GetComponent<Portal>().respawnTransform.position;
        //_otherPortal.GetComponent<Portal>().teleportParticleSystem.Play();
        camera.SetParent(player.transform);
        MoveCamera(player, _cameraTransform, _originalCameraLocalPosition);
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator PortalReactivationCooldown()
    {
        _reactivatingCooldown = true;
        yield return new WaitForSeconds(0.8f);
        _otherPortal.GetComponent<BoxCollider>().enabled = true;
        _reactivatingCooldown = false;
        _canMoveCamera = false;
    }

    private void MoveCamera(Player player, Transform cameraTransform, Vector3 targetLocalPosition)
    {
        cameraTransform.localPosition = Vector3.MoveTowards(cameraTransform.localPosition, targetLocalPosition, 20 * Time.deltaTime);
        if(cameraTransform.localPosition == targetLocalPosition)
        {
            player.canRun = true;
            player.canRotate = true;
            if (_reactivatingCooldown == false)
            StartCoroutine(PortalReactivationCooldown());
        }
    }

    private void Update()
    {
        if(_canMoveCamera == true)
        {
            MoveCamera(_player, _cameraTransform, _originalCameraLocalPosition);
        }
    }
}
