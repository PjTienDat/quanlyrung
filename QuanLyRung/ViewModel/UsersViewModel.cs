using Microsoft.Xaml.Behaviors.Core;
using QuanLyRung.Model;
using QuanLyRung.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{

    public class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<Users> _List;
        public ObservableCollection<Users> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Users _SelectedItem;
        public Users SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {



                    DisplayName = SelectedItem.DisplayName;
                    UserName = SelectedItem.UserName;
                    Password = SelectedItem.Password;
                  




                }
            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        



        public UsersViewModel()
        {
            List = new ObservableCollection<Users>(DataProvider.Ins.DB.Users);

            AddCommand = new RelayCommand<object>((p) =>
            {

                return true;

            }, (p) => {
                {
                    var users = new Users() { DisplayName = DisplayName, UserName = UserName, Password = Password };

                    DataProvider.Ins.DB.Users.Add(users);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(users);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null;
            }, (p) =>
            {
                var userToDelete = DataProvider.Ins.DB.Users.FirstOrDefault(u => u.Id == SelectedItem.Id);
                if (userToDelete != null)
                {
                    DataProvider.Ins.DB.Users.Remove(userToDelete);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                }
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Users.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var users = DataProvider.Ins.DB.Users.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                users.DisplayName = DisplayName;
                users.UserName = UserName;
                users.Password = Password;
               
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;

            });
        }
    }
}
