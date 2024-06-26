
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")] 
    [SerializeField] private float startingHP;
    public float currentHP { get; private set;}
    private Animator anim;
    private bool dead;
    private bool invulnerable;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int flashCount;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHP = startingHP;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHP = Mathf.Clamp(currentHP - _damage, 0, startingHP);

        if (currentHP > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());

        }
        else
        {
            if (!dead)
            { 
                /*//player
               
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;
                dead = true;

                //enemy
                if(GetComponent<EnemyPatrol>() != null)
                GetComponent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;*/


                foreach (Behaviour component in components) 
                component.enabled=false;

                anim.SetBool("grounded",true);
                anim.SetTrigger("die");
                dead = true;
                
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            takeDamage(1);

    }

    public void AddHealth(float _value)
    {
        currentHP = Mathf.Clamp(currentHP + _value,0,startingHP);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.05f);
            yield return new WaitForSeconds(iFrameDuration / (flashCount * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (flashCount * 2));

        } 
        Physics2D.IgnoreLayerCollision(9, 10, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }







}
