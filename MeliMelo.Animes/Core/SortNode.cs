using MeliMelo.Utils;
using System;
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
        public byte Progress
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
                    // Then move the source to the destination
                    IoHelper.Move(source_, destination_, (byte progress) =>
                    {
                        progress_ = progress;

                        if (Changed != null)
                            Changed(this, new DataEventArgs<byte>(progress_));
                    });
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
        public event EventHandler<DataEventArgs<byte>> Changed;

        /// <summary>
        /// Triggered when the node has finished moving
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Triggered when the node has started moving
        /// </summary>
        public event EventHandler Started;

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
        protected byte progress_;

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
