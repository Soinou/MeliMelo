using Squirrel;
using System;

namespace MeliMelo.ViewModels
{
    internal class ApplicationUpdateViewModel : Caliburn.Micro.Screen
    {
        /// <summary>
        /// Creates a new SoftwareUpdateViewModel
        /// </summary>
        /// <param name="has_update">If the software has an update available</param>
        /// <param name="update_info">Update info</param>
        public ApplicationUpdateViewModel(bool has_update, UpdateInfo update_info)
        {
            DisplayName = "MeliMelo - Application Update";

            has_update_ = has_update;
            update_info_ = update_info;
        }

        /// <summary>
        /// Gets if the update button is active
        /// </summary>
        public bool CanUpdate
        {
            get
            {
                return has_update_;
            }
        }

        /// <summary>
        /// Gets the version message
        /// </summary>
        public string VersionMessage
        {
            get
            {
                if (update_info_ != null)
                {
                    if (has_update_)
                        return "Update available: "
                            + update_info_.FutureReleaseEntry.Version.ToString()
                            + " (Current: "
                            + update_info_.CurrentlyInstalledVersion.Version.ToString() + ").";
                    else
                        return "No update available !";
                }
                else
                    return "Impossible to get updates." + Environment.NewLine
                        + "Please check your internet connection and retry.";
            }
        }

        /// <summary>
        /// Cancels the update
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        /// <summary>
        /// Updates the application
        /// </summary>
        public void Update()
        {
            TryClose(true);
        }

        /// <summary>
        /// If an update is available
        /// </summary>
        protected bool has_update_;

        /// <summary>
        /// Update info
        /// </summary>
        protected UpdateInfo update_info_;
    }
}
