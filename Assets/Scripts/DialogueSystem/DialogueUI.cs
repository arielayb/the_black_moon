using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen {get; private set;}

    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;

    private void Start(){
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents){
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        for(int i = 0; i < dialogueObject.Dialogues.Length; i++){
            string dialogue = dialogueObject.Dialogues[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if(i == dialogueObject.Dialogues.Length -1 && dialogueObject.HasRespones){
                break;
            }
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if(dialogueObject.HasRespones){
            responseHandler.ShowResponses(dialogueObject.Responses);
        }else{
            CloseDialogueBox();
        }

    }

    private IEnumerator RunTypingEffect(string dialogue){
        typeWriterEffect.Run(dialogue, textLabel);

        while(typeWriterEffect.IsRunning){
            yield return null;
            if(Input.GetKeyDown(KeyCode.Space)){
                typeWriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox(){
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
