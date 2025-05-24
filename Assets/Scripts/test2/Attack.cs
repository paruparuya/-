using UnityEngine;

public class Attack : MonoBehaviour
{
    public float spped = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * spped * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.TakeDamage(2); // 2ƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }

            Destroy(gameObject); // ’e‚ğÁ‚·
        }
    }
}
