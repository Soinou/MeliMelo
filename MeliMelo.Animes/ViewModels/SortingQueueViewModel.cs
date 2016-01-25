using Caliburn.Micro;
using MeliMelo.Animes.Core;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public class SortingQueueViewModel : Screen
    {
        /// <summary>
        /// Creates a new SortingQueueViewModel
        /// </summary>
        /// <param name="queue">Sorting queue to display</param>
        public SortingQueueViewModel(SortingQueue queue)
        {
            DisplayName = "MeliMelo - Animes";

            queue_ = queue;
            nodes_ = new BindableCollection<SortNodeViewModel>();

            foreach (var node in queue_)
                AddNode(node);

            queue_.NodeAdded += QueueNodeAdded;
        }

        /// <summary>
        /// Gets the list of nodes
        /// </summary>
        public ICollectionView Nodes
        {
            get
            {
                var nodes = CollectionViewSource.GetDefaultView(nodes_);

                nodes.SortDescriptions.Add(new SortDescription("Running",
                    ListSortDirection.Descending));
                nodes.SortDescriptions.Add(new SortDescription("Title",
                    ListSortDirection.Ascending));

                return nodes;
            }
        }

        /// <summary>
        /// Adds a node to the current list of nodes
        /// </summary>
        /// <param name="node"></param>
        protected void AddNode(SortNode node)
        {
            SortNodeViewModel view_model = new SortNodeViewModel(node);

            node.Started += (object sender, EventArgs args) =>
            {
                NotifyOfPropertyChange(() => Nodes);
            };

            view_model.Finished += (object sender, EventArgs args) =>
            {
                nodes_.Remove(view_model);
                NotifyOfPropertyChange(() => Nodes);
            };

            nodes_.Add(view_model);
        }

        /// <summary>
        /// Called when a node is added to the queue
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void QueueNodeAdded(object sender, Utils.DataEventArgs<SortNode> e)
        {
            AddNode(e.Data);
            NotifyOfPropertyChange(() => Nodes);
        }

        /// <summary>
        /// List of nodes
        /// </summary>
        protected IObservableCollection<SortNodeViewModel> nodes_;

        /// <summary>
        /// Sorting queue
        /// </summary>
        protected SortingQueue queue_;
    }
}
