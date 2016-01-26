using Caliburn.Micro;
using MeliMelo.Properties;
using MeliMelo.ViewModels;
using Squirrel;
using System;

namespace MeliMelo.Core
{
    /// <summary>
    /// Wrapper around the Squirrel UpdateManager
    /// </summary>
    internal static class Updater
    {
        /// <summary>
        /// Creates the shortcuts for the installed application
        /// </summary>
        public static async void CreateShortcuts()
        {
            Utils.Log.ILog log = Utils.Log.LogManager.Instance.Get("MeliMelo.Updater");

            try
            {
                string url = Settings.Default.kProjectURL;

                using (var manager = await UpdateManager.GitHubUpdateManager(url))
                {
                    SquirrelAwareApp.HandleEvents(onFirstRun: async () =>
                    {
                        manager.CreateShortcutsForExecutable("MeliMelo.exe",
                            ShortcutLocation.Desktop | ShortcutLocation.Startup, false);

                        await manager.CreateUninstallerRegistryEntry();
                    });
                }
            }
            catch (Exception e)
            {
                log.Error("Updater (FirstRun)", e.Message);
            }
        }

        /// <summary>
        /// Check for updates and if an update is available, prompts the user, then updates
        /// </summary>
        /// <param name="windows">Window manager used to show the update window</param>
        public static async void Update(IWindowManager windows)
        {
            Utils.Log.ILog log = Utils.Log.LogManager.Instance.Get("MeliMelo.Updater");

            try
            {
                bool restart = false;

                string url = Settings.Default.kProjectURL;

                log.Info("Updater (Update)", "Checking for updates");

                using (var manager = await UpdateManager.GitHubUpdateManager(url))
                {
                    var update_info = await manager.CheckForUpdate();
                    if (update_info != null && update_info.ReleasesToApply.Count > 0)
                    {
                        log.Info("Updater (Update)", "Update found, asking user");

                        var result = windows.ShowDialog(new ApplicationUpdateViewModel(true,
                            update_info));

                        if (result.HasValue && result.Value)
                        {
                            log.Info("Updater (Update)", "User accepted, starting update");

                            await manager.DownloadReleases(update_info.ReleasesToApply);
                            await manager.ApplyReleases(update_info);
                            restart = true;

                            log.Info("Updater (Update)", "Update finished");
                        }
                    }
                    else
                    {
                        log.Info("Updater (Update)", "No update found");

                        windows.ShowDialog(new ApplicationUpdateViewModel(false, update_info));
                    }
                }

                if (restart)
                {
                    log.Info("Updater (Update)", "Restarting application");

                    UpdateManager.RestartApp();
                }
            }
            catch (Exception e)
            {
                log.Error("Updater (Update)", e.Message);

                windows.ShowDialog(new ApplicationUpdateViewModel(false, null));
            }
        }
    }
}
