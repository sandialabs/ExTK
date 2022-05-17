using Microsoft.MixedReality.Toolkit.Audio;
using TMPro;
using UnityEngine;

public class AudioFeedBackCustom : MonoBehaviour
{
    private TextToSpeech textToSpeechCustom;
    public TextMeshPro followText;
    private bool follow = false;
    private bool explodeContract = false;
    private MenuController menuController;

    void Awake()
    {
        textToSpeechCustom = GameObject.Find("AudioManager").GetComponent<TextToSpeech>();
        menuController = GameObject.Find("MenuFramework").GetComponent(typeof(MenuController)) as MenuController;
    }

    public void isGreetingUser(TextMeshProUGUI userName)
    {
        string userNameLocal = userName.text;
        string namePreBuild;
        if (userName != null && userName.text != "")
        {
            namePreBuild = "Welcome " + userName.text;
            var msg = string.Format(namePreBuild, textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
        }
        else
        {
            var msg = string.Format("Welcome", textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
        }
        menuController.loginGreeting(userNameLocal);
    }

    public void CustomSpeak(string feedback)
    {
            var msg = string.Format(feedback, textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
    }

    public void CustomSpeak(string feedback, bool buttonOnOff, bool isOnOff)
    {
        if (isOnOff)
        {
            if (buttonOnOff)
            {
                var msg = string.Format(feedback + " On", textToSpeechCustom.Voice.ToString());
                textToSpeechCustom.StartSpeaking(msg);
            }
            else
            {
                var msg = string.Format(feedback + " Off", textToSpeechCustom.Voice.ToString());
                textToSpeechCustom.StartSpeaking(msg);
            }
        }
        else
        {
            var msg = string.Format(feedback, textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
        }
    }

    public void FollowResponse()
    {
        follow = !follow;
        if (follow)
        {
            var msg = string.Format("Follow On", textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
            followText.text = "Say \"Follow Off\"";
        }
        else
        {
            var msg2 = string.Format("Follow Off", textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg2);
            followText.text = "Say \"Follow On\"";
        }
    }

    public void ExplodeContract()
    {
        explodeContract = !explodeContract;
        if (explodeContract)
        {
            var msg = string.Format("Exploding Model", textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg);
        }
        else
        {
            var msg2 = string.Format("Contracting Model", textToSpeechCustom.Voice.ToString());
            textToSpeechCustom.StartSpeaking(msg2);
        }
    }
}
