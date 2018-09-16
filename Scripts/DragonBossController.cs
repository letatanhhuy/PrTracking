using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossController : EnemyController
{
    [Header("Variables From DragonBossController Class")]
    public int bossHealth;
    public GameObject explosionEffect;
    public GameObject movingPoints;
    public GameObject bullet;
    public GameObject bullet_gen_pos;
    public float shootingFrequence;


    //private
    private Animator animator;
    private Transform[] movingPointsList;
    private int mTargetPoint;
    private float shootingStartTime = 0;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        movingPointsList = new Transform[movingPoints.transform.childCount];
        for (int i = 0; i < movingPoints.transform.childCount; i++)
        {
            movingPointsList[i] = movingPoints.transform.GetChild(i);
        }
        mTargetPoint = Random.Range(0, movingPoints.transform.childCount);
        shootingStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        updateAttack();
        limitPlayerInScreen();
    }

    new void Move()
    {
        base.Move();
        float step = 1 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movingPointsList[mTargetPoint].position, step);
        if (comparePos(transform.position, movingPointsList[mTargetPoint].position, 0.05f))
        {
            mTargetPoint = Random.Range(0, movingPoints.transform.childCount);
        }
    }

    void updatePlayerMovement()
    {
        float step = 1 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movingPointsList[mTargetPoint].position, step);
    }

    bool isSamePosition(Vector3 posL, Vector3 posR)
    {
        return Mathf.Approximately(posL.x, posR.x) && Mathf.Approximately(posL.y, posR.y) && Mathf.Approximately(posL.z, posR.z);
    }

    bool comparePos(Vector3 posL, Vector3 posR, float distance)
    {
        bool retVal = false;
        if ((posL - posR).sqrMagnitude <= distance)
        {
            retVal = true;
        }
        return retVal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("isHit");

        GameObject hitExplosion = Instantiate(explosionEffect, other.transform.position, other.transform.rotation);
        hitExplosion.transform.parent = null;
    }

    void recover()
    {
        animator.ResetTrigger("isHit");
    }

    void updateAttack()
    {
        attack_normal();
        Invoke("attack_normal", 2);
    }

    void attack_normal()
    {
        // If the next update is reached
        if (Time.time >= shootingStartTime)
        {
            float randomShootingFrequence = Random.Range(shootingFrequence, shootingFrequence * 4);
            shootingStartTime = Time.time + randomShootingFrequence;
            shootBullet();
        }
    }

    void shootBullet() 
    {
        Instantiate(bullet, bullet_gen_pos.transform.position, bullet_gen_pos.transform.rotation);
    }

    void limitPlayerInScreen()
    {
        Vector3 curPosCam = Camera.main.WorldToViewportPoint(transform.position);
        curPosCam.x = Mathf.Clamp(curPosCam.x, 0.1f, 0.9f);
        curPosCam.y = Mathf.Clamp(curPosCam.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(curPosCam);
    }
}
