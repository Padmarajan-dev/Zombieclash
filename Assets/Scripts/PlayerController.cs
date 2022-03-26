using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player props 
    [SerializeField] float speed = 2f;
    CharacterController cController;
   //for player move direction
    Vector3 MoveDirection = new Vector3(0,0,0);
    
   //player rotation props
    float mouseX = 0.0f;
    float mouseY = 0.0f;
    float xRotation = 0.0f;
    [SerializeField] float mouseSensit = 2f;

    [SerializeField] TextMeshProUGUI health;

    [SerializeField] GameObject GameOverPanel;

    [SerializeField] GameObject GameoverText_1;

    [SerializeField] GameObject GameoverText_2;

    //Camera 
    [SerializeField] Camera cam;
    // Start is called before the first frame update
    //Player Health
    int PlayerHealth = 20;
    void Start()
    {
        cController = GetComponent<CharacterController>();
       if(health != null) 
       {
        health.text = PlayerHealth.ToString();
       }
    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection = GetInputAxis();
        MoveDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y,Vector3.up) * MoveDirection;
        MovePlayer();
        RotatePlayer();
    }

    //for Move Player 
    void MovePlayer() 
    {
        MoveDirection.Normalize();
        if(MoveDirection.magnitude !=0)
        {
          MoveDirection  = GetInputAxis();
          MoveDirection  = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y,Vector3.up) * MoveDirection;  
          cController.Move(MoveDirection * speed * Time.deltaTime);
        }
    }

    // for rotate player along with camera
    void RotatePlayer()
    {
            mouseX = Input.GetAxis("Mouse X") * mouseSensit * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensit * Time.deltaTime;
            
            xRotation = mouseY;
            xRotation = Mathf.Clamp(xRotation,-60f,60f);
            cam.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            transform.Rotate(Vector3.up *  mouseX);
    }

    //for Get Input values 
    Vector3 GetInputAxis()
    {
      return new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
      
    }

    //for detect attack
      private void OnTriggerEnter(Collider other) {
        if(other.gameObject.transform.tag == "Enemy")
        {
          if(PlayerHealth >=0)
          {          
             PlayerHealth -= 2;
          }
          if(PlayerHealth < 0)
          {
            PlayerHealth = 0;
          }
          if(PlayerHealth == 0)
          {
            GameoverText_2.SetActive(true);
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
          }
            if(health != null) 
          {
              health.text = PlayerHealth.ToString();
          }
        }
        if(other.gameObject.transform.tag == "finish")
        {
           GameoverText_1.SetActive(true);
           GameOverPanel.SetActive(true);
           Time.timeScale = 0;
        }
    }
    //reload scene
    public void reloadScene()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    //for quit 
     public void Exit() 
    {
        Application.Quit();
    }

    
}
