using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{

    public float minZ, maxZ;
    public GameObject[] enemy;
    public int numberOfEnemys;
    public float spawnTime;
    private int currentEnemies;
    [SerializeField] private bool ultima;
    public bool Fim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemies>=numberOfEnemys) 
        {
            int enemies = FindObjectsOfType<Enemy>().Length;
            if(enemies <=0)
            {
                FindObjectOfType<CameraFollow>().maxXAndY.x =200;

                if(ultima == true)
                {
                    Fim = true;
                    SceneManager.LoadScene("Menu");
                }
                gameObject.SetActive(false);
            }
        }
    }
    void spawnEnemy() 
    {
        bool positionX = Random.Range(0,2)==0?true:false;
        Vector3 spawnPositio;
        spawnPositio.z =Random.Range(minZ,maxZ);
        if(positionX)
        {
            spawnPositio = new Vector3(transform.position.x+10,0,spawnPositio.z);

        }
        else 
        {
            spawnPositio = new Vector3(transform.position.x-10,0,spawnPositio.z);
        }
        Instantiate(enemy[Random.Range(0,enemy.Length)],spawnPositio,Quaternion.identity);
        currentEnemies++;
        if(currentEnemies <= numberOfEnemys)
        {
            Invoke("spawnEnemy", spawnTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<CameraFollow>().maxXAndY.x = transform.position.x;
            spawnEnemy();
        }
    }
}
