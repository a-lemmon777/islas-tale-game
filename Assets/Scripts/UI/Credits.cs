using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Credits : MonoBehaviour
{
    [Tooltip("Main menu button image")]
    public Image ButtonImage;
    [Tooltip("Main menu button text")]
    public TMP_Text ButtonText;

    void Start()
    {
        StartCoroutine(FadeIntoScene(1f));
    }

    private IEnumerator FadeIntoScene(float delay)
    {
        ButtonImage.color = new Color(1, 1, 1, 0);
        ButtonText.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(delay);
        yield return FadeImage(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // https://forum.unity.com/threads/simple-ui-animation-fade-in-fade-out-c.439825/
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                ButtonImage.color = new Color(1, 1, 1, i);
                ButtonText.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                ButtonImage.color = new Color(1, 1, 1, i);
                ButtonText.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
