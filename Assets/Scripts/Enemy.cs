using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    //Enemy Props
    [SerializeField] float AttackRange = 10.1f;
    float rotSpeed = 12f;
    float speed = 10f;
    float stopRange = 2f;

    //follow player 
    public bool followPlayer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }
    //Check for Attack Player
    void AttackPlayer()
    {
        followPlayer = FindRange().magnitude < AttackRange && FindRange().magnitude > stopRange ? true : false;
        if(followPlayer)
        {
            FollowPlayer();
        }
    }
    //find range between Player and enemy
    Vector3 FindRange()
    {
        Vector3 range = Player.transform.position - transform.position;
        return range;
    }


    //for Follow Player
    void FollowPlayer()
    {
     transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Player.transform.position - transform.position),rotSpeed * Time.deltaTime);
     transform.position += transform.forward * speed * Time.deltaTime;
    }

  
}
