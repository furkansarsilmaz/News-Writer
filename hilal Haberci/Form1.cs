using System;

namespace hilal_Haberci
{
    public partial class Form1 : Form
    {
        HaberTahmin haberTahminci = new HaberTahmin();
        private static Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            //haberTahminci.YukleEgit();
        }

        private async void Uretim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(anahtarBox.Text))
            {
                string inputText = anahtarBox.Text;
                string gelenHaber = string.Empty;
                
                // Anahtar kelimeleri gruplandýrýyoruz
                List<string> groupedWords = GroupWordsInThrees(inputText);

                haberBox.Clear();
                haberBox.Text = "Api'ye gidiliyor...";

                // Asenkron olarak haberleri oluþturuyoruz
                for (int i = 0; i < groupedWords.Count; i++)
                {
                    string result = await haberTahminci.HaberOlustur(groupedWords[i]); // Asenkron çaðrý
                    gelenHaber += result; // Sonuçlarý ekliyoruz
                    haberBox.Text = "Api'ye ulaþýldý haber alýnýyor...";
                }
                haberBox.Text = gelenHaber;
                

            }
            else
            {
                MessageBox.Show("Anahtar kelime girmediniz!", "Kelime gir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void haberEgitim_Click(object sender, EventArgs e)
        {
            haberTahminci.YeniHaberleriEgit();
        }
        private List<string> GroupWordsInThrees(string input)
        {
            string[] anahtarKelimeler = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> AnahtarGruplar = new List<string>();
            for (int i = 0; i < anahtarKelimeler.Length; i += 5)
            {
                string grup;
                grup = string.Join(" ", anahtarKelimeler.Skip(i).Take(5));

                if (anahtarKelimeler.Length - i < 3)
                {
                    int randomIndex1 = random.Next(anahtarKelimeler.Length);
                    int randomIndex2 = random.Next(anahtarKelimeler.Length);
                    int randomIndex3 = random.Next(anahtarKelimeler.Length);
                    grup = string.Join(" ", anahtarKelimeler.Skip(i).Take(5)) + " " + string.Join(" ", anahtarKelimeler[randomIndex1]) + " " +
                        string.Join(" ", anahtarKelimeler[randomIndex2]) + " " + string.Join(" ", anahtarKelimeler[randomIndex3]);
                }

                AnahtarGruplar.Add(grup);
            }
            return AnahtarGruplar;
        }

        private void haberBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
