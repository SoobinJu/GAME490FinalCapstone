using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SpriteRenderer playerSR;
    HealthUI healthui;
    public int Health = 3;
    Rigidbody2D myRb;
    public int knockback;
    private bool isInvincible = false;
    int keycount;
    int score = 0;
    private Rigidbody2D rb;

    public AudioClip Lootsound;
    public AudioClip zombieSound;
    AudioSource audiosource;

    public GameObject gameObject;


    public float invincibilityDurationSeconds;
    public float invincibilityDeltaTime;


    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        playerSR = gameObject.GetComponent<SpriteRenderer>();
        healthui = GameObject.FindObjectOfType<HealthUI>();
        audiosource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == true)
        {
            Vector2 direction = collision.transform.position - transform.position;
            Destroy(collision);
            LoseHealth(1, direction);
        }
        if (collision.gameObject.CompareTag("Key")== true)
        {
            audiosource.PlayOneShot(Lootsound);
            keycount++;
            print(keycount);
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Door1") == true && keycount == 1)
        {
            SceneManager.LoadScene("Game2");
            gameObject.transform.position = new Vector3(8, 12, 0);
        }
        if (collision.gameObject.CompareTag("Door2") == true)//&& 용의자선택)
        {
            SceneManager.LoadScene("Narration2");
            //GameManager.Instance.SpawnPlayer(new Vector3(8, 12, 0), 2);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Door3") == true)
        {
            SceneManager.LoadScene("WinScene");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Treasure") == true)
        {
            audiosource.PlayOneShot(Lootsound);
            score++;
            Destroy(collision.gameObject);
            healthui.updatecore(score);
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy") == true)
    //   {
    //       audiosource.PlayOneShot(zombieSound,0.5f);
    //       Vector2 direction = other.transform.position - transform.position;
    //       LoseHealth(1,direction);
    //myRb.position = Vector2.MoveTowards(myRb.position, ((Vector2)other.transform.position - myRb.position), Time.deltaTime * knockback);
    //   }
    //}

    private void ChangeColorTo(Color color)
    {
        playerSR.color = color;
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (playerSR.color == Color.white)
            {
                ChangeColorTo(Color.black);
            }
            else
            {
                ChangeColorTo(Color.white);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        Debug.Log("Player is no longer invincible!");
        ChangeColorTo(Color.white);
        isInvincible = false;
    }

    void MethodThatTriggersInvulnerability()
    {
        if (!isInvincible)
        {
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    public void LoseHealth(int amount, Vector2 direction)
    {
        if (isInvincible) return;
        healthui.updatehealthUI(Health);
        myRb.AddForce(-direction.normalized * knockback, ForceMode2D.Impulse);
        Health -= amount;

        // The player died
        if (Health <= 0)
        {
            Health = 0;
            SceneManager.LoadScene("LoseScene");

            // Broadcast some sort of death event here before returning
            return;
        }

        StartCoroutine(BecomeTemporarilyInvincible());
    }


}
