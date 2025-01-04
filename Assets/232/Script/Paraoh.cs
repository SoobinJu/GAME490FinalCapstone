using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paraoh : MonoBehaviour
{

    GameObject target;
    public GameObject Projectile;
    float timer = 5;

    public AudioClip paraohsound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        var offset = 170f;
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            Debug.Log(timer);
            if(timer >= 5)
            {
                audioSource.PlayOneShot(paraohsound);
                Shoot();
                Debug.Log("shoot");
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        Vector3 eulerangle = gameObject.transform.eulerAngles;
        eulerangle += new Vector3(0, 0, 190);
        Instantiate(Projectile, gameObject.transform.position, Quaternion.Euler(eulerangle));
    }

}
