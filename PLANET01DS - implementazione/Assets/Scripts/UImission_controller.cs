using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImission_controller : MonoBehaviour
{

    private RectTransform newImage;
    private RectTransform updateImage;

    // Start is called before the first frame update
    void Start()
    {
        newImage = transform.GetChild(0).GetComponent<RectTransform>();
        updateImage = transform.GetChild(1).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewMission()
    {
        newImage.gameObject.SetActive(true);
        StartCoroutine(ShowImage(false));
    }

    public void UpdateMission()
    {
        updateImage.gameObject.SetActive(true);
        StartCoroutine(ShowImage(true));
    }

    IEnumerator ShowImage(bool update)
    {
        if (update)
        {
            int i = 0;
            for( i = 0; i < 20; i++)
            {
                if(i == 10)
                {
                    GetComponent<AudioSource>().Play();
                }
                updateImage.localScale = new Vector2(updateImage.localScale.x + 0.01f, updateImage.localScale.y + 0.01f);
                updateImage.GetComponent<Image>().color = new Color(updateImage.GetComponent<Image>().color.r, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.a + 0.05f);
                yield return new WaitForFixedUpdate();
            }
            /*
            while(updateImage.anchoredPosition.x < -1f)
            {
                updateImage.anchoredPosition = new Vector2(updateImage.anchoredPosition.x + 1f, updateImage.anchoredPosition.y);
                if (updateImage.GetComponent<Image>().color.a < 1f)
                {
                    updateImage.GetComponent<Image>().color = new Color(updateImage.GetComponent<Image>().color.r, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.a + 0.01f);
                }
                yield return new WaitForFixedUpdate();
            }*/
            yield return new WaitForSeconds(3f);
            for (i = 0; i < 20; i++)
            {
                updateImage.localScale = new Vector2(updateImage.localScale.x - 0.01f, updateImage.localScale.y - 0.01f);
                updateImage.GetComponent<Image>().color = new Color(updateImage.GetComponent<Image>().color.r, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.a - 0.05f);
                yield return new WaitForFixedUpdate();
            }
            /*while (updateImage.anchoredPosition.x > -130f)
            {
                updateImage.anchoredPosition = new Vector2(updateImage.anchoredPosition.x - 1f, updateImage.anchoredPosition.y);
                if (updateImage.GetComponent<Image>().color.a > 0f)
                {
                    updateImage.GetComponent<Image>().color = new Color(updateImage.GetComponent<Image>().color.r, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.g, updateImage.GetComponent<Image>().color.a - 0.01f);
                }
                yield return new WaitForFixedUpdate();
            }*/
        }
        else
        {
            int i = 0;
            for (i = 0; i < 20; i++)
            {
                if (i == 10)
                {
                    GetComponent<AudioSource>().Play();
                }
                newImage.localScale = new Vector2(newImage.localScale.x + 0.01f, newImage.localScale.y +0.01f);
                newImage.GetComponent<Image>().color = new Color(newImage.GetComponent<Image>().color.r, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.a + 0.05f);
                yield return new WaitForFixedUpdate();
            }
            /*
            while (newImage.anchoredPosition.x < -1f)
            {
                newImage.anchoredPosition = new Vector2(newImage.anchoredPosition.x + 1f, newImage.anchoredPosition.y);
                if (newImage.GetComponent<Image>().color.a < 1f)
                {
                    newImage.GetComponent<Image>().color = new Color(newImage.GetComponent<Image>().color.r, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.a + 0.01f);
                }
                yield return new WaitForFixedUpdate();
            }
            */
            yield return new WaitForSeconds(3f);
            for (i = 0; i < 20; i++)
            {
                newImage.localScale = new Vector2(newImage.localScale.x - 0.01f, newImage.localScale.y -0.01f);
                newImage.GetComponent<Image>().color = new Color(newImage.GetComponent<Image>().color.r, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.a - 0.05f);
                yield return new WaitForFixedUpdate();
            }
            /*
            while (newImage.anchoredPosition.x > -130f)
            {
                newImage.anchoredPosition = new Vector2(newImage.anchoredPosition.x - 1f, newImage.anchoredPosition.y);
                if (newImage.GetComponent<Image>().color.a > 0f)
                {
                    newImage.GetComponent<Image>().color = new Color(newImage.GetComponent<Image>().color.r, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.g, newImage.GetComponent<Image>().color.a - 0.01f);
                }
                yield return new WaitForFixedUpdate();
            }
            */
        }
        yield return null;
    }
}
