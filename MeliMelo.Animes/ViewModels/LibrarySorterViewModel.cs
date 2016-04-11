using Caliburn.Micro;
using MeliMelo.Animes.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public interface ILibrarySorterViewModelFactory
    {
        LibrarySorterViewModel Create(LibrarySorter sorter);

        void Release(LibrarySorterViewModel view_model);
    }

    public class LibrarySorterViewModel : Screen
    {
        public LibrarySorterViewModel(LibrarySorter sorter)
        {
            sorter_ = sorter;
            nodes_ = new List<SortingNodeViewModel>();
        }

        public bool CanStart
        {
            get
            {
                return !sorter_.Monitoring;
            }
        }

        public bool CanStop
        {
            get
            {
                return sorter_.Monitoring;
            }
        }

        public string LibraryName
        {
            get
            {
                return sorter_.Name;
            }
        }

        public ICollectionView Nodes
        {
            get
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(nodes_);

                view.SortDescriptions.Add(new SortDescription("Running",
                    ListSortDirection.Descending));
                view.SortDescriptions.Add(new SortDescription("Title",
                    ListSortDirection.Ascending));

                return view;
            }
        }

        public void Start()
        {
            sorter_.Start();
            NotifyOfPropertyChange(() => CanStart);
            NotifyOfPropertyChange(() => CanStop);
        }

        public void Stop()
        {
            sorter_.Stop();
            NotifyOfPropertyChange(() => CanStart);
            NotifyOfPropertyChange(() => CanStop);
        }

        protected List<SortingNodeViewModel> nodes_;
        protected LibrarySorter sorter_;
    }
}
