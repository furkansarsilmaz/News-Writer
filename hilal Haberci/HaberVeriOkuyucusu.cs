using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hilal_Haberci
{
    public class HaberVeriOkuyucusu
    {

        public static IEnumerable<List<HaberVerisi>> OkuVeBatchOlustur(string dosyaYolu, int batchSize)
        {
            if (!File.Exists(dosyaYolu))
            {
                throw new FileNotFoundException($"Dosya bulunamadı: {dosyaYolu}");
            }

            var batchData = new List<HaberVerisi>();
            using var reader = new StreamReader(dosyaYolu);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("AnahtarKelimeler"))
                {
                    var parts = line.Split(new string[] { " | " }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        batchData.Add(new HaberVerisi
                        {
                            AnahtarKelimeler = parts[0].Replace("AnahtarKelimeler: ", "").Trim(),
                            HaberMetni = parts[1].Replace("HaberMetni: ", "").Trim()
                        });
                    }
                }

                // Batch dolduğunda veriyi döndür
                if (batchData.Count >= batchSize)
                {
                    //yield döndüğü yerde durur tekrar çağırıldığında kaldığı satırdan okumaya devam eder.
                    yield return batchData;
                    batchData = new List<HaberVerisi>(); // Yeni batch için listeyi sıfırla
                }
            }

            // Son kalan veriler varsa döndür
            if (batchData.Count > 0)
            {
                //yield veriler işlem tamamlandıktan sonra bellekten otomatik olarak temizlenir.
                yield return batchData;
            }
        }
        }
    }
