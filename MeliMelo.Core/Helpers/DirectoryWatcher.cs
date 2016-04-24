using System.IO;

namespace MeliMelo.Core.Helpers
{
    /// <summary>
    /// Represents a directory watcher
    /// </summary>
    public class DirectoryWatcher : DisposableBase
    {
        /// <summary>
        /// Creates a new DirectoryWatcher
        /// </summary>
        /// <param name="path">Directory path</param>
        public DirectoryWatcher(string path)
        {
            watcher_ = new FileSystemWatcher(path);

            watcher_.Created += OnWatcherCreated;
        }

        /// <summary>
        /// Gets/sets the watcher path
        /// </summary>
        public string Path
        {
            get
            {
                return watcher_.Path;
            }
            set
            {
                watcher_.EnableRaisingEvents = false;
                watcher_.Path = value;
                watcher_.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Starts to monitor watched directory
        /// </summary>
        public void Start()
        {
            watcher_.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Stops to monitor watched directory
        /// </summary>
        public void Stop()
        {
            watcher_.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Triggered when a new file was added to the directory
        /// </summary>
        public event DataEventHandler<FileInfo> NewFile;

        /// <inheritdoc />
        protected override void OnFinalize()
        {
            watcher_.Dispose();
        }

        /// <summary>
        /// Called when the internal watcher found a new created file
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnWatcherCreated(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;

            if (File.GetAttributes(path) != FileAttributes.Directory)
            {
                if (NewFile != null)
                    NewFile(new FileInfo(path));
            }
        }

        /// <summary>
        /// Internal watcher
        /// </summary>
        private FileSystemWatcher watcher_;
    }
}
