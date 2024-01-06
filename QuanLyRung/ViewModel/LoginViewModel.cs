using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using QuanLyRung.Model;
using System.Windows.Controls;
using System.Security.Cryptography;

namespace QuanLyRung.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin {get; set;}
        private string _Username;
        public string UserName { get=>_Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

        }
        void Login(Window p)
        {
            if (p == null)
                return;

           
            
            var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == _Username && x.Password == _Password).Count();
            
            if (accCount > 0)
            {
                IsLogin = true;

                p.Close();
            }
            else
            {
               IsLogin = false;
                MessageBox.Show("Bạn nhập sai tài khoản hoặc mật khẩu!");

            }
        }
    }
}