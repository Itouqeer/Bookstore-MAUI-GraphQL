using Bookstore.Maui.Models;
using Bookstore.Maui.ViewModels;

namespace Bookstore.Maui.Views;

[QueryProperty(nameof(UpdatedAuthorId), "Id")]
[QueryProperty(nameof(UpdatedAuthorName), "Name")]
[QueryProperty(nameof(UpdatedAuthorBio), "Bio")]
public partial class AuthorsPage : ContentPage
{
    public string UpdatedAuthorId { get; set; }
    public string UpdatedAuthorName { get; set; }
    public string UpdatedAuthorBio { get; set; }

    private AuthorsViewModel _viewModel;
    public AuthorsPage(AuthorsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (int.TryParse(UpdatedAuthorId, out var id))
        {
            _viewModel.UpdateAuthorInList(new Author
            {
                Id = id,
                Name = UpdatedAuthorName,
                Bio = UpdatedAuthorBio
            });
        }
    }
}