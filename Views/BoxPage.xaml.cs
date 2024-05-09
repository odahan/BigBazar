#nullable disable
using BigBazar.Controls;
using BigBazar.Messages;
using BigBazar.Models;
using BigBazar.Services;
using BigBazar.ViewModels;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;

namespace BigBazar.Views;

public partial class BoxPage : BasePage
{
    public BoxPage(IViewModelFactory viewModelFactory, IDialogService dialog)
    {
        InitializeComponent();
        dialogService = dialog;
        BindingContext = viewModelFactory.Create<BoxPageViewModel>(); 

        WeakReferenceMessenger.Default.Register<BoxPageRebuildCatListMessage>(this, OnRebuildCatListMessageReceived);
    }
    

    private IDialogService dialogService;

    // Event handler for the RebuildCatListMessage
    private void OnRebuildCatListMessageReceived(object recipient, BoxPageRebuildCatListMessage message)
    {
        Guard.IsNotNull(message);
        Guard.IsAssignableToType(message.Tag, typeof(List<BoxCatForDisplay>));
        var boxCats = (List<BoxCatForDisplay>)message.Tag;
        flexLayout.Children.Clear();
        foreach (var boxCat in boxCats)
        {
            var pastilleLabel = new PastilleLabel
            {
                LabelText = boxCat.CatName,
                PayloadIconKind = PayloadKind.Delete,
                Payload = boxCat.IdBoxCat
            };
            flexLayout.Children.Add(pastilleLabel);
        }
        var pastilleLabelAdd = new PastilleLabel
        {
            LabelText = "Add",
            PayloadIconKind = PayloadKind.Add,
            BackColorPastille = Colors.Red
        };
        flexLayout.Children.Add(pastilleLabelAdd);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        ((BoxPageViewModel)BindingContext).IsModified = false;
    }

    
}