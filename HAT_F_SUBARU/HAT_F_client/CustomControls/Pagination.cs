using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templateApp.Controls
{
    public partial class Pagination : UserControl
    {
        public event EventHandler<int> onPageChange;

        private int currentPage = 1;  // Initially, set the current page to 1.
        private int totalPages;

        public int TotalPages { 
            set {
                totalPages = value;
                UpdatePageIndicator();
            } 
            get { return totalPages; }
        }
        public int CurrentPage
        {
            set {  currentPage = value;
                UpdatePageIndicator();
            }
            get { return currentPage; }
        }

        private void UpdatePageIndicator()
        {
            // Replace "lblPageIndicator" with the name of your Label control.
            lblPageIndicator.Text = $"ページ {currentPage}/{totalPages}";
        }

        public Pagination()
        {
            InitializeComponent();
            UpdatePageIndicator();

            // Subscribe to button click events.
            btnFirstPage.Click += (sender, e) => GoToFirstPage();
            btnPreviousPage.Click += (sender, e) => GoToPreviousPage();
            btnNextPage.Click += (sender, e) => GoToNextPage();
            btnLastPage.Click += (sender, e) => GoToLastPage();
        }
        public Pagination(int totalPages = 1)
        {
            InitializeComponent();
            this.totalPages = totalPages;
            UpdatePageIndicator();

            // Subscribe to button click events.
            btnFirstPage.Click += (sender, e) => GoToFirstPage();
            btnPreviousPage.Click += (sender, e) => GoToPreviousPage();
            btnNextPage.Click += (sender, e) => GoToNextPage();
            btnLastPage.Click += (sender, e) => GoToLastPage();
        }


        private void GoToFirstPage()
        {
            currentPage = 1;
            UpdatePageIndicator();
            onPageChange?.Invoke(this, currentPage);
        }

        private void GoToPreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePageIndicator();
                onPageChange?.Invoke(this, currentPage);
            }
        }

        private void GoToNextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdatePageIndicator();
                onPageChange?.Invoke(this, currentPage);
            }
        }

        private void GoToLastPage()
        {
            currentPage = totalPages;
            UpdatePageIndicator();
            onPageChange?.Invoke(this, currentPage);
        }

        public void SetPagingInfo(int currentPage, int totalRowsCount, int pageRowsCount)
        {
            this.currentPage = currentPage;
            this.totalPages = (totalRowsCount / pageRowsCount) + (totalRowsCount % pageRowsCount > 0 ? 1 : 0);
            UpdatePageIndicator();
        }
    }
}
