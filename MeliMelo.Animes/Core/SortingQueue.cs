using MeliMelo.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MeliMelo.Animes.Core
{
    public class SortingQueue : IEnumerable<SortNode>
    {
        public SortingQueue()
        {
            nodes_ = new Queue<SortNode>();
        }

        public void Enqueue(SortNode node)
        {
            node.Finished += NodeFinished;
            nodes_.Enqueue(node);
            if (NodeAdded != null)
                NodeAdded(this, new DataEventArgs<SortNode>(node));
        }

        public IEnumerator<SortNode> GetEnumerator()
        {
            return nodes_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes_.GetEnumerator();
        }

        public void Run()
        {
            // We don't have a running node
            if (current_node_ == null && nodes_.Count > 0)
            {
                // Get the node and start to move it
                current_node_ = nodes_.Peek();
                current_node_.Move();
            }
        }

        /// <summary>
        /// Triggered when a node is added to the queue
        /// </summary>
        public event EventHandler<DataEventArgs<SortNode>> NodeAdded;

        /// <summary>
        /// Called when the currently sorting node is finished
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void NodeFinished(object sender, EventArgs e)
        {
            current_node_ = null;
            nodes_.Dequeue();
        }

        /// <summary>
        /// Current moving node
        /// </summary>
        protected SortNode current_node_;

        /// <summary>
        /// Queue of nodes to sort
        /// </summary>
        protected Queue<SortNode> nodes_;
    }
}
