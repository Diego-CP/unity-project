using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover 
{
    private bool isAlive = true;
    public int faith;
    public int maxFaith;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetBool("Run", horizontalInput != 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Swing");
    }
    
    // Override the function to recieve damage from Fighter to include OnHitpointChange
    protected override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    // Function to increase hitpoints by a given amount
    public void Heal(int healingAmount)
    {
        // If HP is at max, dont do anything
        if(hitpoint == maxHitpoint) 
        {
            return;
        }
        
        hitpoint += healingAmount;
        
        if(hitpoint > maxHitpoint) 
        {
            hitpoint = maxHitpoint;
        }

        GameManager.instance.OnHitpointChange();
    }

    // Function to reduce faith by a given amount
    public void UseFaith(int faithUsed)
    {
        if (faithUsed < faith) 
        {
            faith -= faithUsed;
        }

        GameManager.instance.OnFaithChange();
    }

    public void GainFaith(int faithAmount)
    {
        // If HP is at max, dont do anything
        if(faith == maxFaith) 
        {
            return;
        }

        faith += faithAmount;
        
        if(faith > maxFaith) 
        {
            faith = maxFaith;
        }

        GameManager.instance.OnFaithChange();
    }

    // Function to load the Death scene once the Player dies
    protected override void Death() 
    {
        isAlive = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
    }

    // Update movement ecery frame according to input
    private void FixedUpdate() 
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // If the Player is not alive, they are not able to move
        if (isAlive) 
        {
            UpdateMotor(new Vector3(x, y, 0));
        } 
    }
}
