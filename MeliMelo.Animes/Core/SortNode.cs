using MeliMelo.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MeliMelo.Animes.Core
{
    public class SortNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anime">the anime</param>
        /// <param name="source">the source</param>
        /// <param name="destination">the destination</param>
        public SortNode(Anime anime, string source, string destination)
        {
            anime_ = anime;
            destination_ = destination;
            progress_ = 0;
            running_ = false;
            source_ = source;
        }

        /// <summary>
        /// Destination property
        /// </summary>
        public string Destination
        {
            get
            {
                return destination_;
            }
        }

        /// <summary>
        /// Anime episode property
        /// </summary>
        public string Episode
        {
            get
            {
                return anime_.EpisodeNumber();
            }
        }

        /// <summary>
        /// Gets the file copy progress
        /// </summary>
        public int Progress
        {
            get
            {
                return progress_;
            }
        }

        /// <summary>
        /// Gets if the node is currently moving or not
        /// </summary>
        public bool Running
        {
            get
            {
                return running_;
            }
        }

        /// <summary>
        /// Source property
        /// </summary>
        public string Source
        {
            get
            {
                return source_;
            }
        }

        /// <summary>
        /// Anime title property
        /// </summary>
        public string Title
        {
            get
            {
                return anime_.AnimeTitle();
            }
        }

        public void Copy()
        {
            FileInfo source = new FileInfo(Source);
            FileInfo destination = new FileInfo(Destination);

            byte[][] buffers =
            {
                new byte[kBufferSize],
                new byte[kBufferSize]
            };

            bool swap = false;
            int progress = 0, read = 0;
            long len = source.Length;
            float flen = len;
            Task task = null;

            using (var reader = source.OpenRead())
            using (var writer = destination.OpenWrite())
            {
                for (long size = 0; size < len; size += read)
                {
                    progress = (int)((size / flen) * 100);
                    if (progress != progress_)
                    {
                        progress_ = progress;
                        if (Changed != null)
                            Changed(this, new DataEventArgs<int>(progress_));
                    }

                    read = reader.Read(swap ? buffers[0] : buffers[1], 0, kBufferSize);

                    if (task != null)
                        task.Wait();

                    task = writer.WriteAsync(swap ? buffers[0] : buffers[1], 0, read);

                    swap = !swap;
                }

                writer.Write(swap ? buffers[1] : buffers[0], 0, read);
            }
        }

        /// <summary>
        /// Moves the file
        /// </summary>
        public async void Move()
        {
            await Task.Run(() =>
            {
                running_ = true;

                if (Started != null)
                    Started(this, new EventArgs());

                try
                {
                    // First, delete destination if there is one
                    if (File.Exists(destination_))
                        File.Delete(destination_);

                    // Then copy the source to the destination
                    Copy();

                    // And finally delete the source
                    File.Delete(source_);
                }
                catch (Exception)
                {
                    // Should send a false to the view or something like this
                }

                running_ = false;

                if (Finished != null)
                    Finished(this, new EventArgs());
            });
        }

        /// <summary>
        /// Triggered when the copy progress has changed
        /// </summary>
        public event EventHandler<DataEventArgs<int>> Changed;

        /// <summary>
        /// Triggered when the node has finished moving
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Triggered when the node has started moving
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Copy buffer size
        /// </summary>
        protected const int kBufferSize = 1024 * 1024;

        /// <summary>
        /// The recognized anime
        /// </summary>
        protected Anime anime_;

        /// <summary>
        /// The node destination
        /// </summary>
        protected string destination_;

        /// <summary>
        /// Progress of the copy
        /// </summary>
        protected int progress_;

        /// <summary>
        /// Status of the node
        /// </summary>
        protected bool running_;

        /// <summary>
        /// The node source
        /// </summary>
        protected string source_;
    }
}
