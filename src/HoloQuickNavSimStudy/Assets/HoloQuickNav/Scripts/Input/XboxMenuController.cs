using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XboxMenuController : MonoBehaviour {

    [Tooltip("Selector object used to highlight option on menu.")]
    public GameObject selector;
    private float selectorBaseLocation;
    [Tooltip("Distance for selector to move between buttons.")]
    public float selectorIncrementDistance = 0.03f;
    private int selectorPosition = 0;
    private int numOfButtons;

    [Tooltip("List of actions to perform, order corresponds to order and number of buttons on menu.")]
    public UnityEvent[] Actions;

    // Use this for initialization
    void Start () {

        numOfButtons = Actions.Length;
        selectorBaseLocation = selector.transform.position.y;
        selectorPosition = 0;
        setSelectorLocation();
    }

    private void setSelectorLocation()
    {
        selector.transform.position = new Vector3(selector.transform.position.x, (selectorBaseLocation - selectorIncrementDistance * selectorPosition), selector.transform.position.z);
    }

    public void moveDownList()
    {
        if (selectorPosition != (numOfButtons - 1))
        {
                selectorPosition++;
                setSelectorLocation();
        }
    }

    public void moveUpList()
    {
        if (selectorPosition != 0)
        {
            selectorPosition--;
            setSelectorLocation();
        }

    }

    public void selectButton()
    {
        Actions[selectorPosition].Invoke();
    }
}
