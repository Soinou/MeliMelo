using MeliMelo.Common.Utils;
using System.Collections;
using System.Collections.Generic;

namespace MeliMelo.Animes.Collections
{
    /// <summary>
    /// Represents a queue used to sort sorting nodes
    /// </summary>
    public class SortingQueue : IEnumerable<SortingNode>
    {
        /// <summary>
        /// Creates a new SortingQueue
        /// </summary>
        public SortingQueue()
        {
            current_node_ = null;
            nodes_ = new Queue<SortingNode>();
        }

        /// <summary>
        /// Clears all the nodes, then waits for the eventual current node to finish
        /// </summary>
        public void Clear()
        {
            // Clear all the nodes
            nodes_.Clear();

            // Wait for the eventual current node
            if (current_node_ != null)
            {
                current_node_.Wait();
            }
        }

        /// <summary>
        /// Enqueues a node on this queue
        /// </summary>
        /// <param name="node">Node to enqueue</param>
        public void Enqueue(SortingNode node)
        {
            // Enqueue the node
            nodes_.Enqueue(node);

            // Trigger some events
            if (NodeAdded != null)
            {
                NodeAdded(node);
            }

            // If we don't have a node currently moving
            if (current_node_ == null && nodes_.Count > 0)
            {
                // Dequeue one
                Dequeue();
            }
        }

        /// <inheritdoc />
        public IEnumerator<SortingNode> GetEnumerator()
        {
            return nodes_.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes_.GetEnumerator();
        }

        /// <summary>
        /// Triggered when a node is added to the queue
        /// </summary>
        public event DataEventHandler<SortingNode> NodeAdded;

        /// <summary>
        /// Dequeues the front node and moves it
        /// </summary>
        private void Dequeue()
        {
            // First get the front node
            current_node_ = nodes_.Peek();

            // Place a handler on the Finished event
            current_node_.Finished += () =>
            {
                // Clear the current node
                current_node_ = null;

                // Dequeue this node
                nodes_.Dequeue();

                // If we still have some nodes
                if (nodes_.Count > 0)
                {
                    // Start another dequeue
                    Dequeue();
                }
            };

            // Then move the node
            current_node_.Move();
        }

        /// <summary>
        /// Current moving node
        /// </summary>
        private SortingNode current_node_;

        /// <summary>
        /// Queue of nodes to sort
        /// </summary>
        private Queue<SortingNode> nodes_;
    }
}
