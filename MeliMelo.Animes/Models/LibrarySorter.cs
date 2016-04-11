using Castle.Core;
using MeliMelo.Common.Utils;
using System.IO;

namespace MeliMelo.Animes.Models
{
    public interface ILibrarySorterFactory
    {
        LibrarySorter Create(Library library);

        void Release(LibrarySorter task);
    }

    public class LibrarySorter : DisposableBase, IInitializable
    {
        public LibrarySorter(Library library)
        {
            library_ = library;
            queue_ = new SortingQueue();
            watcher_ = new DirectoryWatcher(library.Input);
        }

        public string Input
        {
            get
            {
                return library_.Input;
            }
            set
            {
                library_.Input = value;
                watcher_.Path = value;
            }
        }

        public bool Monitoring
        {
            get
            {
                return library_.Monitoring;
            }
        }

        public string Name
        {
            get
            {
                return library_.Name;
            }
            set
            {
                library_.Name = value;
            }
        }

        public string Output
        {
            get
            {
                return library_.Output;
            }
            set
            {
                library_.Output = value;
            }
        }

        public SortingQueue Queue
        {
            get
            {
                return queue_;
            }
        }

        /// <summary>
        /// Gets/sets the Reader used by this sorter
        /// </summary>
        public Reader Reader
        {
            get;
            set;
        }

        public void Initialize()
        {
            watcher_.NewFile += OnWatcherNewFile;

            if (library_.Monitoring)
            {
                if (Directory.Exists(Input))
                {
                    watcher_.Start();
                }
            }
        }

        public void Start()
        {
            if (!library_.Monitoring)
            {
                if (Directory.Exists(Input))
                {
                    watcher_.Start();

                    Scan();

                    library_.Monitoring = true;
                }
            }
        }

        public void Stop()
        {
            if (library_.Monitoring)
            {
                watcher_.Stop();

                library_.Monitoring = false;
            }
        }

        protected override void OnFinalize()
        {
            watcher_.Dispose();
        }

        protected void OnWatcherNewFile(FileInfo data)
        {
            Sort(data);
        }

        protected void Scan()
        {
            foreach (var file in Directory.GetFiles(Input))
            {
                var info = new FileInfo(file);

                if (info.Attributes != FileAttributes.Directory)
                {
                    Sort(info);
                }
            }
        }

        protected void Sort(FileInfo file)
        {
            Anime anime = Reader.Read(file.Name);

            if (anime.IsValid())
            {
                queue_.Enqueue(new SortingNode(anime, file.FullName, Output));
            }
        }

        protected Library library_;

        protected SortingQueue queue_;

        protected DirectoryWatcher watcher_;
    }
}
