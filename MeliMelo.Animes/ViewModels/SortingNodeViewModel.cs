using Caliburn.Micro;
using MeliMelo.Animes.Models;
using MeliMelo.Common.Utils;
using System;

namespace MeliMelo.ViewModels
{
    public interface ISortingNodeViewModelFactory
    {
        SortingNodeViewModel Create(SortingNode node);

        void Release(SortingNodeViewModel view_model);
    }

    public class SortingNodeViewModel : PropertyChangedBase
    {
        public SortingNodeViewModel(SortingNode node)
        {
            node_ = node;
            progress_ = node_.Progress;
            //node_.Started += NodeStarted;
            //node_.Finished += NodeFinished;
            //node_.Changed += NodeChanged;
        }

        public bool IsIndeterminate
        {
            get
            {
                return !node_.Running;
            }
        }

        public byte Progress
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

        protected void NodeChanged(object sender, DataEventArgs<byte> e)
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

        protected SortingNode node_;

        protected byte progress_;
    }
}
