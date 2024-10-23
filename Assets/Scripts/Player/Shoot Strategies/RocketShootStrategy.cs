using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor interactor;
    Transform shootPoint;

    public RocketShootStrategy(ShootInteractor _interactor)
    {
        Debug.Log("Switched to rocket mode.");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        //Change the color of the gun!
        interactor.gunRenderer.material.color = interactor.rocketColor;
    }

    public void Shoot()
    {
        PooledObject pooledOjb = interactor.rocketPool.GetPooledObject();
        pooledOjb.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rocket = pooledOjb.GetComponent<Rigidbody>();
        rocket.transform.position = shootPoint.position;
        rocket.transform.rotation = shootPoint.rotation;

        rocket.velocity = shootPoint.forward * interactor.GetShootVelocity();
        //Destroy(bullet.gameObject, 5.0f);
        interactor.rocketPool.DestroyPooledObject(pooledOjb, 5.0f);
    }
}
