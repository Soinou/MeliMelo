using Caliburn.Micro;
using MeliMelo.Animes.Collections;
using MeliMelo.Animes.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public interface ILibrarySorterViewModelFactory
    {
        LibrarySorterViewModel Create(LibrarySorter sorter,
            ISortingNodeViewModelFactory node_factory);

        void Release(LibrarySorterViewModel view_model);
    }

    public class LibrarySorterViewModel : Screen
    {
        public LibrarySorterViewModel(LibrarySorter sorter,
            ISortingNodeViewModelFactory node_factory)
        {
            node_factory_ = node_factory;
            nodes_ = new List<SortingNodeViewModel>();
            sorter_ = sorter;
            sorter_.Queue.NodeAdded += OnQueueNodeAdded;
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

        public void Run()
        {
            sorter_.Scan();
        }

        private void OnQueueNodeAdded(SortingNode data)
        {
            var node = node_factory_.Create(data);
            node.Finished += () =>
            {
                nodes_.Remove(node);
                NotifyOfPropertyChange(() => Nodes);
            };
            nodes_.Add(node);
            NotifyOfPropertyChange(() => Nodes);
        }

        private ISortingNodeViewModelFactory node_factory_;
        private List<SortingNodeViewModel> nodes_;
        private LibrarySorter sorter_;
    }
}
