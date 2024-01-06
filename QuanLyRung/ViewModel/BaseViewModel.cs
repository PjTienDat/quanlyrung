using QuanLyRung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
public class DataService
{
    private static DataService _instance;

    public static DataService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataService();
            }
            return _instance;
        }
    }

    public ObservableCollection<SupplierTree> SupplierTreeList { get; set; }

    private DataService()
    {
        SupplierTreeList = new ObservableCollection<SupplierTree>(DataProvider.Ins.DB.SupplierTree);
    }
}
public class DataServices
{
    private static DataServices _instances;

    public static DataServices Instances
    {
        get
        {
            if (_instances == null)
            {
                _instances = new DataServices();
            }
            return _instances;
        }
    }

    public ObservableCollection<SupplierWood> SupplierWoodList { get; set; }

    private DataServices()
    {
        SupplierWoodList = new ObservableCollection<SupplierWood>(DataProvider.Ins.DB.SupplierWood);
    }
}
public class DataServicess
{
    private static DataServicess _instancess;

    public static DataServicess Instancess
    {
        get
        {
            if (_instancess == null)
            {
                _instancess = new DataServicess();
            }
            return _instancess;
        }
    }

    public ObservableCollection<StorageAnimal> StorageAnimalList { get; set; }

    private DataServicess()
    {
        StorageAnimalList = new ObservableCollection<StorageAnimal>(DataProvider.Ins.DB.StorageAnimal);
    }
}