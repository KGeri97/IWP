using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{

   public GameObject Panel;

    
  // This is a MonoBehaviour-provided function that Unity will run one time
  // automatically when the cursor is over this GameObject
  // (GameObject needs a collider component)
 
  void OnMouseOver()
  {
      // in here you write code to respond to this event
 
      // Use GetComponent to find the SpriteRenderer component on this GameObject
      // then set its color to be green
      GetComponent<SpriteRenderer>().color = Color.green;

         if (Input.GetMouseButtonDown(0))
        {

            if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        }
  }
 
  // This is a MonoBehaviour-provided function that Unity will run one time
  // automatically when the cursor is no longer over this GameObject
  // (GameObject needs a collider component)
 
   void OnMouseExit()
   {
      // in here you write code to respond to this event
 
      // Use GetComponent to find the SpriteRenderer component on this GameObject
      // reset it to normal color
      GetComponent<SpriteRenderer>().color = Color.white;
   }
}