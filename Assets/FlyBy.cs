using UnityEngine;

public class FlyBy : MonoBehaviour
{
    public float speed = 25f;
    public float resetPosition = -20f;
    public float startPosition = 300f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < resetPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition);
        }
    }
}
