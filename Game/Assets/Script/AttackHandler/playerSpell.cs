using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class playerSpell : MonoBehaviour
{
    public GameObject spell;
    public float attack = 2f;
    Gethit hit;
    void Start()
    {
        StartCoroutine(WaitTime());
        Collider[] colliders = GetObjectsInOverlapBox(spell.transform.position, spell.transform.localScale);

        // Kiểm tra từng đối tượng
        foreach (Collider collider in colliders)
        {
            if(collider.transform.tag=="Enemy" || collider.transform.tag == "EnemyShoot")
            singleDmg(collider.transform);
        }
        Destroy(gameObject, 1f);

    }
 
    private Collider[] GetObjectsInOverlapBox(Vector3 center, Vector3 size)
    {
        // Lấy danh sách các đối tượng trong vùng va chạm
        Collider[] colliders = Physics.OverlapBox(center, size,spell.transform.rotation);
        return colliders;
    }
    void singleDmg(Transform targeted)
    {

        Gethit enemy = targeted.GetComponent<Gethit>();
        if (enemy != null)
            enemy.Take(DmgCalcaculte(attack));
    }
    float DmgCalcaculte(float atk)
    {
        return (attack * gameManager.PlayerAtk()); 
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.4f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(spell.transform.position,spell.transform.localScale* 1.02f);
    }
}
