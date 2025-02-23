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

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Ư�� �������� ȣ��
    public void PlayBAnimationTrigger()
    {
        MarbleAnimator.SetTrigger("Marble");
        StartCoroutine(WaitForWin());
    }

    private IEnumerator WaitForWin() //���챸�� �ڻ�~���� �Ѿ��
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

        SceneManager.LoadScene("WinScene");
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
        if(other.CompareTag("Collectible"))
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
        // Ư�� ������Ʈ���� ������ ��
        if (other.CompareTag("Attackzone"))
        {
            IsAttackzone = false;  // Ư�� �������� ����
            Shatter.SetActive(false);
        }
    }

        private void Collect(Collectible collectible)
    {
        if(collectible.Collected())
        {
            if(collectible is AmuletCollectible)
            {
                Amulet++;
            }
            else if (collectible is SwordCollectible)
            {
                Sword++;
            }
            UpdateGUI();
        }
    }

    private void UseAttack()
    {
        animator.SetBool("IsAttacking", true);
    }

    private void UpdateGUI()
    {
        SwordCounter.text = Sword.ToString();
        AmuletCounter.text = Amulet.ToString(); 
    }
}


