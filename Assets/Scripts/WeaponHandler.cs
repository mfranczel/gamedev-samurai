using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject weaponHolder;
    public GameObject weapon;

    [SerializeField] private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Instantiate(weapon, weaponHolder.transform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localScale = new Vector3(100, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}