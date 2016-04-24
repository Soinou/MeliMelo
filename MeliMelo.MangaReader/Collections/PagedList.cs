using MeliMelo.MangaReader.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeliMelo.MangaReader.ViewModels
{
    /// <summary>
    /// Represents a paged list, loading/unloading pages based on a certain index
    /// </summary>
    public class PagedList<T> where T : IVirtualizable
    {
        /// <summary>
        /// Creates a new PagedList
        /// </summary>
        public PagedList()
        {
            items_ = new Dictionary<int, IList<T>>();
            previous_pages_ = null;
        }

        /// <summary>
        /// Gets the items dictionary
        /// </summary>
        public IDictionary<int, IList<T>> Items
        {
            get
            {
                return items_;
            }
        }

        /// <summary>
        /// Adds an item to the list at the given page index
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Item</param>
        public void Add(int index, T item)
        {
            IList<T> page = GetPage(index);

            if (page == null)
            {
                page = new List<T>();
                items_.Add(index, page);
            }

            page.Add(item);
        }

        /// <summary>
        /// Updates the loaded pages to fit with the given index
        /// </summary>
        /// <param name="index">Index</param>
        public void Update(int index)
        {
            if (previous_pages_ == null)
            {
                previous_pages_ = new int[3] { index - 1, index, index + 1 };

                foreach (var page in previous_pages_)
                {
                    LoadPage(page);
                }
            }
            else if (index != previous_pages_[1])
            {
                int[] current_pages = new int[3] { index - 1, index, index + 1 };

                for (int i = 0; i < 3; i++)
                {
                    int new_page = current_pages[i];
                    int previous_page = previous_pages_[i];

                    if (!previous_pages_.Contains(new_page))
                    {
                        LoadPage(new_page);
                    }

                    if (!current_pages.Contains(previous_page))
                    {
                        UnloadPage(previous_page);
                    }
                }

                previous_pages_ = current_pages;
            }
        }

        /// <summary>
        /// Gets the page of slides with the given index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>The page of slides</returns>
        private IList<T> GetPage(int index)
        {
            IList<T> page = null;

            if (items_.TryGetValue(index, out page))
            {
                return page;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the page with the given index
        /// </summary>
        /// <param name="index">Index of the page to load</param>
        private async void LoadPage(int index)
        {
            await Task.Run(() =>
            {
                var page = GetPage(index);

                if (page != null)
                {
                    foreach (var item in page)
                    {
                        item.Load();
                    }
                }
            });
        }

        /// <summary>
        /// Unloads a slide page
        /// </summary>
        /// <param name="index">Index of the page to unload</param>
        private async void UnloadPage(int index)
        {
            await Task.Run(() =>
            {
                var page = GetPage(index);

                if (page != null)
                {
                    foreach (var item in page)
                    {
                        item.Unload();
                    }
                }
            });
        }

        /// <summary>
        /// Dictionary of slides
        /// </summary>
        private IDictionary<int, IList<T>> items_;

        /// <summary>
        /// Previously visible pages
        /// </summary>
        private int[] previous_pages_;
    }
}
