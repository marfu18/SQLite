using SQLite.controlador;
using SQLite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SQLite.ModelViewModel
{
    public class ViewModel
    {
        conexion conn = new conexion();
        crud crud = new crud();
        private int id;
        private string _name;
        private string _apellido;
        private double _edad;
        private string _direccion;
        private string _puesto;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; OnPropertyChanged(); }
        }
        public double Edad
        {
            get { return _edad; }
            set { _edad = value; OnPropertyChanged(); }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; OnPropertyChanged(); }
        }
        public string Puesto
        {
            get { return _puesto; }
            set { _puesto = value; OnPropertyChanged(); }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        public async void Guardar()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Campo de nombre vacio", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Campo de Apellido vacio", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(Edad.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Campo de edad vacio", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Direccion))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Campo de direccion vacio", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Puesto))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Campo de puesto vacio", "Ok");
                return;
            }
            var personas = new personas()
            {
                id = Id,
                name = Name,
                apellido = Apellido,
                edad = Edad,
                direccion = Direccion,
                puesto = Puesto

            };

            try
            {


                conn.Conn().CreateTable<personas>();
                conn.Conn().Insert(personas);
                conn.Conn().Close();

                await App.Current.MainPage.DisplayAlert("Exito", "Datos Guardados", "Ok");
                await App.Current.MainPage.Navigation.PushAsync(new inicio());


            }
            catch (SQLiteException)
            {

            }
        }

        public ICommand ClearCommand { private set; get; }
        public ICommand SendEmailCommand { private set; get; }

        public ViewModel()
        {
            ClearCommand = new Command(() => Clear());

        }

        public ICommand GuardarCommand
        {
            get { return new Command(() => Guardar()); }
        }
        public ICommand DeleteCommand { get { return new Command(() => eliminar()); } }
        public ICommand UpdateCommand { get { return new Command(() => actualizar()); } }
        void Clear()
        {

            Name = string.Empty;
            Apellido = string.Empty;
            Edad = 0;
            Direccion = string.Empty;
            Puesto = string.Empty;


        }
        async void eliminar()
        {
            var persona = await crud.getId(Convert.ToInt32(Id));
            bool conf = await App.Current.MainPage.DisplayAlert("Delete", "Eliminar Persona", "Aceptar", "Cancelar");
            if (conf)
            {
                if (persona != null)
                {
                    await crud.Delete(persona);
                    await App.Current.MainPage.DisplayAlert("Delete", "Datos Eliminados", "Aceptar");
                    Clear();
                    await App.Current.MainPage.Navigation.PopModalAsync();

                }
            }

        }
        async void actualizar()
        {

            bool conf = await App.Current.MainPage.DisplayAlert("Update", "Actualizar datos", "Acceptar", "Cancelar");
            if (conf)
            {
                if (!string.IsNullOrEmpty(Id.ToString()))
                {
                    personas update = new personas
                    {
                        id = Convert.ToInt32(Id.ToString()),
                        name = Name,
                        apellido = Apellido,
                        edad = Convert.ToDouble(Edad),
                        direccion = Direccion,
                        puesto = Puesto
                    };
                    await crud.getUpdateId(update);
                    await App.Current.MainPage.DisplayAlert("Update", "Datos Actualizados", "Aceptar");
                    await App.Current.MainPage.Navigation.PopModalAsync();

                }
            }

        }


    }
}
