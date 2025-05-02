using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text SwordCounter;
    public Text AmuletCounter;
    public Animator MarbleAnimator;
    public GameObject Shatter;

    private int Sword = 0;
    private int Amulet = 0;
    private int RequiredItems = 4;

    private Animator animator;
    private bool IsAttackzone = false;
    private Rigidbody2D rb;

    AudioSource audioSource;
    public AudioClip amuletSound;
    public AudioClip swordSound;
    public AudioClip swingSound;

    public L2Time L2time;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        LoadInventory();
    }

    // Æ¯Á¤ ½ÃÁ¡¿¡¼­ È£Ãâ
    public void PlayBAnimationTrigger()
    {
        MarbleAnimator.SetTrigger("Marble");
        StartCoroutine(WaitForWin());
    }

    private IEnumerator WaitForWin() //¿©¿ì±¸½½ ¹Ú»ì~À©¾À ³Ñ¾î°¡±â
    {
        while (!MarbleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Marble"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Narration3");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (Sword + Amulet) >= RequiredItems && IsAttackzone)
        {
            UseAttack();
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collect(other.GetComponent<Collectible>());
        }

        if (other.CompareTag("Attackzone"))
        {
            IsAttackzone = true;
            Shatter.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Æ¯Á¤ ¿ÀºêÁ§Æ®¿¡¼­ ³ª°¬À» ¶§
        if (other.CompareTag("Attackzone"))
        {
            IsAttackzone = false;  // Æ¯Á¤ ±¸¿ª¿¡¼­ ³ª°¨
            Shatter.SetActive(false);
        }
    }

    private void Collect(Collectible collectible)
    {
        if (collectible.Collected()) // ✅ Will return false if already collected!
        {
            // ✅ Only update if actually collected
            if (collectible is AmuletCollectible)
            {
                Amulet++;
                audioSource.PlayOneShot(amuletSound);
            }
            else if (collectible is SwordCollectible)
            {
                Sword++;
                audioSource.PlayOneShot(swordSound);
            }
            UpdateGUI();
            SaveInventory();
        }
    }


    private void UseAttack()
    {
        L2time.L2TimePause();

        animator.SetBool("IsAttacking", true);
        audioSource.PlayOneShot(swingSound);
    }

    private void UpdateGUI()
    {
        SwordCounter.text = Sword.ToString();
        AmuletCounter.text = Amulet.ToString();
    }

    public void SaveInventory()
    {
        PlayerPrefs.SetInt("SwordCount", Sword);
        PlayerPrefs.SetInt("AmuletCount", Amulet);
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey("SwordCount"))
        {
            Sword = PlayerPrefs.GetInt("SwordCount");
        }

        if (PlayerPrefs.HasKey("AmuletCount"))
        {
            Amulet = PlayerPrefs.GetInt("AmuletCount");
        }

        UpdateGUI();
    }
}