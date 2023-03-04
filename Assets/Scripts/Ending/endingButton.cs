using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class endingButton : MonoBehaviour
{
    [SerializeField] GameObject endingButtonUI;
    [SerializeField] GameObject showImgUI;
    [SerializeField] Image img;
    [SerializeField] Image bgImg;
    [SerializeField] GameObject showLRButtonUI;
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject lButton;

    [SerializeField] Sprite[] imgs;
    enum Steps
    {
        notShow,
        showEndingButton,
        showImg,
        quitGame
    }
    [SerializeField] Steps nowStep = Steps.notShow;

    int page = 1;
    [SerializeField] int maxPage = 3;
    bool isLoad = true;

    // Start is called before the first frame update
    void Start()
    {
        nowStep = Steps.notShow;
        isLoad = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowStep)
        {
            case Steps.notShow:
                if (!isLoad)
                {
                    endingButtonUI.SetActive(false);
                    showImgUI.SetActive(false);
                    showLRButtonUI.SetActive(false);
                    rButton.SetActive(false);
                    lButton.SetActive(false);
                    ShowEndingButton();
                    isLoad = true;
                }
                break;
            case Steps.showEndingButton:
                if(!isLoad)
                {
                    endingButtonUI.SetActive(true);
                    showImgUI.SetActive(false);
                    showLRButtonUI.SetActive(false);
                    rButton.SetActive(false);
                    lButton.SetActive(false);
                    isLoad = true;
                    Debug.Log("Load complete");
                }
                break;
            case Steps.showImg:
                if(Input.GetKeyUp(KeyCode.Escape))
                {
                    ShowEndingButton();
                    break;
                }
                if(!isLoad)
                {
                    isLoad = true;
                    if (page < maxPage) rButton.SetActive(true);
                    else rButton.SetActive(false);
                    if (page > 1) lButton.SetActive(true);
                    else lButton.SetActive(false);
                    img.sprite = imgs[page - 1]; 
                }
                break;
            default:
                break;
        }
    }

    public void ShowEndingButton()
    {
        nowStep = Steps.showEndingButton;
        isLoad = false;
        Debug.Log("ShowEndingButton() Call");
    }
    public void ButtonShowImg()
    {
        endingButtonUI.SetActive(false);
        showImgUI.SetActive(true);
        showLRButtonUI.SetActive(true);
        rButton.SetActive(false);
        lButton.SetActive(false);
        isLoad = false;
        nowStep = Steps.showImg;
        bgImg.color = Color.black;
        img.rectTransform.sizeDelta = new Vector2(1280,720);
        page = 1;
        Debug.Log("ButtonShowImg() Call");
    }
    public void ButtonEndGame()
    {
        nowStep = Steps.quitGame;
        Debug.Log("ButtonEndGame() Call");
        Application.Quit();
    }
    public void ButtonRight()
    {
        page++;
        if(page>maxPage) page = maxPage;
        isLoad = false;
    }
    public void ButtonLeft()
    {
        page--;
        if(page<1) page = 1;
        isLoad = false;
    }
}
