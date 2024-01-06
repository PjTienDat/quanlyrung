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
    public class WoodViewModel : BaseViewModel
    {
        private ObservableCollection<Wood> _List;
        public ObservableCollection<Wood> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<SupplierWood> _SupplierWood;
        public ObservableCollection<SupplierWood> SupplierWood { get => _SupplierWood; set { _SupplierWood = value; OnPropertyChanged(); } }
        private ObservableCollection<Wood> _FilteredWoodList;
        public ObservableCollection<Wood> FilteredWoodList
        {
            get => _FilteredWoodList;
            set { _FilteredWoodList = value; OnPropertyChanged(); }
        }

        private Wood _SelectedItem;
        public Wood SelectedItem
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
                    Unit = SelectedItem.Unit;
                    SelectedSupplierWood = SelectedItem.SupplierWood;
                    DateInput = SelectedItem.DateInput;


                }
            }
        }
        private SupplierWood _SelectedSupplierWood;
        public SupplierWood SelectedSupplierWood
        {
            get => _SelectedSupplierWood;
            set
            {
                _SelectedSupplierWood = value;
                OnPropertyChanged();

            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        private int? _Count;
        public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
        private string _Unit;
        public string Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }
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
                FilterWood();


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





        public WoodViewModel()
        {
            List = new ObservableCollection<Wood>(DataProvider.Ins.DB.Wood);
            SupplierWood = DataServices.Instances.SupplierWoodList;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSupplierWood == null)
                    return false;

                return true;

            }, (p) => {
                {
                    var wood = new Wood() { DisplayName = DisplayName, IdSupplierWood = SelectedSupplierWood.Id, Count = Count, DateInput = DateInput, Unit = Unit };

                    DataProvider.Ins.DB.Wood.Add(wood);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(wood);
                }
            });


            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null;
            }, (p) =>
            {
                var woodToDelete = DataProvider.Ins.DB.Wood.FirstOrDefault(u => u.Id == SelectedItem.Id);
                if (woodToDelete != null)
                {
                    DataProvider.Ins.DB.Wood.Remove(woodToDelete);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    List = new ObservableCollection<Wood>(DataProvider.Ins.DB.Wood);
                    SupplierWood = DataServices.Instances.SupplierWoodList;
                }
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSupplierWood == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Wood.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var wood = DataProvider.Ins.DB.Wood.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                wood.DisplayName = DisplayName;
                wood.IdSupplierWood = SelectedSupplierWood.Id;
                wood.Count = Count;
                wood.Unit = Unit;
                wood.DateInput = DateInput;

                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                List = new ObservableCollection<Wood>(DataProvider.Ins.DB.Wood);
                SupplierWood = DataServices.Instances.SupplierWoodList;

            });
            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedSupplierWood == null)
                {
                    ShowSearchResults = false;
                    OnPropertyChanged(nameof(ShowSearchResults));
                    return;
                }
                FilterWood();
            });
        }

        private void FilterWood()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                FilteredWoodList = new ObservableCollection<Wood>(List.Where(t => t.IdSupplierWood == SelectedSupplierWood.Id));
                ShowSearchResults = true;
            }
            else
            {
                FilteredWoodList = new ObservableCollection<Wood>(
                    List.Where(t => t.DisplayName.Contains(SearchTerm))
                );
            }
            OnPropertyChanged(nameof(ShowSearchResults));
            OnPropertyChanged(nameof(FilteredWoodList));
        }
    }
}
