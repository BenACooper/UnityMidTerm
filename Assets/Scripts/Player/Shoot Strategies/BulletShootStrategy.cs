using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : MonoBehaviour, IShootStrategy
{
    ShootInteractor interactor;
    Transform shootPoint;

    public BulletShootStrategy(ShootInteractor _interactor)
    {
        Debug.Log("Switched to bullet mode.");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        //Change the color of the gun!
        interactor.gunRenderer.material.color = interactor.bulletColor;
    }

    public void Shoot()
    {
        PooledObject pooledOjb = interactor.bulletPool.GetPooledObject();
        pooledOjb.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledOjb.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * interactor.GetShootVelocity();
        //Destroy(bullet.gameObject, 5.0f);
        interactor.bulletPool.DestroyPooledObject(pooledOjb, 5.0f);
    }


}
