using Bookstore.Maui.ViewModels;

namespace Bookstore.Maui.Views
{
    [QueryProperty(nameof(BookId), "Id")]
    public partial class BookDetailPage : ContentPage
    {
        private readonly BookDetailViewModel _viewModel;

        public BookDetailPage(BookDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private int? _bookId;
        public string BookId
        {
            set => _bookId = int.TryParse(value, out var id) ? id : null;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            _viewModel.LoadBook(_bookId);
        }
    }
}
