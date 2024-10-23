using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    /*[SerializeField] private Input inputType;*/

    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletColor;
    public Color rocketColor;

    [Header("Shoot")]
    //[SerializeField] private Rigidbody bulletPrefab;
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;

    [SerializeField] private float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMoveBehaviour moveBehaviour;

    private float finalShootVelocity;
    private IShootStrategy currentStrategy;

    public override void Interact()
    {
        if (currentStrategy == null)
        {
            currentStrategy = new BulletShootStrategy(this);
        }

        if (input.weapon1Pressed)
        {
            currentStrategy = new BulletShootStrategy(this);
        }
        
        if (input.weapon2Pressed)
        {
            currentStrategy = new RocketShootStrategy(this);
        }
        //Shoot strategy.
        if (input.primaryShootPressed && currentStrategy != null)
        {
            currentStrategy.Shoot();
        }
    }

    /*void Shoot()
    {
        PooledObject pooledOjb = objPool.GetPooledObject();
        pooledOjb.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledOjb.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * finalShootVelocity;
        //Destroy(bullet.gameObject, 5.0f);
        objPool.DestroyPooledObject(pooledOjb, 5.0f);
    }*/

    public Transform GetShootPoint()
    {
        return shootPoint;
    }

    public float GetShootVelocity()
    {
        finalShootVelocity = moveBehaviour.GetForwardSpeed() + shootVelocity;
        return finalShootVelocity;

    }
}
