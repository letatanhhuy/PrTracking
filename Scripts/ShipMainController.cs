using UnityEngine;

public class ShipMainController : MonoBehaviour {

    public float speed = 1.0f;
    public GameObject generateBulletPoint;
    public GameObject bulletType;

    //private
    private const string TAG = "ShipMainController";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        updatePlayerMovement();
        limitPlayerInScreen();
	}

    void shoot() {
        GameObject newBullet = Instantiate(bulletType, generateBulletPoint.transform.position, generateBulletPoint.transform.rotation);
        newBullet.transform.parent = null;
    }

    void updatePlayerMovement() {
        //moving
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }
        transform.position = pos;

        //shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Log.LOGD(TAG, "updateCharacterMove: shot");
            shoot();
        }
    }
    void limitPlayerInScreen() {
        Vector3 curPosCam = Camera.main.WorldToViewportPoint(transform.position);
        curPosCam.x = Mathf.Clamp(curPosCam.x, 0.1f, 0.9f);
        curPosCam.y = Mathf.Clamp(curPosCam.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(curPosCam);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Log.LOGD(TAG, "OnTriggerEnter2D");
    }
}
