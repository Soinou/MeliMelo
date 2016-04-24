using Caliburn.Micro;
using MeliMelo.Animes.Collections;
using MeliMelo.Common.Utils;

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

        public event DataEventHandler Finished;

        private void NodeChanged(byte e)
        {
            Progress = e;
        }

        private void NodeFinished()
        {
            if (Finished != null)
                Finished();
        }

        private void NodeStarted()
        {
            NotifyOfPropertyChange(() => IsIndeterminate);
            NotifyOfPropertyChange(() => Progress);
        }

        private SortingNode node_;

        private byte progress_;
    }
}
