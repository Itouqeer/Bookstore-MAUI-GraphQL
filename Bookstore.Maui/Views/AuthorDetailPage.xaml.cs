using Bookstore.Maui.ViewModels;

namespace Bookstore.Maui.Views;

[QueryProperty(nameof(Author), "Id")]
public partial class AuthorDetailPage : ContentPage
{

    private readonly AuthorDetailViewModel _viewModel;

    public AuthorDetailPage(AuthorDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private int? _authorId;
    public string Author
    {
        set => _authorId = int.TryParse(value, out var id) ? id : null;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        _viewModel.LoadAuthor(_authorId);
    }
}
