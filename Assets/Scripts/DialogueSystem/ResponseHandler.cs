using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private Image responseBoxGameObj;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    List<GameObject> temporaryResponseButtons = new List<GameObject>();

    private void Start(){
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents){
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses){
        float responseBoxHeight = 0;

        for(int i = 0; i < responses.Length; i++){
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            Debug.Log(response.ResponseText);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
            temporaryResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
        responseBoxGameObj.enabled = true;
    }

    private void OnPickedResponse(Response response, int responseIndex){
        responseBox.gameObject.SetActive(false);
        responseBoxGameObj.enabled = false;

        foreach(GameObject button in temporaryResponseButtons){
            Destroy(button);
        }

        temporaryResponseButtons.Clear();

        if(responseEvents != null && responseIndex <= responseEvents.Length){
            if(responseEvents != null){
                responseEvents[responseIndex].OnPickedResponse.Invoke();
            }
        }

        responseEvents = null;

        if(response.DialogueObject){
            dialogueUI.ShowDialogue(response.DialogueObject);
        }else{
            dialogueUI.CloseDialogueBox();
        }

    }
}
