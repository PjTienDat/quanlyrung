using QuanLyRung.Model;
using QuanLyRung.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{
    
public class SupplierTreeViewModel : BaseViewModel
{
    private ObservableCollection<SupplierTree> _List;
    public ObservableCollection<SupplierTree> List { get => _List; set { _List = value; OnPropertyChanged(); } }

    private SupplierTree _SelectedItem;
    public SupplierTree SelectedItem
    {
        get => _SelectedItem;
        set
        {
            _SelectedItem = value;
            OnPropertyChanged();
            if (SelectedItem != null)
            {



                DisplayName = SelectedItem.DisplayName;
                Adress = SelectedItem.Adress;




            }
        }
    }




    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

    private string _Adress;
    public string Adress { get => _Adress; set { _Adress = value; OnPropertyChanged(); } }


    public ICommand AddCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }







        public SupplierTreeViewModel()
    {
        List = DataService.Instance.SupplierTreeList;

        AddCommand = new RelayCommand<object>((p) =>
        {
            if (DisplayName == null)
                return false;

            return true;

        }, (p) => {
            {
                var supplierTree = new SupplierTree() { DisplayName = DisplayName, Adress = Adress };

                DataProvider.Ins.DB.SupplierTree.Add(supplierTree);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(supplierTree);
            }
        });

           




            EditCommand = new RelayCommand<object>((p) =>
        {
            if ( SelectedItem == null)
                return false;

            var displayList = DataProvider.Ins.DB.SupplierTree.Where(x => x.Id == SelectedItem.Id);
            if (displayList != null && displayList.Count() != 0)
                return true;
            return false;

        }, (p) =>
        {
            var supplierTree = DataProvider.Ins.DB.SupplierTree.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
            supplierTree.DisplayName = DisplayName;
            supplierTree.Adress = Adress;
            DataProvider.Ins.DB.SaveChanges();

            SelectedItem.DisplayName = DisplayName;
            List = DataService.Instance.SupplierTreeList;

        });


            DeleteCommand = new RelayCommand<object>((p) => {
                return SelectedItem != null;
            }, (p) => {
                var supplierTree = DataProvider.Ins.DB.SupplierTree.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                if (supplierTree != null)
                {
                    
                    var relatedItems = DataProvider.Ins.DB.Tree.Where(x => x.IdSupplierTree == SelectedItem.Id).ToList();

                    DataProvider.Ins.DB.Tree.RemoveRange(relatedItems);
                    DataProvider.Ins.DB.SupplierTree.Remove(supplierTree);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    DataService.Instance.SupplierTreeList.Remove(SelectedItem);
                }
              
            });


        }
   }
}

