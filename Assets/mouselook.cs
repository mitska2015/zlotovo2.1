using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public float shakeAmount = 0.02f;
    public float shakeSpeed = 3f;
    private float timer = 0F;
    private Vector3 originalPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        ApplyShake();
    }
    void ApplyShake()
    {
        timer += Time.deltaTime * shakeSpeed;
        float waveX = Mathf.Sin(timer) * shakeAmount;
        float waveY = Mathf.Sin(timer * 0.7f) * shakeAmount;
        transform.localPosition= originalPosition+new Vector3(waveX, waveY, 0f);
    }
}
