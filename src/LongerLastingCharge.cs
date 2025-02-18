using MSCLoader;
using UnityEngine;

namespace LongerLastingCharge  
{
    public class LongerLastingCharge : Mod
    {
        private GameObject modifierObject = null;

        private SettingsSliderInt radio_consumption_slider = null;
        private SettingsText radio_charge_duration_text = null;

        private SettingsSlider flashlight_consumption_slider = null;
        private SettingsText flashlight_charge_duration_text = null;

        public override string ID => "LongerLastingCharge"; // Your (unique) mod ID 
        public override string Name => "Longer Lasting Charge"; // Your mod name
        public override string Author => "Xmusjackson"; // Name of the Author (your name)
        public override string Version => "0.8"; // Version
        public override string Description => "Make the batteries last longer in the boombox and flashlight!";
        public override byte[] Icon => Properties.Resource.Icon;

        public override void ModSetup()
        {
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.ModSettingsLoaded, Mod_SettingsLoaded);
        }

        private void Mod_Settings()
        {
            Settings.AddHeader("Radio Consumption Control");

            Settings.AddText("This setting controls how quickly the radio consumes battery power. Higher values mean slower consumption." +
                " The game divides 1 by this number and multiplies that by the volume level. It then subtracts that amount in total from the radio's charge level once every second. ");

            string radio_dur_text = CalculateDuration(8000);
            string flashlight_dur_text = CalculateDuration(0.0013f);


            radio_charge_duration_text = Settings.AddText("Full Charge Duration: " + radio_dur_text);

            radio_consumption_slider = Settings.AddSlider("_consumption_divider_setting", "Consumption Divider", 8000, 100000, 8000, UpdateCallback);

            Settings.AddHeader("Flashlight Consumption Control");

            Settings.AddText("This setting controls how quickly the flashlight consumes battery power. Lower values mean slower consumption." +
                "The game simply subtracts this number in total from the remaining charge once every second. A value of 0 will result in infinite charge!");

            flashlight_charge_duration_text = Settings.AddText("Full Charge Duration: " + flashlight_dur_text);

            flashlight_consumption_slider = Settings.AddSlider("_consumption_setting", "Consumption", 0f, 0.0013f, 0.0013f, UpdateCallback, 8);
        }

        private void Mod_SettingsLoaded()
        {
            string radio_dur_text = CalculateDuration(radio_consumption_slider.GetValue());
            string flashlight_dur_text = CalculateDuration(flashlight_consumption_slider.GetValue());

            radio_charge_duration_text.SetValue("Full Charge Duration: " + radio_dur_text);
            flashlight_charge_duration_text.SetValue("Full Charge Duration: " + flashlight_dur_text);
        }

        private void UpdateCallback()
        {
            string radio_dur_text = CalculateDuration(radio_consumption_slider.GetValue());
            string flashlight_dur_text = CalculateDuration(flashlight_consumption_slider.GetValue());

            radio_charge_duration_text.SetValue("Full Charge Duration: " + radio_dur_text);
            flashlight_charge_duration_text.SetValue("Full Charge Duration: " + flashlight_dur_text);

            RadioModifier radioModifier = GameObject.FindObjectOfType<RadioModifier>();
            FlashlightModifier flashlightModifier = GameObject.FindObjectOfType<FlashlightModifier>();

            if (radioModifier == null || flashlightModifier == null)
            {
                return;
            }

            radioModifier.UpdateValue(radio_consumption_slider.GetValue());
            flashlightModifier.UpdateValue(flashlight_consumption_slider.GetValue());
        }

        private string CalculateDuration(int consumption_divider)
        {
            int duration = (int)(1f / (1f / (float)consumption_divider));
            int dur_hours = duration / 3600;
            int dur_minutes = (duration % 3600) / 60;
            int dur_seconds = duration % 60;
            return dur_hours.ToString().PadLeft(2, '0') + ":" + dur_minutes.ToString().PadLeft(2, '0') + ":" + dur_seconds.ToString().PadLeft(2, '0');
        }

        private string CalculateDuration(float consumption)
        {
            int duration = (int)(1f / consumption);
            int dur_hours = duration / 3600;
            int dur_minutes = (duration % 3600) / 60;
            int dur_seconds = duration % 60;
            return dur_hours.ToString().PadLeft(2, '0') + ":" + dur_minutes.ToString().PadLeft(2, '0') + ":" + dur_seconds.ToString().PadLeft(2, '0');
        }

        private void Mod_OnLoad()
        {
            modifierObject = new GameObject();
            modifierObject.AddComponent<RadioModifier>().UpdateValue(radio_consumption_slider.GetValue());
            modifierObject.AddComponent<FlashlightModifier>().UpdateValue(flashlight_consumption_slider.GetValue());
        }       
    }
}


