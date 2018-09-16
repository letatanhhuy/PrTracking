using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{

    public float speed = 1.0f;

    //private
    private const string TAG = "Bullet_Controller";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateBulletMove();
    }

    void updateBulletMove()
    {
        Vector3 pos = transform.position;
        pos.y += speed * Time.deltaTime;
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Log.LOGD(TAG, "OnTriggerEnter2D");
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Log.LOGD(TAG, "OnBecameInvisible");
        Destroy(this.gameObject);
    }
}
