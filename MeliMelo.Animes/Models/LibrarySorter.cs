using MeliMelo.Animes.Collections;
using System.IO;

namespace MeliMelo.Animes.Models
{
    public interface ILibrarySorterFactory
    {
        LibrarySorter Create(Library library, AnimeParser parser);

        void Release(LibrarySorter task);
    }

    public class LibrarySorter
    {
        public LibrarySorter(Library library, AnimeParser parser)
        {
            library_ = library;
            parser_ = parser;
            queue_ = new SortingQueue();
        }

        public string Input
        {
            get
            {
                return library_.input;
            }
            set
            {
                library_.input = value;
            }
        }

        public string Name
        {
            get
            {
                return library_.name;
            }
            set
            {
                library_.name = value;
            }
        }

        public string Output
        {
            get
            {
                return library_.output;
            }
            set
            {
                library_.output = value;
            }
        }

        public SortingQueue Queue
        {
            get
            {
                return queue_;
            }
        }

        public void Scan()
        {
            foreach (var file in Directory.GetFiles(Input, "*", SearchOption.AllDirectories))
            {
                var info = new FileInfo(file);

                if (info.Attributes != FileAttributes.Directory)
                {
                    Sort(info);
                }
            }
        }

        private void Sort(FileInfo file)
        {
            Anime anime = parser_.Read(file.Name);

            if (anime.IsValid())
            {
                queue_.Enqueue(new SortingNode(anime, file.FullName, Output));
            }
        }

        private Library library_;
        private AnimeParser parser_;
        private SortingQueue queue_;
    }
}
