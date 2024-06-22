using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Otopark_Otomasyonu
{
    public partial class Form1 : Form
    {
        SerialPort sr = new SerialPort(); // Seri port nesnesi oluþturuluyor

        public Form1()
        {
            InitializeComponent();
            sr.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler); // Seri porttan veri alýndýðýnda çalýþacak olan olay iþleyici atanýyor
        }

        // Boþ park sayýsýný hesaplayan fonksiyon
        private int GetEmptyParkCount()
        {
            int totalPark = 6; // Toplam park sayýsý
            int arabaCounts = GetArabaCount(); // Parkta bulunan araç sayýsýný al
            return totalPark - arabaCounts; // Boþ park sayýsýný hesapla
        }

        // Parkta bulunan araç sayýsýný veritabanýndan alan fonksiyon
        private int GetArabaCount()
        {
            int count = 0;
            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritabaný baðlantý dizesi

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritabaný baðlantýsýný aç
                    string query = "SELECT COUNT(*) FROM Araba"; // Araç sayýsýný almak için SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        count = (int)cmd.ExecuteScalar(); // Sorguyu çalýþtýr ve sonucu al
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araba count retrieval error: " + ex.Message); // Hata durumunda mesaj göster
                }
            }
            return count; // Araç sayýsýný döndür
        }

        // Form yüklendiðinde çalýþacak olan olay iþleyici
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames(); // Mevcut seri port isimlerini al
            comboBoxPortName.Items.AddRange(portNames); // Seri port isimlerini combobox'a ekle

            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritabaný baðlantý dizesi
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritabaný baðlantýsýný aç
                    string query = "SELECT * FROM Araba ORDER BY GirisSaati DESC"; // Araç verilerini almak için SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd); // Sorgu sonucunu almak için adapter oluþtur
                        DataTable table = new DataTable(); // DataTable oluþtur
                        adapter.Fill(table); // DataTable'ý doldur
                        dataGridView1.DataSource = table; // DataGridView'a veri kaynaðý olarak ata
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri çekerken hata oluþtu: " + ex.Message); // Hata durumunda mesaj göster
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritabaný baðlantýsýný aç
                    string query = "SELECT * FROM ArabaCikis2 ORDER BY CikisTarihi DESC, CikisSaati DESC"; // Çýkýþ yapmýþ araç verilerini almak için SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd); // Sorgu sonucunu almak için adapter oluþtur
                        DataTable table = new DataTable(); // DataTable oluþtur
                        adapter.Fill(table); // DataTable'ý doldur
                        dataGridView2.DataSource = table; // DataGridView'a veri kaynaðý olarak ata
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ArabaCikis2 verilerini çekerken hata oluþtu: " + ex.Message); // Hata durumunda mesaj göster
                }
            }

            int arabaCount = GetArabaCount(); // Parktaki araç sayýsýný al
            int emptyParks = GetEmptyParkCount(); // Boþ park sayýsýný al
            labelArabaCount.Text = arabaCount.ToString(); // Araç sayýsýný etikete yaz
            labelEmptyParks.Text = emptyParks.ToString(); // Boþ park sayýsýný etikete yaz
        }

        // Seri port açma butonuna týklandýðýnda çalýþacak olay iþleyici
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sr.IsOpen)
                {
                    sr.Close(); // Eðer seri port açýksa kapat
                }

                if (comboBoxPortName.SelectedItem != null)
                {
                    sr.PortName = comboBoxPortName.SelectedItem.ToString(); // Seçilen seri port adýný al
                    sr.BaudRate = 9600; // Baud rate ayarla
                    sr.Open(); // Seri portu aç
                    button1.Enabled = false; // Açma butonunu devre dýþý býrak
                    button2.Enabled = true; // Kapama butonunu etkinleþtir
                    label3.Text = "AÇIK"; // Etikete "AÇIK" yaz
                    sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan araç sayýsýný gönder
                }
                else
                {
                    MessageBox.Show("Lütfen bir port seçin."); // Port seçilmemiþse mesaj göster
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seri port açýlýrken hata oluþtu: " + ex.Message); // Hata durumunda mesaj göster
            }
        }

        // Araç ID'lerini veritabanýndan alan fonksiyon
        private List<int> GetArabaIDs()
        {
            List<int> arabaIDs = new List<int>();
            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritabaný baðlantý dizesi

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritabaný baðlantýsýný aç
                    string query = "SELECT ArabaID FROM Araba"; // Araç ID'lerini almak için SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader(); // Sorgu sonucunu okuyucu ile al
                        while (reader.Read())
                        {
                            arabaIDs.Add(reader.GetInt32(0)); // Her bir ID'yi listeye ekle
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araba ID Hatasý: " + ex.Message); // Hata durumunda mesaj göster
                }
            }

            return arabaIDs; // Araç ID'lerini döndür
        }

        // Seri porttan veri alýndýðýnda çalýþacak olay iþleyici
        private void SerialDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string inData = sp.ReadExisting().Trim(); // Gelen veriyi al ve boþluklarý temizle

                // Gelen veriyi konsola yaz
                Console.WriteLine("Received data: " + inData);

                if (inData == "1")
                {
                    if (GetArabaCount() < 6)
                    {
                        HandleNewCarEntry(); // Yeni araç giriþi iþlemi
                    }
                    else
                    {
                        MessageBox.Show("Otoparkta Boþ Alan Yok"); // Otoparkta boþ alan yoksa mesaj göster
                    }
                }
                else if (inData == "2")
                {
                    HandleCarExit(); // Araç çýkýþý iþlemi
                }
                else
                {
                    Console.WriteLine("Unexpected data received: " + inData); // Beklenmeyen veri alýndýðýnda konsola yaz
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data received handler error: " + ex.Message); // Hata durumunda mesaj göster
            }
        }

        // Yeni araç giriþi iþlemi
        private void HandleNewCarEntry()
        {
            DateTime currentTime = DateTime.Now; // Þu anki zamaný al
            string girisSaati = currentTime.ToString("HH:mm:ss"); // Giriþ saatini al
            string girisTarihi = currentTime.ToString("dd.MM.yyyy"); // Giriþ tarihini al

            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritabaný baðlantý dizesi
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritabaný baðlantýsýný aç
                    string query = "INSERT INTO Araba (GirisSaati, GirisTarihi) VALUES (@GirisSaati, @GirisTarihi)"; // Araç giriþ bilgilerini eklemek için SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@GirisSaati", girisSaati); // Giriþ saati parametresini ekle
                        cmd.Parameters.AddWithValue("@GirisTarihi", girisTarihi); // Giriþ tarihi parametresini ekle
                        cmd.ExecuteNonQuery(); // Sorguyu çalýþtýr
                    }

                    this.Invoke(new Action(() =>
                    {
                        Form1_Load(null, null); // Formu yeniden yükle
                        int arabaCount = GetArabaCount(); // Araç sayýsýný al
                        labelArabaCount.Text = arabaCount.ToString(); // Araç sayýsýný etikete yaz
                        sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan araç sayýsýný gönder
                    }));

                    MessageBox.Show("Yeni Araç Giriþ Yaptý\nGiriþ Tarihi: " + currentTime); // Yeni araç giriþi mesajý göster
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araba giriþ hatasý: " + ex.Message); // Hata durumunda mesaj göster
                }
            }
        }

        // Araç çýkýþý iþlemi
        private void HandleCarExit()
        {
            List<int> arabaIDs = GetArabaIDs(); // Araç ID'lerini al
            if (arabaIDs.Count > 0)
            {
                Random rnd = new Random();
                int indexToDelete = arabaIDs[rnd.Next(arabaIDs.Count)]; // Rastgele bir araç ID seç

                string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritabaný baðlantý dizesi
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open(); // Veritabaný baðlantýsýný aç

                        // Araç giriþ zamanýný al
                        DateTime girisZamani;
                        string selectQuery = "SELECT GirisSaati, GirisTarihi FROM Araba WHERE ArabaID = @ID"; // Giriþ bilgilerini almak için SQL sorgusu
                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                        {
                            selectCmd.Parameters.AddWithValue("@ID", indexToDelete); // Araç ID'si parametresini ekle
                            using (SqlDataReader reader = selectCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    girisZamani = DateTime.ParseExact(reader["GirisTarihi"].ToString() + " " + reader["GirisSaati"].ToString(), "dd.MM.yyyy HH:mm:ss", null); // Giriþ zamanýný al
                                }
                                else
                                {
                                    MessageBox.Show("Araba bilgisi bulunamadý."); // Araç bilgisi bulunamadýysa mesaj göster
                                    return;
                                }
                            }
                        }

                        // Þu anki zaman
                        DateTime cikisZamani = DateTime.Now;

                        // Kaldýðý süreyi hesapla
                        TimeSpan kalinanSure = cikisZamani - girisZamani;
                        int totalMinutes = (int)kalinanSure.TotalMinutes; // Toplam dakikayý al
                        double odeme = totalMinutes * 5; // Dakika baþýna 5 TL ücret

                        // Ücreti göster ve ödeme onayý iste
                        DialogResult result = MessageBox.Show($"Araba ID: {indexToDelete}\nKaldýðý Süre: {kalinanSure.Days} gün {kalinanSure.Hours} saat {kalinanSure.Minutes} dakika\nÖdenecek Tutar: {odeme:F2} TL\nAraba ödemeyi yaptý mý?", "Ödeme Onayý", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            // ArabaCikis2 tablosu için IDENTITY_INSERT'i etkinleþtir
                            string enableIdentityInsertQuery = "SET IDENTITY_INSERT ArabaCikis2 ON";
                            using (SqlCommand enableIdentityInsertCmd = new SqlCommand(enableIdentityInsertQuery, con))
                            {
                                enableIdentityInsertCmd.ExecuteNonQuery();
                            }

                            // Kayýtlarý ArabaCikis2 tablosuna ekle
                            string insertQuery = "INSERT INTO ArabaCikis2 (ArabaID, GirisTarihi, GirisSaati, CikisTarihi, CikisSaati, OdemeTutari) " +
                                                 "VALUES (@ID, @GirisTarihi, @GirisSaati, @CikisTarihi, @CikisSaati, @OdemeTutari)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@ID", indexToDelete); // Araç ID'si parametresini ekle
                                insertCmd.Parameters.AddWithValue("@GirisTarihi", girisZamani.ToString("dd.MM.yyyy")); // Giriþ tarihi parametresini ekle
                                insertCmd.Parameters.AddWithValue("@GirisSaati", girisZamani.ToString("HH:mm:ss")); // Giriþ saati parametresini ekle
                                insertCmd.Parameters.AddWithValue("@CikisTarihi", cikisZamani.ToString("dd.MM.yyyy")); // Çýkýþ tarihi parametresini ekle
                                insertCmd.Parameters.AddWithValue("@CikisSaati", cikisZamani.ToString("HH:mm:ss")); // Çýkýþ saati parametresini ekle
                                insertCmd.Parameters.AddWithValue("@OdemeTutari", odeme); // Ödeme tutarý parametresini ekle
                                insertCmd.ExecuteNonQuery(); // Sorguyu çalýþtýr
                            }

                            // ArabaCikis2 tablosu için IDENTITY_INSERT'i devre dýþý býrak
                            string disableIdentityInsertQuery = "SET IDENTITY_INSERT ArabaCikis2 OFF";
                            using (SqlCommand disableIdentityInsertCmd = new SqlCommand(disableIdentityInsertQuery, con))
                            {
                                disableIdentityInsertCmd.ExecuteNonQuery();
                            }

                            // Araba tablosundan kaydý sil
                            string deleteQuery = "DELETE FROM Araba WHERE ArabaID = @ID";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {
                                deleteCmd.Parameters.AddWithValue("@ID", indexToDelete); // Araç ID'si parametresini ekle
                                deleteCmd.ExecuteNonQuery(); // Sorguyu çalýþtýr
                            }

                            this.Invoke(new Action(() =>
                            {
                                Form1_Load(null, null); // Formu yeniden yükle
                                int arabaCount = GetArabaCount(); // Araç sayýsýný al
                                labelArabaCount.Text = arabaCount.ToString(); // Araç sayýsýný etikete yaz
                                sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan araç sayýsýný gönder
                            }));

                            MessageBox.Show("Araba çýkýþ yaptý ve ödeme alýndý."); // Çýkýþ ve ödeme mesajý göster
                        }
                        else
                        {
                            MessageBox.Show("Araba çýkýþý yapýlamaz. Lütfen önce ödemeyi yapýn."); // Ödeme yapýlmadýysa mesaj göster
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Araba çýkýþ hatasý: " + ex.Message); // Hata durumunda mesaj göster
                    }
                }
            }
        }

        // Form kapandýðýnda çalýþacak olay iþleyici
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sr.IsOpen)
            {
                sr.Close(); // Eðer seri port açýksa kapat
            }
        }

        // Seri portu kapatma butonuna týklandýðýnda çalýþacak olay iþleyici
        private void button2_Click(object sender, EventArgs e)
        {
            if (sr.IsOpen)
            {
                sr.Close(); // Seri portu kapat
                button1.Enabled = true; // Açma butonunu etkinleþtir
                button2.Enabled = false; // Kapama butonunu devre dýþý býrak
                label3.Text = "KAPALI"; // Etikete "KAPALI" yaz
            }
        }
    }
}
