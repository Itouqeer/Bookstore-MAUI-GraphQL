using Bookstore.Maui.Models;
using Bookstore.Maui.ViewModels;

namespace Bookstore.Maui.Views;

[QueryProperty(nameof(UpdatedBookId), "Id")]
[QueryProperty(nameof(UpdatedBookTitle), "Title")]
[QueryProperty(nameof(UpdatedBookAuthorId), "AuthorId")]
[QueryProperty(nameof(UpdatedBookAuthorName), "AuthorName")]
[QueryProperty(nameof(UpdatedBookAuthorBio), "AuthorBio")]
public partial class BooksPage : ContentPage
{
    public string UpdatedBookId { get; set; }
    public string UpdatedBookTitle { get; set; }
    public string UpdatedBookAuthorId { get; set; }
    public string UpdatedBookAuthorName { get; set; }
    public string UpdatedBookAuthorBio { get; set; }

    private BooksViewModel _viewModel;
    public BooksPage(BooksViewModel bookViewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = bookViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (int.TryParse(UpdatedBookId, out var id) && int.TryParse(UpdatedBookAuthorId, out var authorId))
        {
            _viewModel.UpdateBookInList(new Book
            {
                Id = id,
                Title = UpdatedBookTitle,
                Author = new Author
                {
                    Id = authorId,
                    Name = UpdatedBookAuthorName,
                    Bio = UpdatedBookAuthorBio
                }
            });
        }
    }
}