using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fixation : MonoBehaviour
{
    public float delay;
    // Sync with reference in EgoMotion.cs
    public bool Exp1;

    int sceneIndex = 0;
    // Scene Index = 0:Control, 1:Full, 2:Local, 3:Global, 2:Same, 3:Opposite
    
    public GameObject overlord;
    // Component volatileControl;

    // private Overlord volatileHunter;

    // Start is called before the first frame update
    void Start()
    {
        //Call Overlord Function to Write CSV
        //overlord = GameObject.Find("ExperimentController");
        // volatileHunter = overlord.GetComponent<Overlord>();

        // volatileHunter.WriteData();
        
        sceneIndex = Random.Range(0,4);
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevelAfterDelay(float delay)
     {
         yield return new WaitForSeconds(delay);

         // Random Scene Selector
         if(Exp1 == true)
         {
             if(sceneIndex == 0)
             {
                 SceneManager.LoadScene("ControlCondition");
             }
             if(sceneIndex == 1)
             {
                 SceneManager.LoadScene("FullFlow1");
             }
             if(sceneIndex == 2)
             {
                 SceneManager.LoadScene("LocalFlow");
             }
             if(sceneIndex == 3)
             {
                 SceneManager.LoadScene("GlobalFlow");
             }
         }

         if(Exp1 == false)
         {
             if(sceneIndex == 0)
             {
                 SceneManager.LoadScene("ControlCondition");
             }
             if(sceneIndex == 1)
             {
                 SceneManager.LoadScene("FullFlow2");
             }
             if(sceneIndex == 2)
             {
                 SceneManager.LoadScene("SameFlow");
             }
             if(sceneIndex == 3)
             {
                 SceneManager.LoadScene("OppositeFlow");
             }
         }
         
     }
}
