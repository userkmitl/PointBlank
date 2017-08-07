﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointBlank.API;
using PointBlank.API.Services;
using PointBlank.API.DataManagment;
using Newtonsoft.Json.Linq;

namespace PointBlank.Services.UpdateChecker
{
    [Service("UpdateChecker", true)]
    internal class UpdateChecker : Service
    {
        #region Info
        public static readonly string URL = "https://pastebin.com/raw/ZVcNXEVw";
        #endregion

        #region Variables
        private Thread tChecker;

        private bool Running = true;
        #endregion

        public override void Load()
        {
            // Setup the variables
            tChecker = new Thread(new ThreadStart(CheckUpdates));

            // Run the code
            tChecker.Start();
        }

        public override void Unload()
        {
            Running = false;
            tChecker.Abort();
        }

        #region Thread Functions
        private void CheckUpdates()
        {
            while (Running)
            {
                Thread.Sleep(300000); // Check every 5 minutes
                if (WebsiteData.GetData(URL, out string data))
                {
                    JObject info = JObject.Parse(data);

                    if ((string)info["PointBlank_Version"] == "0") // Ignore if no version
                        continue;
                    if ((string)info["PointBlank_Version"] != PointBlankInfo.Version)
                        Logging.LogImportant("A new update is available for PointBlank!");
                }
            }
        }
        #endregion
    }
}
