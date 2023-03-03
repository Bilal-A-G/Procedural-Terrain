using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private Fog fog;
    [SerializeField] private List<GameObject> uiElements;
    [SerializeField] private GameObject disabledUI;

    private Vector3 _moveDirection;
    private Vector2 _mousePositionLastFrame;
    private Vector2 _mouseDelta;
    private bool _isLooking;
    private bool _overlayActive;
    
    private void Awake()
    {
        _overlayActive = true;
    }

    void Update()
    {
        _moveDirection = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.A))
        {
            _moveDirection.x = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            _moveDirection.x = 1;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _moveDirection.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveDirection.z = -1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _moveDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _moveDirection.y = -1;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _isLooking = true;
            Cursor.visible = false;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            _isLooking = false;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fog.doFog = !fog.doFog;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            _overlayActive = !_overlayActive;
        }

        if (_overlayActive)
        {
            foreach (GameObject ui in uiElements)
            {
                ui.SetActive(true);
            }
            
            disabledUI.SetActive(false);
        }
        else
        {
            foreach (GameObject ui in uiElements)
            {
                ui.SetActive(false);
            }
            
            disabledUI.SetActive(true);
        }

        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _mouseDelta = currentMousePosition - _mousePositionLastFrame;

        Vector3 positionVector = (_moveDirection.z * player.forward + _moveDirection.y * player.up + _moveDirection.x * player.right).normalized;
        player.position +=  positionVector * (speed * Time.deltaTime);

        if (_isLooking)
        {
            player.eulerAngles += new Vector3(-_mouseDelta.y * rotationSpeed * Time.deltaTime, 
                _mouseDelta.x * rotationSpeed * Time.deltaTime, 0);
        }

        _mousePositionLastFrame = currentMousePosition;
    }
}
