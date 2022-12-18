using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;
    public float force = 50f;
    public int maxHealth = 1000;
    public int currHealth;
    public int speed;
    public int attack;
    
    public int gameMaxHealth;
    public int gameMaxSpeed;
    public int gameMaxAttack;

    public int kills = 0;
    public int xps = 0;


    [SerializeField] private Vector3 movement;
    private Vector3 rotationSpeed = new Vector3(0, 40, 0);
    public Rigidbody rb;

    private float walk = 1.0f;
    public HealthBar healthBar;
    public GameObject SpawnPoint;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI xpText;
    
    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private WeaponHandler _weaponHandler;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SpawnPoint.transform.position;
        cam = Camera.main;
        movement = Vector3.zero;
        healthBar.SetMaxHealth(maxHealth);
        currHealth = maxHealth;
        
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }

        if (TryGetComponent(out WeaponHandler weaponHandler))
        {
            _weaponHandler = weaponHandler;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        movement = transform.right * Horizontal + transform.forward * Vertical;
        movement *= force;
        movement.y = rb.velocity.y;

        if (Horizontal == 0 && Vertical == 0)
        {
            _animator.SetFloat("Speed", 0);
        }
        else
        {
            _animator.SetFloat("Speed", 0.5f);
        }

        rb.velocity = movement;
    }
    
    public void Die()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath?.Invoke();
        }
    }
    
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weaponHandler.Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        
        if (currHealth <= 0) {
            Die();
        }
    }

    public void AddKill(int nKills)
    {
        kills += nKills;
        killText.SetText(kills.ToString());
        ChangeXPs(nKills * 10 );
        
    }

    public void ChangeXPs(int xp) {
        xps += xp;
        xpText.text = xps.ToString();
    }
}