using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    private const string TAG = "BackgroundController";

    public float scrollSpeed;
    public float backgroungSize;
    private Transform[] bgTransformList;
    private Transform topBG, bottomBG, middleBG;
    private Vector3 startPosition;
    private float startPositionY;

    void Start()
    {
        startPosition = transform.position;
        startPositionY = transform.position.y;
        bgTransformList = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            bgTransformList[i] = transform.GetChild(i);
        }
        topBG = bgTransformList[0];
        middleBG = bgTransformList[1];
        bottomBG = bgTransformList[2];
    }

    void Update()
    {
        float newPosition = Time.time * scrollSpeed;
        transform.position = startPosition + Vector3.up * newPosition;
        if ((startPositionY - transform.position.y) >= backgroungSize * 2)
        {
            bottomBG.transform.position = topBG.transform.position + Vector3.up * backgroungSize * 2;
            cycleBackgroundOrder();
            startPositionY = transform.position.y;
        }

    }

    private void cycleBackgroundOrder()
    {
        Transform temp = topBG;
        topBG = bottomBG;
        bottomBG = middleBG;
        middleBG = temp;
    }
}
