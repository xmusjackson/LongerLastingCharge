using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;


public class FlashlightModifier : MonoBehaviour
{
    private GameObject _flashlight_itemx = null;

    private PlayMakerFSM _flashlight_itemx_flashlight = null;

    private FsmFloat flashlight_consumption = null;

    FlashlightModifier()
    {
        _flashlight_itemx = GameObject.Find("flashlight(itemx)");

        if (_flashlight_itemx == null)
        {
            ModConsole.Log("flashlight(itemx) object not found! Aborting!");
            return;
        }

        for (int i = 0; i < _flashlight_itemx.transform.childCount; i++)
        {
            if (_flashlight_itemx.transform.GetChild(i).name.Equals("FlashLight"))
            {
                _flashlight_itemx_flashlight = _flashlight_itemx.transform.GetChild(i).GetComponent<PlayMakerFSM>();
            }
        }

        if (_flashlight_itemx_flashlight == null)
        {
            ModConsole.LogError("flashlight(itemx) child \"FlashLight\" not found! Aborting!");
            return;
        }
        else
        {
            flashlight_consumption = _flashlight_itemx_flashlight.FsmVariables.FindFsmFloat("Consumption");
        }

        if (flashlight_consumption == null)
        {
            ModConsole.LogError("FSM variable \"Consumption\" not found! Aborting!");
            return;
        }
    }

    public void UpdateValue(float value)
    {
        flashlight_consumption.Value = value;
    }
}