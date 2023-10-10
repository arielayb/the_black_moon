using UnityEngine;
using PlayerControllerEvent;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogObj(DialogueObject dialogueObject){
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerController player)){
            print("we here!");
            player.interactable = this;
        }
    }

    private void OnTriggerExit(Collider other){
         if(other.CompareTag("Player") && other.TryGetComponent(out PlayerController player)){
            if(player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this){
                player.interactable = null;
            }
        }
    }

    public void Interact(PlayerController player){
       foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>()){
            if(responseEvents.DialogueObject == dialogueObject){
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
