using AppFit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaAtividades : ContentPage
    {
        public ListaAtividades()
        {
            InitializeComponent();

            BindingContext = new ListaAtividadesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as ListaAtividadesViewModel;
            viewModel.AtualizarLista.Execute(null);
        }
    }
}