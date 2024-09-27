using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damege;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Enemy enemy =  collision.GetComponent<Enemy>();
        PersonMovement player =collision.GetComponent<PersonMovement>();
        if (enemy != null) 
        {
            enemy.TookDamege(damege);


        }
        if(player != null)
        {
            player.TookDamage(damege);
        }
    }
}
