using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private float distance;

    void Start()
    {

    }
    void Update()
    {
        WaveChasing();
    }

    void WaveChasing()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            //old//
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward);

            //New//
            //restricts the enemy's movement to the x-axis only
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
