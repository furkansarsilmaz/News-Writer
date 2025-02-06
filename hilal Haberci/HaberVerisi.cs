using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hilal_Haberci
{
    public class HaberVerisi
    {
        [LoadColumn(0)] // İlk sütun (AnahtarKelimeler)
        public string AnahtarKelimeler { get; set; }

        [LoadColumn(1)] // İkinci sütun (HaberMetni)
        public string HaberMetni { get; set; }
    }
}
