//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyRung.Model
{
    using QuanLyRung.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public partial class Tree : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        private int? _Count;
        public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
        private int? _IdSupplierTree;
        public int? IdSupplierTree { get => _IdSupplierTree; set { _IdSupplierTree = value; OnPropertyChanged(); } }

        private SupplierTree _SupplierTree;
        public virtual SupplierTree SupplierTree { get => _SupplierTree; set { _SupplierTree = value; OnPropertyChanged(); } }

    }
}