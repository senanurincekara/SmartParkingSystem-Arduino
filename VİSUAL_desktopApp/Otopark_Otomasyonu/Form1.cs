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
        SerialPort sr = new SerialPort(); // Seri port nesnesi olu�turuluyor

        public Form1()
        {
            InitializeComponent();
            sr.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler); // Seri porttan veri al�nd���nda �al��acak olan olay i�leyici atan�yor
        }

        // Bo� park say�s�n� hesaplayan fonksiyon
        private int GetEmptyParkCount()
        {
            int totalPark = 6; // Toplam park say�s�
            int arabaCounts = GetArabaCount(); // Parkta bulunan ara� say�s�n� al
            return totalPark - arabaCounts; // Bo� park say�s�n� hesapla
        }

        // Parkta bulunan ara� say�s�n� veritaban�ndan alan fonksiyon
        private int GetArabaCount()
        {
            int count = 0;
            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritaban� ba�lant� dizesi

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritaban� ba�lant�s�n� a�
                    string query = "SELECT COUNT(*) FROM Araba"; // Ara� say�s�n� almak i�in SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        count = (int)cmd.ExecuteScalar(); // Sorguyu �al��t�r ve sonucu al
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araba count retrieval error: " + ex.Message); // Hata durumunda mesaj g�ster
                }
            }
            return count; // Ara� say�s�n� d�nd�r
        }

        // Form y�klendi�inde �al��acak olan olay i�leyici
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames(); // Mevcut seri port isimlerini al
            comboBoxPortName.Items.AddRange(portNames); // Seri port isimlerini combobox'a ekle

            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritaban� ba�lant� dizesi
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritaban� ba�lant�s�n� a�
                    string query = "SELECT * FROM Araba ORDER BY GirisSaati DESC"; // Ara� verilerini almak i�in SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd); // Sorgu sonucunu almak i�in adapter olu�tur
                        DataTable table = new DataTable(); // DataTable olu�tur
                        adapter.Fill(table); // DataTable'� doldur
                        dataGridView1.DataSource = table; // DataGridView'a veri kayna�� olarak ata
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri �ekerken hata olu�tu: " + ex.Message); // Hata durumunda mesaj g�ster
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritaban� ba�lant�s�n� a�
                    string query = "SELECT * FROM ArabaCikis2 ORDER BY CikisTarihi DESC, CikisSaati DESC"; // ��k�� yapm�� ara� verilerini almak i�in SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd); // Sorgu sonucunu almak i�in adapter olu�tur
                        DataTable table = new DataTable(); // DataTable olu�tur
                        adapter.Fill(table); // DataTable'� doldur
                        dataGridView2.DataSource = table; // DataGridView'a veri kayna�� olarak ata
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ArabaCikis2 verilerini �ekerken hata olu�tu: " + ex.Message); // Hata durumunda mesaj g�ster
                }
            }

            int arabaCount = GetArabaCount(); // Parktaki ara� say�s�n� al
            int emptyParks = GetEmptyParkCount(); // Bo� park say�s�n� al
            labelArabaCount.Text = arabaCount.ToString(); // Ara� say�s�n� etikete yaz
            labelEmptyParks.Text = emptyParks.ToString(); // Bo� park say�s�n� etikete yaz
        }

        // Seri port a�ma butonuna t�kland���nda �al��acak olay i�leyici
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sr.IsOpen)
                {
                    sr.Close(); // E�er seri port a��ksa kapat
                }

                if (comboBoxPortName.SelectedItem != null)
                {
                    sr.PortName = comboBoxPortName.SelectedItem.ToString(); // Se�ilen seri port ad�n� al
                    sr.BaudRate = 9600; // Baud rate ayarla
                    sr.Open(); // Seri portu a�
                    button1.Enabled = false; // A�ma butonunu devre d��� b�rak
                    button2.Enabled = true; // Kapama butonunu etkinle�tir
                    label3.Text = "A�IK"; // Etikete "A�IK" yaz
                    sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan ara� say�s�n� g�nder
                }
                else
                {
                    MessageBox.Show("L�tfen bir port se�in."); // Port se�ilmemi�se mesaj g�ster
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seri port a��l�rken hata olu�tu: " + ex.Message); // Hata durumunda mesaj g�ster
            }
        }

        // Ara� ID'lerini veritaban�ndan alan fonksiyon
        private List<int> GetArabaIDs()
        {
            List<int> arabaIDs = new List<int>();
            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritaban� ba�lant� dizesi

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritaban� ba�lant�s�n� a�
                    string query = "SELECT ArabaID FROM Araba"; // Ara� ID'lerini almak i�in SQL sorgusu
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
                    MessageBox.Show("Araba ID Hatas�: " + ex.Message); // Hata durumunda mesaj g�ster
                }
            }

            return arabaIDs; // Ara� ID'lerini d�nd�r
        }

        // Seri porttan veri al�nd���nda �al��acak olay i�leyici
        private void SerialDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string inData = sp.ReadExisting().Trim(); // Gelen veriyi al ve bo�luklar� temizle

                // Gelen veriyi konsola yaz
                Console.WriteLine("Received data: " + inData);

                if (inData == "1")
                {
                    if (GetArabaCount() < 6)
                    {
                        HandleNewCarEntry(); // Yeni ara� giri�i i�lemi
                    }
                    else
                    {
                        MessageBox.Show("Otoparkta Bo� Alan Yok"); // Otoparkta bo� alan yoksa mesaj g�ster
                    }
                }
                else if (inData == "2")
                {
                    HandleCarExit(); // Ara� ��k��� i�lemi
                }
                else
                {
                    Console.WriteLine("Unexpected data received: " + inData); // Beklenmeyen veri al�nd���nda konsola yaz
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data received handler error: " + ex.Message); // Hata durumunda mesaj g�ster
            }
        }

        // Yeni ara� giri�i i�lemi
        private void HandleNewCarEntry()
        {
            DateTime currentTime = DateTime.Now; // �u anki zaman� al
            string girisSaati = currentTime.ToString("HH:mm:ss"); // Giri� saatini al
            string girisTarihi = currentTime.ToString("dd.MM.yyyy"); // Giri� tarihini al

            string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritaban� ba�lant� dizesi
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Veritaban� ba�lant�s�n� a�
                    string query = "INSERT INTO Araba (GirisSaati, GirisTarihi) VALUES (@GirisSaati, @GirisTarihi)"; // Ara� giri� bilgilerini eklemek i�in SQL sorgusu
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@GirisSaati", girisSaati); // Giri� saati parametresini ekle
                        cmd.Parameters.AddWithValue("@GirisTarihi", girisTarihi); // Giri� tarihi parametresini ekle
                        cmd.ExecuteNonQuery(); // Sorguyu �al��t�r
                    }

                    this.Invoke(new Action(() =>
                    {
                        Form1_Load(null, null); // Formu yeniden y�kle
                        int arabaCount = GetArabaCount(); // Ara� say�s�n� al
                        labelArabaCount.Text = arabaCount.ToString(); // Ara� say�s�n� etikete yaz
                        sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan ara� say�s�n� g�nder
                    }));

                    MessageBox.Show("Yeni Ara� Giri� Yapt�\nGiri� Tarihi: " + currentTime); // Yeni ara� giri�i mesaj� g�ster
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araba giri� hatas�: " + ex.Message); // Hata durumunda mesaj g�ster
                }
            }
        }

        // Ara� ��k��� i�lemi
        private void HandleCarExit()
        {
            List<int> arabaIDs = GetArabaIDs(); // Ara� ID'lerini al
            if (arabaIDs.Count > 0)
            {
                Random rnd = new Random();
                int indexToDelete = arabaIDs[rnd.Next(arabaIDs.Count)]; // Rastgele bir ara� ID se�

                string connectionString = "Data Source=DESKTOP-2JMETKU;Initial Catalog=sensem;Integrated Security=True;Encrypt=False"; // Veritaban� ba�lant� dizesi
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open(); // Veritaban� ba�lant�s�n� a�

                        // Ara� giri� zaman�n� al
                        DateTime girisZamani;
                        string selectQuery = "SELECT GirisSaati, GirisTarihi FROM Araba WHERE ArabaID = @ID"; // Giri� bilgilerini almak i�in SQL sorgusu
                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                        {
                            selectCmd.Parameters.AddWithValue("@ID", indexToDelete); // Ara� ID'si parametresini ekle
                            using (SqlDataReader reader = selectCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    girisZamani = DateTime.ParseExact(reader["GirisTarihi"].ToString() + " " + reader["GirisSaati"].ToString(), "dd.MM.yyyy HH:mm:ss", null); // Giri� zaman�n� al
                                }
                                else
                                {
                                    MessageBox.Show("Araba bilgisi bulunamad�."); // Ara� bilgisi bulunamad�ysa mesaj g�ster
                                    return;
                                }
                            }
                        }

                        // �u anki zaman
                        DateTime cikisZamani = DateTime.Now;

                        // Kald��� s�reyi hesapla
                        TimeSpan kalinanSure = cikisZamani - girisZamani;
                        int totalMinutes = (int)kalinanSure.TotalMinutes; // Toplam dakikay� al
                        double odeme = totalMinutes * 5; // Dakika ba��na 5 TL �cret

                        // �creti g�ster ve �deme onay� iste
                        DialogResult result = MessageBox.Show($"Araba ID: {indexToDelete}\nKald��� S�re: {kalinanSure.Days} g�n {kalinanSure.Hours} saat {kalinanSure.Minutes} dakika\n�denecek Tutar: {odeme:F2} TL\nAraba �demeyi yapt� m�?", "�deme Onay�", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            // ArabaCikis2 tablosu i�in IDENTITY_INSERT'i etkinle�tir
                            string enableIdentityInsertQuery = "SET IDENTITY_INSERT ArabaCikis2 ON";
                            using (SqlCommand enableIdentityInsertCmd = new SqlCommand(enableIdentityInsertQuery, con))
                            {
                                enableIdentityInsertCmd.ExecuteNonQuery();
                            }

                            // Kay�tlar� ArabaCikis2 tablosuna ekle
                            string insertQuery = "INSERT INTO ArabaCikis2 (ArabaID, GirisTarihi, GirisSaati, CikisTarihi, CikisSaati, OdemeTutari) " +
                                                 "VALUES (@ID, @GirisTarihi, @GirisSaati, @CikisTarihi, @CikisSaati, @OdemeTutari)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@ID", indexToDelete); // Ara� ID'si parametresini ekle
                                insertCmd.Parameters.AddWithValue("@GirisTarihi", girisZamani.ToString("dd.MM.yyyy")); // Giri� tarihi parametresini ekle
                                insertCmd.Parameters.AddWithValue("@GirisSaati", girisZamani.ToString("HH:mm:ss")); // Giri� saati parametresini ekle
                                insertCmd.Parameters.AddWithValue("@CikisTarihi", cikisZamani.ToString("dd.MM.yyyy")); // ��k�� tarihi parametresini ekle
                                insertCmd.Parameters.AddWithValue("@CikisSaati", cikisZamani.ToString("HH:mm:ss")); // ��k�� saati parametresini ekle
                                insertCmd.Parameters.AddWithValue("@OdemeTutari", odeme); // �deme tutar� parametresini ekle
                                insertCmd.ExecuteNonQuery(); // Sorguyu �al��t�r
                            }

                            // ArabaCikis2 tablosu i�in IDENTITY_INSERT'i devre d��� b�rak
                            string disableIdentityInsertQuery = "SET IDENTITY_INSERT ArabaCikis2 OFF";
                            using (SqlCommand disableIdentityInsertCmd = new SqlCommand(disableIdentityInsertQuery, con))
                            {
                                disableIdentityInsertCmd.ExecuteNonQuery();
                            }

                            // Araba tablosundan kayd� sil
                            string deleteQuery = "DELETE FROM Araba WHERE ArabaID = @ID";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {
                                deleteCmd.Parameters.AddWithValue("@ID", indexToDelete); // Ara� ID'si parametresini ekle
                                deleteCmd.ExecuteNonQuery(); // Sorguyu �al��t�r
                            }

                            this.Invoke(new Action(() =>
                            {
                                Form1_Load(null, null); // Formu yeniden y�kle
                                int arabaCount = GetArabaCount(); // Ara� say�s�n� al
                                labelArabaCount.Text = arabaCount.ToString(); // Ara� say�s�n� etikete yaz
                                sr.WriteLine("sayi:" + labelArabaCount.Text); // Seri porttan ara� say�s�n� g�nder
                            }));

                            MessageBox.Show("Araba ��k�� yapt� ve �deme al�nd�."); // ��k�� ve �deme mesaj� g�ster
                        }
                        else
                        {
                            MessageBox.Show("Araba ��k��� yap�lamaz. L�tfen �nce �demeyi yap�n."); // �deme yap�lmad�ysa mesaj g�ster
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Araba ��k�� hatas�: " + ex.Message); // Hata durumunda mesaj g�ster
                    }
                }
            }
        }

        // Form kapand���nda �al��acak olay i�leyici
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sr.IsOpen)
            {
                sr.Close(); // E�er seri port a��ksa kapat
            }
        }

        // Seri portu kapatma butonuna t�kland���nda �al��acak olay i�leyici
        private void button2_Click(object sender, EventArgs e)
        {
            if (sr.IsOpen)
            {
                sr.Close(); // Seri portu kapat
                button1.Enabled = true; // A�ma butonunu etkinle�tir
                button2.Enabled = false; // Kapama butonunu devre d��� b�rak
                label3.Text = "KAPALI"; // Etikete "KAPALI" yaz
            }
        }
    }
}
