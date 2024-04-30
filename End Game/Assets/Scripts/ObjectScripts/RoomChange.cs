using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for changing room, less usable after cinemachine revolution, now only shows the name of some rooms
public class RoomChange : MonoBehaviour
{
    public Vector2 cameraMaxPositionChange;
    public Vector2 cameraMinPositionChange;
    public Vector3 playerPositionChange;
    private CameraMovement _cameramove;
    public bool needText;
    public string roomName;
    public GameObject text;
    public Text writeText;

    void Start()
    {
        _cameramove = Camera.main.GetComponent<CameraMovement>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _cameramove.minPosition += cameraMinPositionChange;
            _cameramove.maxPosition += cameraMaxPositionChange;
            other.transform.position += playerPositionChange;
            if (needText)
            {
                StartCoroutine(ChangeRoomName());
            }
        }
    }
    
    private IEnumerator ChangeRoomName()
    {
        text.SetActive(true);
        writeText.text = roomName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}