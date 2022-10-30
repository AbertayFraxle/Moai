using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100;

    float xRotation = 0f;

    [SerializeField] Transform playerBody;

    [SerializeField] GameObject settingsMenu;
    bool isMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isMenu = false;
        mouseSensitivity = 800 * PlayerPrefs.GetFloat("sensitivity", 0.5f) + 1;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            settingsMenu.SetActive(true);
            isMenu = true;
        }

        if (!isMenu)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void setMenu()
    {
        isMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = 800 * PlayerPrefs.GetFloat("sensitivity", 0.5f) + 1;
    }
}
