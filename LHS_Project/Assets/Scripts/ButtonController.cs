using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "TitleScene") SceneManager.LoadScene("FlightScene");
        if(SceneManager.GetActiveScene().name == "GameOverScene") SceneManager.LoadScene("TitleScene"); 
    }
}
