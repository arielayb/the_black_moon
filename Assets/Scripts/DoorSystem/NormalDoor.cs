using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalDoor : MonoBehaviour, DoorSystemInterface
{
    [SerializeField] private GameObject door;
    public float openRot;
    public float closeRot;
    public float speed;
    public bool opening;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public bool UnlockedDoor()
    {
        if(!opening){
            print("Door opened!");
            opening = true;
        }else{
            print("Door closed!");
            opening = false;
        }

        return opening;
    }

    public void LockedDoor()
    {

    }

    public void EventDoor()
    {

    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        UnlockedDoor();
        interactSuccessful = true;
    }

    public void EndInteraction()
    {
        // if(opening){
        //     opening = false;
        // }
    }

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        if (opening)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot,new Vector3(currentRot.x,
                                                                            openRot,
                                                                            currentRot.z),
                                                                            speed * Time.deltaTime);
            }
        }else{
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x,
                                                                            closeRot,
                                                                            currentRot.z),
                                                                            speed * Time.deltaTime);
            }
        }
    }
}
