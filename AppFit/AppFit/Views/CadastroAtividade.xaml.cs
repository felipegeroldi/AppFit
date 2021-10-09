using AppFit.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroAtividade : ContentPage
    {
        public CadastroAtividade()
        {
            InitializeComponent();

            BindingContext = new CadastroAtividadeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as CadastroAtividadeViewModel;

            if(viewModel.Id == 0)
            {
                viewModel.Data = DateTime.Now;
            }
        }
    }
}