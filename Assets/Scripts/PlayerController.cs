using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    public float force = 50f;
    public int maxHealth;
    public int currHealth;

    public int kills;
    public int xps;


    [SerializeField] private Vector3 movement;
    private Vector3 rotationSpeed = new Vector3(0, 40, 0);
    public Rigidbody rb;

    private float walk = 1.0f;
    public HealthBar healthBar;
    public GameObject SpawnPoint;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI xpText;

    [SerializeField] private Animator _animator;

    [SerializeField] private WeaponHandler _weaponHandler;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SpawnPoint.transform.position;
        cam = Camera.main;
        motor = FindObjectOfType<PlayerMotor>();
        movement = Vector3.zero;
        healthBar.SetMaxHealth(maxHealth);

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

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weaponHandler.Attack();
        }
    }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    void AddKill(int nKills)
    {
        kills += nKills;
        killText.text = kills.ToString();
    }

    void AddXPs(int xp)
    {
        xps += xp;
        xpText.text = xps.ToString();
    }
}