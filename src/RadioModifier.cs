using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

public class RadioModifier : MonoBehaviour
{
    private GameObject _radio_itemx = null;

    private PlayMakerFSM _radio_itemx_play = null;

    private FsmFloat radio_consumption_divider = null;

    RadioModifier()
    {
        _radio_itemx = GameObject.Find("radio(itemx)");

        if (_radio_itemx == null)
        {
            ModConsole.Log("radio(itemx) object not found! Aborting!");
            return;
        }

        // This object has two PlayMakers, the second one (called "Charge") is the one we want
        _radio_itemx_play = _radio_itemx.transform.Find("Play").GetComponents<PlayMakerFSM>()[1];

        if (_radio_itemx_play == null)
        {
            ModConsole.LogError("radio(itemx) child \"Play\" not found! Aborting!");
            return;
        }
        
        radio_consumption_divider = _radio_itemx_play.FsmVariables.FindFsmFloat("ConsumptionDivider");

        if (radio_consumption_divider == null)
        {
            ModConsole.LogError("FSM variable \"ConsumptionDivider\" not found! Aborting!");
            return;
        }
    }

    public void UpdateValue(int value)
    {
        radio_consumption_divider.Value = value;
    }
}