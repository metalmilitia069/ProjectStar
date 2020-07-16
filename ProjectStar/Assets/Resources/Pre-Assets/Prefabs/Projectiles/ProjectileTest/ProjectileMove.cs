using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;



    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 15);
        var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
        muzzlePrefab.transform.forward = gameObject.transform.forward;
        var muzzleChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
        Destroy(muzzleVFX.gameObject, muzzleChild.main.duration);

        //Physics.IgnoreCollision(this.GetComponent<Collider>(), )
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            
        }
        else
        {
            Debug.Log("Speed = 0");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        speed = 0;
        ContactPoint contactPoint = collision.contacts[0];

        if (collision.gameObject.GetComponent<ProjectileMove>())
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);//(this.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }


        if (!collision.gameObject.GetComponent<ProjectileMove>())
        {
            speed = 0;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);

            Vector3 pos = contactPoint.point;

            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var hitChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitVFX.gameObject, hitChild.main.duration);

            Destroy(gameObject);
            //return;
        }

        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);

        //Vector3 pos = contactPoint.point;

        //var hitVFX = Instantiate(hitPrefab, pos, rot);
        //var hitChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
        //Destroy(hitVFX.gameObject, hitChild.main.duration);

        //Destroy(gameObject);
    }

    
}
