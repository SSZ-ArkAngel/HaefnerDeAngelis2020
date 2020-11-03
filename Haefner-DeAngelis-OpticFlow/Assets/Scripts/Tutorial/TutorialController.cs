using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    
    public Text tutorialText;
    public Text fixationText;
    int tutorialKey = 0;

    public Transform opticFlowVideo;
    public Transform probeSample;
    public Transform dialSample;
    public Transform probeSample2;

    Transform video;
    Transform probe;
    Transform dial;

    float speedX = 0.1f;
    float speedY = 0.15f;

    // private IEnumerator waitForEnter(KeyCode key)
    // {

    // }

    // Start is called before the first frame update
    void Start()
    {
        IntroductionTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.Escape))
        {
            //volatileHunter.ExportData();
            SceneManager.LoadScene("Overlord");
        }

        if(tutorialKey != 1)
        {
            fixationText.text = "";
        }

        if(tutorialKey != 2)
        {
            GameObject videoToDestroy = GameObject.Find("OpticFlowSampleVideo(Clone)");
            Destroy(videoToDestroy);
        }

        if(tutorialKey != 3 && tutorialKey != 6)
        {
            GameObject probeToDestroy = GameObject.Find("OpticProbeSample(Clone)");
            GameObject probeToDestroy2 = GameObject.Find("OpticProbeSample2(Clone)");
            Destroy(probeToDestroy);
            Destroy(probeToDestroy2);
        }

        if(tutorialKey != 4 && tutorialKey != 7)
        {
            GameObject dialToDestroy = GameObject.Find("PaddleSample(Clone)");
            Destroy(dialToDestroy);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            tutorialKey += 1;

            if(tutorialKey == 0)
            {
                IntroductionTutorial();
            }
            if(tutorialKey == 1)
            {
                FixationTutorial();
            }
            if(tutorialKey == 2)
            {
                OpticFlowTutorial();
            }
            if(tutorialKey == 3)
            {
                ProbeTutorial();
            }
            if(tutorialKey == 4)
            {
                DialTutorial();
            }
            if(tutorialKey == 5)
            {
                InputTutorial1();
            }
            if(tutorialKey == 6)
            {
                InputTutorial2();
            }
            if(tutorialKey == 7)
            {
                stimulusTutorial();
            }
            // if(tutorialKey == 8)
            // {
            //     InputTutorial3();
            // }
            if(tutorialKey == 8)
            {
                SceneManager.LoadScene("Overlord");
            }
        }
    }

    // All the tutorial methods are entailed over here

    void IntroductionTutorial() //0
    {
        tutorialText.text = "Hello! And Welcome to the Tutorial for this Experiment!\n\nPress Enter to proceed to the next page";
    }

    void FixationTutorial() //1
    {
        tutorialText.text = "This is a Fixation Cross. It will be presented before every stimulus. Please focus on the cross when it appears!\n\nPress Enter to proceed to the next page";
        fixationText.text = "+";
    }

    void OpticFlowTutorial() //2
    {
        tutorialText.text = "This is what an optic flow field looks like. During the experiment the entire screen will be covered by one.\n\nPress Enter to proceed to the next page";
        video = Instantiate(opticFlowVideo);
    }

    void ProbeTutorial() //3
    {
        tutorialText.text = "This is what a probe looks like. It will appear on the right side of the screen in front of the optic flow.\n\nPress Enter to proceed to the next page";
        probe = Instantiate(probeSample);
    }

    void DialTutorial() //4
    {
        tutorialText.text = "This is a dial. You can move it using the arrow keys. Holding shift will make it rotate faster.\n\nPress Enter to proceed to the next page";
        dial = Instantiate(dialSample);
    }

    void InputTutorial1() //5
    {
        tutorialText.text = "During the experiment a moving probe will be shown in front of an optic flow field. You will be asked to indicate the direction you saw it move in, using the dial.\n\nPress Enter to proceed to the next page";
    }

    void InputTutorial2() //6
    {
        tutorialText.text = "Note the motion of the probe!\n\nPress Enter to proceed to the next page";
        probe = Instantiate(probeSample2);
    }

    void stimulusTutorial() //7
    {
        tutorialText.text = "This is the dial again. You can move it using the arrow keys. Holding shift will make it rotate faster. Try to manipulate a it to make it match the motion of the probe!\n\nPress Enter to proceed to the next page.";
        dial = Instantiate(dialSample);
    }

    // void InputTutorial3() //8
    // {
    //     tutorialText.text = "This is the dial again. You can move it using the arrow keys. Holding shift will make it rotate faster. Adjust it to match the motion of the probe\n\nPress Enter to proceed to the next page";
        
    // }

    private IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(3.0f);
    }

}
