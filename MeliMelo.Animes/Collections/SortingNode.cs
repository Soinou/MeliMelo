using MeliMelo.Common.Helpers;
using MeliMelo.Common.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MeliMelo.Animes.Collections
{
    public class SortingNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anime">The anime object detected</param>
        /// <param name="input">The input file path</param>
        /// <param name="output">The output directory</param>
        public SortingNode(Anime anime, string input, string output)
        {
            anime_ = anime;
            input_ = input;
            output_ = Path.Combine(output, anime.AnimeTitle());
            progress_ = 0;
            task_ = null;
        }

        /// <summary>
        /// Gets the anime episode number
        /// </summary>
        public string Episode
        {
            get
            {
                return anime_.EpisodeNumber();
            }
        }

        /// <summary>
        /// Gets the input file path
        /// </summary>
        public string Input
        {
            get
            {
                return input_;
            }
        }

        /// <summary>
        /// Gets the output file path
        /// </summary>
        public string Output
        {
            get
            {
                return output_;
            }
        }

        /// <summary>
        /// Gets the file copy progress
        /// </summary>
        public byte Progress
        {
            get
            {
                return progress_;
            }
        }

        /// <summary>
        /// Gets if the node is currently being moved or not
        /// </summary>
        public bool Running
        {
            get
            {
                return task_ != null;
            }
        }

        /// <summary>
        /// Gets the node anime title
        /// </summary>
        public string Title
        {
            get
            {
                return anime_.AnimeTitle();
            }
        }

        /// <summary>
        /// Moves the file
        /// </summary>
        public void Move()
        {
            var file_path = Path.Combine(output_, Path.GetFileName(input_));

            Console.WriteLine("File path: " + file_path);

            task_ = Task.Run(() =>
            {
                if (Started != null)
                    Started();

                try
                {
                    // Then move the source to the destination
                    IoHelper.Move(input_, file_path, (byte progress) =>
                    {
                        progress_ = progress;

                        if (Changed != null)
                            Changed(progress_);
                    });
                }
                catch (Exception)
                {
                    // Send some errors to the listeners
                }

                task_ = null;

                if (Finished != null)
                    Finished();
            });
        }

        /// <summary>
        /// Waits for the current move task to finish
        /// </summary>
        public void Wait()
        {
            if (task_ != null)
            {
                task_.Wait();
            }
        }

        /// <summary>
        /// Triggered when the copy progress has changed
        /// </summary>
        public event DataEventHandler<byte> Changed;

        /// <summary>
        /// Triggered when the node has finished moving
        /// </summary>
        public event DataEventHandler Finished;

        /// <summary>
        /// Triggered when the node has started moving
        /// </summary>
        public event DataEventHandler Started;

        /// <summary>
        /// The recognized anime
        /// </summary>
        private Anime anime_;

        /// <summary>
        /// The node source
        /// </summary>
        private string input_;

        /// <summary>
        /// The node destination
        /// </summary>
        private string output_;

        /// <summary>
        /// Progress of the copy
        /// </summary>
        private byte progress_;

        /// <summary>
        /// Current moving task
        /// </summary>
        private Task task_;
    }
}
