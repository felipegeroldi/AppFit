using AppFit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFit.ViewModels
{
    class ListaAtividadesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Atividade> _listaAtividades = new ObservableCollection<Atividade>();
        bool estaAtualizando = false;

        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }


        public ObservableCollection<Atividade> ListaAtividades
        {
            get => _listaAtividades;
            set => _listaAtividades = value;
        }

        public string ParametroBusca { get; set; }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if(!EstaAtualizando)
                        {
                            EstaAtualizando = true;
                            var listaAtividades = await App.Database.GetAllRows();

                            ListaAtividades.Clear();

                            listaAtividades
                                .OrderBy(item => item.Data)
                                .ToList()
                                .ForEach(item => ListaAtividades.Add(item));
                        }
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possivel carregar as atividades cadastradas", "Ok");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }

        public ICommand Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (!EstaAtualizando)
                        {
                            EstaAtualizando = true;
                            var listaAtividades = await App.Database.Search(ParametroBusca);

                            ListaAtividades.Clear();

                            listaAtividades
                                .OrderBy(item => item.Data)
                                .ToList()
                                .ForEach(item => ListaAtividades.Add(item));
                        }
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possivel carregar as atividades cadastradas", "Ok");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }

        public ICommand ExibirAtividade
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroAtividade?AtividadeId={id}");
                });
            }
        }

        public ICommand RemoverAtividade
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        bool confirmado = await Application.Current.MainPage
                            .DisplayAlert(
                                "Excluir Atividade", "Tem certeza que deseja excluir eesas ativiade?", "Sim", "Não"
                            );

                        if (confirmado)
                        {
                            await App.Database.Delete(id);
                            AtualizarLista.Execute(null);
                        }
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possivel carregar as atividades cadastradas", "Ok");
                    }
                });
            }
        }
    }
}
