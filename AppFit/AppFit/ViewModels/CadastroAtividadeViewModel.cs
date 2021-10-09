using AppFit.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFit.ViewModels
{
    [QueryProperty("AtividadeId", "AtividadeId")]
    class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int _id;
        string _descricao;
        string _observacoes;
        DateTime _data;
        double? _peso;

        public string AtividadeId {
            set 
            {
                int.TryParse(value, out int id);

                CarregarAtividade.Execute(id);
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Descricao"));
            }
        }
        public string Observacoes
        {
            get => _observacoes;
            set
            {
                _observacoes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Observacoes"));
            }
        }
        public DateTime Data
        {
            get => _data; set
            {
                _data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }
        public double? Peso
        {
            get => _peso;
            set
            {
                _peso = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
            }
        }

        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = 0;
                Descricao = string.Empty;
                Data = DateTime.Now;
                Peso = null;
                Observacoes = string.Empty;
            });
        }

        public ICommand CarregarAtividade
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    var atividade = await App.Database.GetById(id);

                    if(atividade != null)
                    {
                        Id = atividade.Id;
                        Descricao = atividade.Descricao;
                        Data = atividade.Data;
                        Peso = atividade.Peso;
                        Observacoes = atividade.Observacoes;
                    }
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possivel carregar as atividades cadastradas", "Ok");
                }
            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {
                try
                {
                    Atividade model = new Atividade()
                    {
                        Id = Id,
                        Descricao = Descricao,
                        Data = Data,
                        Peso = Peso,
                        Observacoes = Observacoes
                    };

                    if(model.Id == 0)
                    {
                        await App.Database.Insert(model);
                        await Application.Current.MainPage.DisplayAlert("Sucesso!", "A atividade foi cadastrada", "Ok");
                    }
                    else
                    {
                        await App.Database.Update(model);
                        await Application.Current.MainPage.DisplayAlert("Sucesso!", "A atividade foi atualizada", "Ok");
                    }

                    await Shell.Current.GoToAsync("//MinhasAtividades");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possivel salvar a atividade", "Ok");
                }
            });
        }
    }
}
