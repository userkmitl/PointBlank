﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointBlank.API.Commands;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;
using SDG.Framework.Modules;
using Translation = PointBlank.Framework.Translations.CommandTranslations;

namespace PointBlank.Commands
{
    [PointBlankCommand("Modules", 0)]
    internal class CommandModules : PointBlankCommand
    {
        #region Properties
        public override string[] DefaultCommands => new string[]
        {
            "Modules"
        };

        public override string Help => "Lists all the module on the server";

        public override string Usage => Commands[0];

        public override string DefaultPermission => "unturned.commands.server.modules";

        public override EAllowedCaller AllowedCaller => EAllowedCaller.SERVER;
        #endregion

        public override void Execute(UnturnedPlayer executor, string[] args)
        {
            UnturnedChat.SendMessage(executor, "Module list");
            for(int i = 0; i < ModuleHook.modules.Count; i++)
            {
                if (ModuleHook.modules[i] == null)
                    continue;
                if (ModuleHook.modules[i].config == null)
                    continue;
                if (!ModuleHook.modules[i].isEnabled)
                    continue;

                UnturnedChat.SendMessage(executor, "Name: " + ModuleHook.modules[i].config.Name);
                UnturnedChat.SendMessage(executor, "Version: " + ModuleHook.modules[i].config.Version);
            }
        }
    }
}
