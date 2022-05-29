using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {
    private bool isAlive = true;
    public int faith;
    public int maxFaith;
    private Animator anim;
    public GameObject projectile;
    protected Quaternion direction;
    public float cooldown = 0.5f;
    private float lastUse;
    private float angle;
    private float currentX;
    private float currentY;
    protected float spellRange = 0.5f;
    public int secondSpellDamage = 1;
    public float secondSpellPushForce = 5.0f;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    virtual public void Update(){
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetBool("Run", horizontalInput != 0);

        if(Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.Alpha8)){
            UseFirstSpell();
        }

        if(Input.GetKeyDown(KeyCode.Alpha9)) {
            UseSecondSpell();
        }
    }

    void Attack() {
        anim.SetTrigger("Swing");
    }

    void UseFirstSpell() {
        // If more time has passed than the cooldown, the player can use the spell
        if (Time.time - lastUse > cooldown) {
            // Reset lastUse as current time
            lastUse = Time.time;

            // Instantiate a projectile accordingly to what direction the player is moving
            float angle = Mathf.Atan2(currentY, currentX) * Mathf.Rad2Deg - 90;
            Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        }
    }

    void UseSecondSpell() {
        // If more time has passed than the cooldown, the player can use the spell
        if (Time.time - lastUse > cooldown) {
            // Reset lastUse as current time
            lastUse = Time.time;

            // List of colliders that oerlap with the spell's circular hitbox
            Collider2D[] collidedBoxes = Physics2D.OverlapCircleAll(transform.position, spellRange);
            // When nothing is hit, continue to the next frame
            for(int i = 0; i < collidedBoxes.Length; i++) {
                // When nothing is hit, continue
                if (collidedBoxes[i] == null) 
                    continue;
                
                // Check if what is being collided with is a Fighter and is not the player
                if (collidedBoxes[i].tag == "Fighter") {
                    if (collidedBoxes[i].name != "Player") {
                        // Create a Damage object
                        Damage dmg = new Damage{
                            damageAmount = secondSpellDamage,
                            origin = transform.position,
                            pushForce = secondSpellPushForce
                        };

                        collidedBoxes[i].SendMessage("RecieveDamage", dmg);
                    }
                }
            }
        }
    }

    // Function to draw the AOE of the second spell
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spellRange);
    }

    // Override the function to recieve damage from Fighter to include OnHitpointChange
    protected override void RecieveDamage(Damage dmg) {
        base.RecieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    // Function to increase hitpoints by a given amount
    public void Heal(int healingAmount) {
        // If HP is at max, dont do anything
        if(hitpoint == maxHitpoint) {
            return;
        }
        hitpoint += healingAmount;
        if(hitpoint > maxHitpoint) {
            hitpoint = maxHitpoint;
        }
        GameManager.instance.OnHitpointChange();
    }

    // Function to reduce faith by a given amount
    public void UseFaith(int faithUsed) {
        if (faithUsed < faith) {
            faith -= faithUsed;
        }
        GameManager.instance.OnFaithChange();
    }

    public void GainFaith(int faithAmount) {
        // If faith is at max, dont do anything
        if(faith == maxFaith) {
            return;
        }
        faith += faithAmount;
        if(faith > maxFaith) {
            faith = maxFaith;
        }
        GameManager.instance.OnFaithChange();
    }

    // Function to load the Death scene once the Player dies
    protected override void Death() {
        isAlive = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
    }

    // Update movement every frame according to input
    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Guardar los valores de x y y si se mueve el jugador
        if (x != 0 || y != 0) {
            currentX = x;
            currentY = y;
        }

        // If the Player is not alive, they are not able to move
        if (isAlive) {
            UpdateMotor(new Vector3(x, y, 0));
        } 
    }
}
