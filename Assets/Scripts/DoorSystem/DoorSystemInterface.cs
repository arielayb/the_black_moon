using UnityEngine;

public interface DoorSystemInterface: IInteractable {

    public bool UnlockedDoor();
    public void LockedDoor();
    public void EventDoor();

}
