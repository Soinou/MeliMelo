using Caliburn.Micro;
using MeliMelo.Animes.Core;
using System;

namespace MeliMelo.ViewModels
{
    public class SortNodeViewModel : PropertyChangedBase
    {
        public SortNodeViewModel(SortNode node)
        {
            node_ = node;
            progress_ = node_.Progress;
            node_.Started += NodeStarted;
            node_.Finished += NodeFinished;
            node_.Changed += NodeChanged;
        }

        public bool IsIndeterminate
        {
            get
            {
                return !node_.Running;
            }
        }

        public int Progress
        {
            get
            {
                return progress_;
            }
            set
            {
                if (progress_ != value)
                {
                    progress_ = value;
                    NotifyOfPropertyChange(() => Progress);
                }
            }
        }

        public bool Running
        {
            get
            {
                return node_.Running;
            }
        }

        public string Title
        {
            get
            {
                return node_.Title + " - Episode " + node_.Episode;
            }
        }

        public event EventHandler Finished;

        protected void NodeChanged(object sender, Utils.DataEventArgs<int> e)
        {
            Progress = e.Data;
        }

        protected void NodeFinished(object sender, EventArgs e)
        {
            if (Finished != null)
                Finished(this, new EventArgs());
        }

        protected void NodeStarted(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => IsIndeterminate);
            NotifyOfPropertyChange(() => Progress);
        }

        protected SortNode node_;

        protected int progress_;
    }
}
