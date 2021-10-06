using AppFit.Helpers;
using System;
using System.IO;
using Xamarin.Forms;

namespace AppFit
{
    public partial class App : Application
    {
        private static SQLiteDatabaseHelper database;

        internal static SQLiteDatabaseHelper Database { 
            get {
                if(database is null)
                {
                    database = new SQLiteDatabaseHelper(
                        Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData
                        ), "XamAppFit.db3")
                    );
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
