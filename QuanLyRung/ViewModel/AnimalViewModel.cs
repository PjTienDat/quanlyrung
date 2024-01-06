using QuanLyRung.Model;
using QuanLyRung.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{
    public class AnimalViewModel : BaseViewModel
    {
        private ObservableCollection<Animal> _List;
        public ObservableCollection<Animal> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<StorageAnimal> _StorageAnimal;
        public ObservableCollection<StorageAnimal> StorageAnimal { get => _StorageAnimal; set { _StorageAnimal = value; OnPropertyChanged(); } }
        private ObservableCollection<Animal> _FilteredAnimalList;
        public ObservableCollection<Animal> FilteredAnimalList
        {
            get => _FilteredAnimalList;
            set { _FilteredAnimalList = value; OnPropertyChanged(); }
        }

        private Animal _SelectedItem;
        public Animal SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {

                    DisplayName = SelectedItem.DisplayName;
                    Count = SelectedItem.Count;
                    StatusAnimal = SelectedItem.StatusAnimal;
                    DateInput = SelectedItem.DateInput;
                    SelectedStorageAnimal = SelectedItem.StorageAnimal;
                    


                }
            }
        }
        private StorageAnimal _SelectedStorageAnimal;
        public StorageAnimal SelectedStorageAnimal
        {
            get => _SelectedStorageAnimal;
            set
            {
                _SelectedStorageAnimal = value;
                OnPropertyChanged();

            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        private int? _Count;
        public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
      
        private string _StatusAnimal;
        public string StatusAnimal { get => _StatusAnimal; set { _StatusAnimal = value; OnPropertyChanged(); } }

        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged(); } }

        private string _SearchTerm;
        public string SearchTerm
        {
            get => _SearchTerm;
            set
            {
                _SearchTerm = value;
                OnPropertyChanged();
                FilterAnimal();


            }
        }


        private bool _showSearchResults;
        public bool ShowSearchResults
        {
            get => _showSearchResults;
            set
            {
                _showSearchResults = value;
                OnPropertyChanged();
            }
        }



        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }





        public AnimalViewModel()
        {
            List = new ObservableCollection<Animal>(DataProvider.Ins.DB.Animal);
            StorageAnimal = DataServicess.Instancess.StorageAnimalList;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedStorageAnimal == null)
                    return false;

                return true;

            }, (p) => 
            {
                {
                    var animal = new Animal() { DisplayName = DisplayName, IdStorageAnimal = SelectedStorageAnimal.Id, Count = Count, DateInput = DateInput, StatusAnimal = StatusAnimal };

                    DataProvider.Ins.DB.Animal.Add(animal);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(animal);
                }
            });


            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null;
            }, (p) =>
            {
                var animalToDelete = DataProvider.Ins.DB.Animal.FirstOrDefault(u => u.Id == SelectedItem.Id);
                if (animalToDelete != null)
                {
                    DataProvider.Ins.DB.Animal.Remove(animalToDelete);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    List = new ObservableCollection<Animal>(DataProvider.Ins.DB.Animal);
                    StorageAnimal = DataServicess.Instancess.StorageAnimalList;
                }
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedStorageAnimal == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Animal.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var animal = DataProvider.Ins.DB.Animal.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                animal.DisplayName = DisplayName;
                animal.IdStorageAnimal = SelectedStorageAnimal.Id;
                animal.Count = Count;
                animal.StatusAnimal = StatusAnimal;
                animal.DateInput = DateInput;


                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                List = new ObservableCollection<Animal>(DataProvider.Ins.DB.Animal);
                StorageAnimal = DataServicess.Instancess.StorageAnimalList;

            });
            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedStorageAnimal == null)
                {
                    ShowSearchResults = false;
                    OnPropertyChanged(nameof(ShowSearchResults));
                    return;
                }
                FilterAnimal();
            });
        }

        private void FilterAnimal()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                FilteredAnimalList = new ObservableCollection<Animal>(List.Where(t => t.IdStorageAnimal == SelectedStorageAnimal.Id));
                ShowSearchResults = true;
            }
            else
            {
                FilteredAnimalList = new ObservableCollection<Animal>(
                    List.Where(t => t.DisplayName.Contains(SearchTerm))
                );
            }
            OnPropertyChanged(nameof(ShowSearchResults));
            OnPropertyChanged(nameof(FilteredAnimalList));
        }
    }
}
