using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _7x3Camera;
    private string _cameraSize;

    private void Start()
    {
        ActivateCameraBasedOnSize();
    }

    private void ActivateCameraBasedOnSize()
    {
        _cameraSize = PlayerPrefs.GetString("CameraSize", _cameraSize);

        _7x3Camera.gameObject.SetActive(false);

        switch (_cameraSize)
        {
            case "Small":
                _mainCamera.gameObject.SetActive(false);
                _7x3Camera.gameObject.SetActive(true);
                break;
        }
    }
}
