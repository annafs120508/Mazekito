using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _7x3Camera;
    [SerializeField] private Camera _9x4Camera;
    [SerializeField] private Camera _13x6Camera;
    [SerializeField] private Camera _20x9Camera;
    private string _cameraSize;

    private void Start()
    {
        _mainCamera.gameObject.SetActive(false);
        _7x3Camera.gameObject.SetActive(false);
        _9x4Camera.gameObject.SetActive(false);
        _13x6Camera.gameObject.SetActive(false);
        _20x9Camera.gameObject.SetActive(false);
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

            case "Medium":
                _mainCamera.gameObject.SetActive(false);
                _9x4Camera.gameObject.SetActive(true);
                break;

            case "SemiLarge":
                _mainCamera.gameObject.SetActive(false);
                _13x6Camera.gameObject.SetActive(true);
                break;

            case "Large":
                _mainCamera.gameObject.SetActive(false);
                _20x9Camera.gameObject.SetActive(true);
                break;
        }
    }
}
