using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObj;
    [SerializeField] private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObj;

    public ResponseEvent[] Events => events;

    public void OnValidate() {
        if(dialogueObj == null){
            return;
        }

        if(dialogueObj.Responses == null){
            return;
        }

        if(events != null && events.Length == dialogueObj.Responses.Length){
            return;
        }

        if(events == null){
            events = new ResponseEvent[dialogueObj.Responses.Length];
        }else{
            Array.Resize(ref events, dialogueObj.Responses.Length);
        }

        for(int i = 0; i < dialogueObj.Responses.Length; i++){
            Response response = dialogueObj.Responses[i];
            if(events[i] != null){
                events[i].name = response.ResponseText;
                continue;
            }
        
            events[i] = new ResponseEvent(){
                name = response.ResponseText
            };
        }
    }
}
