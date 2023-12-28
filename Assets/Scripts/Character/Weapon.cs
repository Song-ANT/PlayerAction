using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> colliders = new List<Collider>();

    private void OnEnable()
    {
        colliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (colliders.Contains(other)) return;

        colliders.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }


    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }

}