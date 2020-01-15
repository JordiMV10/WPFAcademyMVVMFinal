using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFAcademyMVVMFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //var std = new Student();
            //std.Name = "PericoNooo";
            //std.Dni = "abcdaaaaa";
            //var sr = std.Save<Student>();

            //if (sr.IsSuccess)
            //{
            //    var repoStudents = Entity.DepCon.Resolve<IStudentsRepository>();

            //    var student = repoStudents.FindByDni("abcdaaaaa");
            //    MessageBox.Show(student.Name);
            //}

        }
    }
}
