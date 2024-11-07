﻿using System.Collections.ObjectModel;

namespace Bloxstrap.Models.Persistable
{
    public class Settings
    {
        // bloxstrap configuration
        public BootstrapperStyle BootstrapperStyle { get; set; } = BootstrapperStyle.ProgressDialog;
        public BootstrapperIcon BootstrapperIcon { get; set; } = BootstrapperIcon.mysteriousobject;
        public string BootstrapperTitle { get; set; } = App.MysteriousString;
        public string BootstrapperIconCustomLocation { get; set; } = "";
        public Theme Theme { get; set; } = Theme.Default;
        public bool CheckForUpdates { get; set; } = false;
        public bool ConfirmLaunches { get; set; } = true;
        public string Locale { get; set; } = "nil";
        public bool ForceRobloxLanguage { get; set; } = false;
        public bool UseFastFlagManager { get; set; } = true;
        public bool WPFSoftwareRender { get; set; } = false;
        public bool EnableAnalytics { get; set; } = false;
        public bool UpdateRoblox {  get; set; } = true;
        public bool MultiInstanceLaunching { get; set; } = false;
        public bool RenameClientToEuroTrucks2 { get; set; } = true;
        public string ClientPath { get; set; } = Path.Combine(Paths.Base, "Roblox//Player");
        public string Channel { get; set; } = Bloxstrap.RobloxInterfaces.Deployment.DefaultChannel;
        public string ChannelHash { get; set; } = "";

        // integration configuration
        public bool EnableActivityTracking { get; set; } = true;
        public bool UseDiscordRichPresence { get; set; } = false;
        public bool HideRPCButtons { get; set; } = true;
        public bool ShowAccountOnRichPresence { get; set; } = false;
        public bool ShowServerDetails { get; set; } = false;
        public ObservableCollection<CustomIntegration> CustomIntegrations { get; set; } = new();

        // mod preset configuration
        public bool UseDisableAppPatch { get; set; } = false;
    }
}
