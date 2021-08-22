using SQLite.controlador;
using SQLite.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SQLite.ModelViewModel
{
    public class ListView
    {
        crud crud = new crud();
        private ObservableCollection<personas> _persons;

        public ObservableCollection<personas> Persons
        {
            get { return _persons; }
            set { _persons = value; OnPropertyChanged(); }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        private personas _selectedPersona;

        public personas SelectedPersona
        {
            get { return _selectedPersona; }
            set { _selectedPersona = value; OnPropertyChanged(); }
        }

        public ICommand IrInformacionCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public ListView(INavigation navigation)
        {
            Navigation = navigation;
            IrInformacionCommand = new Command<Type>(async (pageType) => await IrInformacion(pageType));

            Persons = new ObservableCollection<personas>();

            mostrar();

        }

        public async void mostrar()
        {
            try
            {
                var personasList = await crud.getReadPersonas();
                foreach (var persons in personasList)
                {
                    Persons.Add(new personas
                    {
                        id = persons.id,
                        name = persons.name,
                        apellido = persons.apellido,
                        direccion = persons.direccion,
                        edad = persons.edad,
                        puesto = persons.puesto,

                    });
                }



            }
            catch (SQLiteException e)
            {


            }
        }

        async Task IrInformacion(Type pageType)
        {
            if (SelectedPersona != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new ViewModel()
                {
                    Id = SelectedPersona.id,
                    Name = SelectedPersona.name,
                    Apellido = SelectedPersona.apellido,
                    Edad = SelectedPersona.edad,
                    Direccion = SelectedPersona.direccion,
                    Puesto = SelectedPersona.puesto


                };

                await Navigation.PushModalAsync(page);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione Persona", "ok");
            }
        }


    }
}


