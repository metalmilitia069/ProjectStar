using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject _effectToSpawn;

    public int speed = 5;
    public float timeToFire = 0;

    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        _effectToSpawn = vfx[0];

        rotation = firePoint.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation = this.transform.localRotation;
        //rotation = firePoint.transform.forward;//.localRotation;
        if (Input.GetMouseButton(0) && Time.time > timeToFire)
        {
            timeToFire = Time.time + (1 / _effectToSpawn.GetComponent<ProjectileMove>().fireRate);
            SpawnVfx();
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 1f, 0f), speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f, 1f, 0f), -speed * Time.deltaTime);
        }
    }

    public void SpawnVfx()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(_effectToSpawn, firePoint.transform.position, firePoint.transform.rotation);//rotation);
        }
        else
        {
            Debug.Log("No Fire Point Found!");
        }
    }
}
