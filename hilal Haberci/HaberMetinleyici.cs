using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hilal_Haberci
{
    public class HaberMetinleyici
    {
        [ColumnName("PredictedLabel")]
        public string HaberMetni { get; set; }
    }
}
