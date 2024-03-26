using LiveCharts.Wpf.Charts.Base;
using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monefy.Services.Interfaces
{
    internal interface IChartService
    {
        public void AddSerie(MyChart chart, Brush color);
        
    }
}
