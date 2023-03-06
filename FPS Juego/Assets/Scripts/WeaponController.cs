using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public Transform weaponMuzzle;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletShot;
    

    [Header("Shoot Parameters")]
    public float fireRange = 200;
    public float retract = 4f;
    public float fireRate = 0.6f;
    public int maxBullets = 10;
    private int currentBullets;

    [Header("Multimedia")]
    public GameObject flash;



    private Transform cameraPlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        currentBullets = maxBullets;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBullets <= 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }
        HandleShoot();

        //Volver la pistola a la posicion inicial
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
       

    }

    private void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject flashClone = Instantiate(flash, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
            Destroy(flashClone, 1f);

            AddRetract();

            RaycastHit hit;
            if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers))
            {

                GameObject bulletShotClone = Instantiate(bulletShot, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletShotClone, 4f);
            }
            currentBullets--;
            
        }
    }

    private void AddRetract()
    {
        //transform.Rotate(-retract, 0f, 0f);
        transform.position = transform.position - transform.forward * (retract / 50f);
        
    }

   
}
