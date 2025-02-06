using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using Microsoft.ML.OnnxRuntime;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hilal_Haberci
{
    public class HaberTahmin
    {
        // Model ve veri yolu değişkenleri
        public static readonly string ModelYolu = "haberModel.zip";
        public MLContext mlContext = new MLContext();
        public PredictionEngine<HaberVerisi, HaberMetinleyici> predictionEngine;
        public ITransformer loadedModel;



        public void YukleEgit()
        {
            if (System.IO.File.Exists(ModelYolu))
            {
                // Modeli yükle
                loadedModel = mlContext.Model.Load(ModelYolu, out var inputSchema);
                Console.WriteLine($"Model {ModelYolu} dosyasından yüklendi.");
            }
            else
            {
                YeniHaberleriEgit();

            }
        }

        public async Task<string> HaberOlustur(string anahtarKelimeler)
        {
            //var veri = new HaberVerisi { AnahtarKelimeler = anahtarKelimeler };
            //predictionEngine = mlContext.Model.CreatePredictionEngine<HaberVerisi, HaberMetinleyici>(loadedModel);
            //var tahmin = predictionEngine.Predict(veri);

            var keywords = new
            {
                keywords = anahtarKelimeler.Split(" ")
            };

            var client = new HttpClient();
            var url = "http://127.0.0.1:5000/generate";

            var json = JsonConvert.SerializeObject(keywords);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Unicode karakterleri çöz ve metni al
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                    string generatedText = responseObject.generated_text.ToString();
                    //Console.WriteLine("Generated Text: " + generatedText);
                    for (int i = 0; i < 100; i++)
                    {
                        generatedText = generatedText.Replace("  ", " ");
                    }
                    return generatedText;
                }
                else
                {
                    MessageBox.Show("Error: " + response.StatusCode, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return null;
        }


        public void YeniHaberleriEgit()
        {
            string veriDosyası = "haber_veri_seti.txt";
            int batchSize = 10000; // Her seferde işlenecek satır sayısı

            if (!File.Exists(veriDosyası))
            {
                MessageBox.Show($"{veriDosyası} dosyası bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ITransformer model = null;
            int batchCounter = 0;
            
            foreach (var batchData in HaberVeriOkuyucusu.OkuVeBatchOlustur(veriDosyası, batchSize))
            {
                model = TrainBatch(batchData, model);
                batchCounter++;
                Console.WriteLine($"Batch {batchCounter} işlendi.");
            }

            // Eğitilen modeli kaydet
            using (var fileStream = new FileStream(ModelYolu, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, null, fileStream);
            }

            MessageBox.Show("Model başarıyla kaydedildi.", "Başardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private ITransformer TrainBatch(List<HaberVerisi> batchData, ITransformer existingModel)
        {
            // Batch verisini IDataView'e dönüştür
            var dataView = mlContext.Data.LoadFromEnumerable(batchData);

            // Veri işlem hattını oluştur
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(HaberVerisi.AnahtarKelimeler))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(HaberVerisi.HaberMetni)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // İlk batch için yeni bir model eğit, sonraki batch'ler için mevcut modeli güncelle
            return existingModel == null ? pipeline.Fit(dataView) : pipeline.Fit(dataView);
        }




    }
}
