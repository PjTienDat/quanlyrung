using QuanLyRung.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand TreeCommand { get; set; }
        public ICommand WoodCommand { get; set; }
        public ICommand SupplierTreeCommand { get; set; }
        public ICommand SupplierWoodCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand AnimalCommand { get; set; }
        public ICommand StorageAnimalCommand { get; set; }


        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {

                    p.Show();
                }
                else
                {
                    p.Close();
                }

            }
              );

            TreeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { TreeWindow wd = new TreeWindow(); wd.ShowDialog(); });
            WoodCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WoodWindow wd = new WoodWindow(); wd.ShowDialog(); });
            SupplierTreeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SupplierTreeWindow wd = new SupplierTreeWindow(); wd.ShowDialog(); });
            SupplierWoodCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SupplierWoodWindow wd = new SupplierWoodWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            AnimalCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AnimalWindow wd = new AnimalWindow(); wd.ShowDialog(); });
            StorageAnimalCommand = new RelayCommand<object>((p) => { return true; }, (p) => { StorageAnimalWindow wd = new StorageAnimalWindow(); wd.ShowDialog(); });


            
        }
    }
}
