using Caliburn.Micro;
using MeliMelo.Animes.Repositories;
using MeliMelo.Animes.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public class MainViewModel : Conductor<LibrarySorterViewModel>
    {
        /// <summary>
        /// Creates a new MainViewModel
        /// </summary>
        public MainViewModel(DialogManager dialog, ISortingNodeViewModelFactory node_factory,
            ILibrarySorterViewModelFactory sorter_factory, LibrarySorterRepository repository)
        {
            DisplayName = "MeliMelo - Animes";

            dialog_ = dialog;
            node_factory_ = node_factory;
            selected_sorter_ = null;
            sorter_factory_ = sorter_factory;
            sorters_ = new List<LibrarySorterViewModel>();

            foreach (var sorter in repository.Items)
            {
                sorters_.Add(sorter_factory_.Create(sorter, node_factory));
            }
        }

        public bool CanDelete
        {
            get
            {
                return false;
            }
        }

        public bool CanEdit
        {
            get
            {
                return false;
            }
        }

        public ICollectionView Libraries
        {
            get
            {
                var view = CollectionViewSource.GetDefaultView(sorters_);

                view.SortDescriptions.Add(new SortDescription("LibraryName",
                    ListSortDirection.Ascending));

                return view;
            }
        }

        public LibrarySorterViewModel SelectedLibrary
        {
            get
            {
                return selected_sorter_;
            }
            set
            {
                if (selected_sorter_ != value)
                {
                    selected_sorter_ = value;
                    NotifyOfPropertyChange(() => SelectedLibrary);
                    ActivateItem(selected_sorter_);
                }
            }
        }

        public void Add()
        {
            var sorter = dialog_.AddLibrary();

            if (sorter != null)
            {
                sorters_.Add(sorter_factory_.Create(sorter, node_factory_));

                NotifyOfPropertyChange(() => Libraries);
            }
        }

        public void Delete()
        {
        }

        public void Edit()
        {
        }

        protected LibrarySorterViewModel selected_sorter_;
        protected List<LibrarySorterViewModel> sorters_;
        private DialogManager dialog_;
        private ISortingNodeViewModelFactory node_factory_;
        private ILibrarySorterViewModelFactory sorter_factory_;
    }
}
