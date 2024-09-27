using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class @players : MonoBehaviour
{
    public int index =0;
    [SerializeField] List<GameObject>Players = new List<GameObject>();
    PlayerInputManager maneger;
    // Start is called before the first frame update
    void Start()
    {
        maneger = GetComponent<PlayerInputManager>();
        index = Random.Range(0, Players.Count);
        maneger.playerPrefab = Players[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
