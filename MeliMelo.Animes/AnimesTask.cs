﻿using MeliMelo.Core.Configuration.Values;
using MeliMelo.Core.Tasks;
using MeliMelo.Utils.Log;
using System;
using System.Collections.Generic;
using System.IO;

namespace MeliMelo.Animes
{
    /// <summary>
    /// Represents the animes task
    /// </summary>
    public class AnimesTask : IAutoTask
    {
        /// <summary>
        /// Creates a new AnimesTask
        /// </summary>
        /// <param name="input">Input configuration value</param>
        /// <param name="output">Output configuration value</param>
        public AnimesTask(PathValue input, PathValue output)
        {
            log_ = LogManager.Instance.Get("MeliMelo.Animes");
            input_ = input;
            output_ = output;
            watchers_ = new List<FileSystemWatcher>();
            reader_ = new Reader();
        }

        /// <summary>
        /// Gets the task name
        /// </summary>
        public string Name
        {
            get
            {
                return "Animes";
            }
        }

        /// <summary>
        /// Gets if the task is running
        /// </summary>
        public bool Running
        {
            get
            {
                return running_;
            }
        }

        /// <summary>
        /// Runs the task
        /// </summary>
        public void Run()
        {
            // Nothing to do here
        }

        /// <summary>
        /// Starts the watcher
        /// </summary>
        public void Start()
        {
            if (!running_)
            {
                log_.Info("SortService (Start)", "Starting sort service...");

                string input = input_.Value;
                string output = output_.Value;

                // If we have some folders to scan
                if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(output))
                {
                    // If the directory exists
                    if (Directory.Exists(input))
                    {
                        // Before watching anything, scan the folder
                        Scan(input);

                        log_.Info("SortService (Start)", "Setting up watcher for folder " + input);

                        // Create a new watcher
                        FileSystemWatcher watcher = new FileSystemWatcher();

                        // Set the watcher path
                        watcher.Path = input;

                        // Set watcher handlers
                        watcher.Created += new FileSystemEventHandler(OnChanged);
                        watcher.Error += new ErrorEventHandler(OnError);

                        // Start to watch events
                        watcher.EnableRaisingEvents = true;

                        // Add this watcher to our list of watchers
                        watchers_.Add(watcher);
                    }

                    if (watchers_.Count > 0)
                    {
                        log_.Info("SortService (Start)", "Sort service started");
                        running_ = true;
                    }
                    else
                        log_.Error("SortService (Start)", "Could not start sort service: no folder to sort");
                }
                else
                    log_.Error("SortService (Start)", "Could not start sort service: no folder to sort");
            }
        }

        /// <summary>
        /// Stops the listener
        /// </summary>
        public void Stop()
        {
            if (running_)
            {
                log_.Info("SortService (Stop)", "Stopping all watchers");

                // For each watcher we have
                foreach (FileSystemWatcher watcher in watchers_)
                {
                    log_.Info("SortService (Stop)", "Stopping watcher for folder " + watcher.Path + "...");

                    // Stop to listen to events
                    watcher.EnableRaisingEvents = false;

                    // Dispose of everything
                    watcher.Dispose();

                    log_.Info("SortService (Stop)", "Watcher for folder " + watcher.Path + " stopped");
                }

                // Clear our watchers
                watchers_.Clear();

                log_.Info("SortService (Stop)", "All watchers stopped");

                running_ = false;
            }
        }

        /// <summary>
        /// Node found event, triggered when a new node is found
        /// </summary>
        public event EventHandler<SortNode> NodeFound;

        /// <summary>
        /// On changed event
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="e">the arguments</param>
        protected void OnChanged(object source, FileSystemEventArgs e)
        {
            // Get the event informations
            string path = e.FullPath;
            string name = e.Name;

            try
            {
                // If the event is a created event and the path we got is a file
                if (e.ChangeType == WatcherChangeTypes.Created && File.GetAttributes(path) != FileAttributes.Directory)
                    // Send a node to the listeners
                    SendNode(path, name);
            }
            catch (Exception)
            {
                // Ignore exceptions, the file doesn't exist or something else, so we just don't
                // send it
            }
        }

        /// <summary>
        /// Called on error
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the arguments</param>
        protected void OnError(object sender, ErrorEventArgs e)
        {
            log_.Error("SortService (OnError)", "An error has occured: " + e.GetException().Message);
        }

        /// <summary>
        /// Scans the unsorted folder at startup
        /// </summary>
        protected void Scan(string folder)
        {
            log_.Info("SortService (Scan)", "Scanning folder " + folder + "...");

            // For each file in the unsorted directory
            foreach (string path in Directory.GetFiles(folder))
            {
                // Get the file name
                string name = Path.GetFileName(path);

                // Send a node to the listeners
                SendNode(path, name);
            }

            log_.Info("SortService (Scan)", "Folder " + folder + " scanned");
        }

        /// <summary>
        /// Sends a node to the listeners
        /// </summary>
        /// <param name="path">the file path</param>
        /// <param name="name">the file name</param>
        protected void SendNode(string path, string name)
        {
            // Get an anime from the file name
            Anime anime = reader_.Read(name);

            // If the file name represents a valid anime
            if (anime.IsValid())
            {
                // Get the output directory path
                string outputDirectory = Path.Combine(output_.Value, anime.AnimeTitle());

                // If the output directory does not exist
                if (!Directory.Exists(outputDirectory))
                    // Create it
                    Directory.CreateDirectory(outputDirectory);

                // Create a new node
                SortNode node = new SortNode(anime, path, Path.Combine(outputDirectory, name));

                // Trigger the node found event
                NodeFound(this, node);
            }
            else
                log_.Error("SortService(SendNode)", "Could not sort file " + name + ": malformatted name");
        }

        /// <summary>
        /// Input folder
        /// </summary>
        protected PathValue input_;

        /// <summary>
        /// The log file
        /// </summary>
        protected ILog log_;

        /// <summary>
        /// Output folder
        /// </summary>
        protected PathValue output_;

        /// <summary>
        /// The anitomy reader
        /// </summary>
        protected Reader reader_;

        /// <summary>
        /// If the task is running
        /// </summary>
        protected bool running_;

        /// <summary>
        /// The watchers
        /// </summary>
        protected List<FileSystemWatcher> watchers_;
    }
}
