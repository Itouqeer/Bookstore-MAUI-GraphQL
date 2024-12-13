﻿using Bookstore.Maui.Views;

namespace Bookstore.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BookDetailPage), typeof(BookDetailPage));
            Routing.RegisterRoute(nameof(AuthorDetailPage), typeof(AuthorDetailPage));
        }
    }
}
