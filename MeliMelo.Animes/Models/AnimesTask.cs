using Castle.Core;
using MeliMelo.Common.Services.Configuration;
using MeliMelo.Common.Services.Configuration.Values;
using MeliMelo.Common.Services.Logging;
using MeliMelo.Common.Utils;
using System.Collections.Generic;
using System.IO;

namespace MeliMelo.Animes.Models
{
    /// <summary>
    /// Represents the animes task
    /// </summary>
    public class AnimesTask : TimedTask, IInitializable
    {
        /// <summary>
        /// Creates a new AnimesTask
        /// </summary>
        public AnimesTask()
        {
            reader_ = new Reader();
            watchers_ = new List<FileSystemWatcher>();
        }

        /// <summary>
        /// Gets/sets the configuration
        /// </summary>
        public IConfiguration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the input folder path
        /// </summary>
        public PathValue Input
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets/sets the logger
        /// </summary>
        public ILog Log
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets/sets the log manager
        /// </summary>
        public ILogManager LogManager
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the output folder path
        /// </summary>
        public PathValue Output
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the sorting queue of this task
        /// </summary>
        public SortingQueue Queue
        {
            get;
            set;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            files_ = new List<string>();
            Input = Configuration.GetPath("Input");
            Log = LogManager.Get("MeliMelo.Animes");
            Output = Configuration.GetPath("Output");
        }

        /// <inheritdoc />
        public override void Run()
        {
            // Queue.Run();
        }

        /// <inheritdoc />
        protected override void OnStart()
        {
            Log.Info("AnimesTask", "Starting file watchers");

            string input = Input.Value;
            string output = Output.Value;

            // If we have a folder to scan and a folder to output to
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(output))
            {
                // If the directory exists
                if (Directory.Exists(input))
                {
                    // Before watching anything, scan the folder
                    Scan();

                    Log.Info("AnimesTask", "Setting up watcher for input directory \"" + input
                        + "\"");

                    // Create a new watcher
                    FileSystemWatcher watcher = new FileSystemWatcher();

                    // Set the watcher path
                    watcher.Path = input;

                    // Set watcher handlers
                    watcher.Created += OnWatcherCreated;
                    watcher.Error += OnWatcherError;

                    // Start to watch events
                    watcher.EnableRaisingEvents = true;

                    // Add this watcher to our list of watchers
                    watchers_.Add(watcher);
                }

                if (watchers_.Count > 0)
                {
                    Log.Info("AnimesTask", "Sort service started");
                }
                else
                {
                    Log.Error("AnimesTask", "Could not start sort service: no directory to "
                        + "sort");
                }
            }
            else
            {
                Log.Error("AnimesTask", "Could not start sort service: no directory to sort");
            }
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            Log.Info("AnimesTask", "Clearing sorting queue and waiting for running nodes to "
                + "finish");

            Queue.Clear();

            Log.Info("AnimesTask", "Sorting queue cleared");

            Log.Info("AnimesTask", "Stopping all file watchers");

            // For each watcher we have
            foreach (FileSystemWatcher watcher in watchers_)
            {
                string path = watcher.Path;

                Log.Info("AnimesTask", "Stopping watcher for directory \"" + path + "\"...");

                watcher.EnableRaisingEvents = false;
                watcher.Dispose();

                Log.Info("AnimesTask", "Watcher for directory \"" + path + "\" stopped");
            }

            watchers_.Clear();

            Log.Info("AnimesTask", "All watchers stopped");
        }

        /// <summary>
        /// Called when a file has been created in the folder watched by one of the watchers
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnWatcherCreated(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            string name = e.Name;

            if (File.GetAttributes(path) != FileAttributes.Directory)
            {
                Log.Info("AnimesTask", "\"" + name + "\" was added to the watched directory");
                Sort(path, name);
            }
        }

        /// <summary>
        /// Called on error
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the arguments</param>
        protected void OnWatcherError(object sender, ErrorEventArgs e)
        {
            Log.Error("AnimesTask", "An error has occured: "
                + e.GetException().Message);
        }

        /// <summary>
        /// Scans the unsorted folder at startup or on task run
        /// </summary>
        protected void Scan()
        {
            string input = Input.Value;

            Log.Info("AnimesTask", "Scanning input directory \"" + input + "\"...");

            // For each file in the unsorted directory
            foreach (string path in Directory.GetFiles(input))
            {
                // Get the file name
                string name = Path.GetFileName(path);

                // Sort the file
                Sort(path, name);
            }

            Log.Info("AnimesTask", "Input directory \"" + input + "\" scanned");
        }

        /// <summary>
        /// Sorts the file given
        /// </summary>
        /// <param name="path">the file path</param>
        /// <param name="name">the file name</param>
        protected void Sort(string path, string name)
        {
            // Get an anime from the file name
            Anime anime = reader_.Read(name);

            // If the file name represents a valid anime
            if (anime.IsValid())
            {
                Log.Info("AnimesTask", "\"" + name + "\" is an anime, will sort it");
                // Create a new sort node and queue it
                Queue.Enqueue(new SortingNode(anime, path, Path.Combine(Output.Value,
                    anime.AnimeTitle(), name)));
            }
            else
            {
                Log.Error("AnimesTask", "Could not sort file " + name + ": malformatted name");
            }
        }

        /// <summary>
        /// List of files being added to the watched folder
        /// </summary>
        protected List<string> files_;

        /// <summary>
        /// The anitomy reader
        /// </summary>
        protected Reader reader_;

        /// <summary>
        /// The watchers
        /// </summary>
        protected List<FileSystemWatcher> watchers_;
    }
}
