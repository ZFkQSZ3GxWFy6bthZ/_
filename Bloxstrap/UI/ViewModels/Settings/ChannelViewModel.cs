﻿using Bloxstrap.AppData;
using Bloxstrap.RobloxInterfaces;

namespace Bloxstrap.UI.ViewModels.Settings
{
    public class ChannelViewModel : NotifyPropertyChangedViewModel
    {
        private string _oldPlayerVersionGuid = "";
        private string _oldStudioVersionGuid = "";

        public ChannelViewModel()
        {
            Task.Run(() => LoadChannelDeployInfo(App.Settings.Prop.Channel));
        }

        public bool UpdateCheckingEnabled
        {
            get => App.Settings.Prop.CheckForUpdates;
            set => App.Settings.Prop.CheckForUpdates = value;
        }

        private async Task LoadChannelDeployInfo(string channel)
        {
            const string LOG_IDENT = "ChannelViewModel::LoadChannelDeployInfo";

            ShowLoadingError = false;
            OnPropertyChanged(nameof(ShowLoadingError));

            ChannelInfoLoadingText = "Fetching latest deploy info, please wait...";
            OnPropertyChanged(nameof(ChannelInfoLoadingText));

            ChannelDeployInfo = null;
            OnPropertyChanged(nameof(ChannelDeployInfo));

            try
            {
                ClientVersion info = await Deployment.GetInfo(false, channel);

                ShowChannelWarning = info.IsBehindDefaultChannel;
                OnPropertyChanged(nameof(ShowChannelWarning));

                ChannelDeployInfo = new DeployInfo
                {
                    Version = info.Version,
                    VersionGuid = info.VersionGuid
                };

                App.State.Prop.IgnoreOutdatedChannel = true;

                OnPropertyChanged(nameof(ChannelDeployInfo));
            }
            catch (HttpResponseException ex)
            {
                ShowLoadingError = true;
                OnPropertyChanged(nameof(ShowLoadingError));

                ChannelInfoLoadingText = ex.ResponseMessage.StatusCode switch
                {
                    HttpStatusCode.NotFound => "The specified channel name does not exist.",
                    _ => $"Failed to fetch information! (HTTP {(int)ex.ResponseMessage.StatusCode} - {ex.ResponseMessage.ReasonPhrase})",
                };
                OnPropertyChanged(nameof(ChannelInfoLoadingText));
            }
            catch (Exception ex)
            {
                App.Logger.WriteLine(LOG_IDENT, "An exception occurred while fetching channel information");
                App.Logger.WriteException(LOG_IDENT, ex);

                ShowLoadingError = true;
                OnPropertyChanged(nameof(ShowLoadingError));

                ChannelInfoLoadingText = $"Failed to fetch information! ({ex.Message})";
                OnPropertyChanged(nameof(ChannelInfoLoadingText));
            }
        }

        public bool ShowLoadingError { get; set; } = false;
        public bool ShowChannelWarning { get; set; } = false;

        public DeployInfo? ChannelDeployInfo { get; private set; } = null;
        public string ChannelInfoLoadingText { get; private set; } = null!;

        public string ViewChannel
        {
            get => App.Settings.Prop.Channel;
            set
            {
                value = value.Trim();
                Task.Run(() => LoadChannelDeployInfo(value));
                App.Settings.Prop.Channel = value;
            }
        }

        public string ChannelHash
        {
            get => App.Settings.Prop.ChannelHash;
            set => App.Settings.Prop.ChannelHash = value;
        }

        public bool UpdateRoblox
        {
            get => App.Settings.Prop.UpdateRoblox;
            set => App.Settings.Prop.UpdateRoblox = value;
        }

        public bool ForceRobloxReinstallation
        {
            // wouldnt it be better to check old version guids?
            // what about fresh installs?
            get => String.IsNullOrEmpty(App.State.Prop.Player.VersionGuid) && String.IsNullOrEmpty(App.State.Prop.Studio.VersionGuid);
            set
            {
                if (value)
                {
                    _oldPlayerVersionGuid = App.State.Prop.Player.VersionGuid;
                    _oldStudioVersionGuid = App.State.Prop.Studio.VersionGuid;
                    App.State.Prop.Player.VersionGuid = "";
                    App.State.Prop.Studio.VersionGuid = "";
                }
                else
                {
                    App.State.Prop.Player.VersionGuid = _oldPlayerVersionGuid;
                    App.State.Prop.Studio.VersionGuid = _oldStudioVersionGuid;
                }
            }
        }
    }
}
