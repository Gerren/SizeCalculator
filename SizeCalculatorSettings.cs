﻿using Newtonsoft.Json;
using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeCalculator
{
    public class SizeCalculatorSettings : ISettings
    {
        private readonly SizeCalculator plugin;

        public int SizeDecimals { get; set; } = 3;
        public int SizeRound{ get; set; } = 0;
        public bool SizeDisk{ get; set; } = false;


        // Parameterless constructor must exist if you want to use LoadPluginSettings method.
        public SizeCalculatorSettings()
        {
        }

        public SizeCalculatorSettings(SizeCalculator plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<SizeCalculatorSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                SizeDecimals = savedSettings.SizeDecimals;
                SizeRound = savedSettings.SizeRound;
                SizeDisk = savedSettings.SizeDisk;
            }
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to SizeDecimals and Option2.
        }

        public void EndEdit()
        {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to SizeDecimals and Option2.
            if (SizeDecimals < 1) SizeDecimals = 1;
            plugin.SavePluginSettings(this);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}