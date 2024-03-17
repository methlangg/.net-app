using System;
using System.IO;
using System.Linq;

class NotDefteri
{
    static string NotDefteriYolu = "notlar.txt";

    static void Main(string[] args)
    {
        Console.WriteLine("Not defterine hoş geldiniz!");

        while (true)
        {
            Console.WriteLine("\nLütfen yapmak istediğiniz işlemi seçin:");
            Console.WriteLine("1. Notları Görüntüle");
            Console.WriteLine("2. Not Ekle");
            Console.WriteLine("3. Not Sil");
            Console.WriteLine("4. Not Defteri Değiştir");
            Console.WriteLine("5. Çıkış");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    NotlariGoruntule();
                    break;
                case "2":
                    NotEkle();
                    break;
                case "3":
                    NotSil();
                    break;
                case "4":
                    NotDefteriDegistir();
                    break;
                case "5":
                    Console.WriteLine("Not defterinden çıkılıyor...");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void NotlariGoruntule()
    {
        if (!File.Exists(NotDefteriYolu))
        {
            Console.WriteLine("Not defterinde henüz hiç not yok.");
            return;
        }

        Console.WriteLine("Notlar:\n");

        string[] notlar = File.ReadAllLines(NotDefteriYolu);
        notlar = notlar.OrderBy(not => DateTime.Parse(not.Split(':')[0])).ToArray();

        for (int i = 0; i < notlar.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {notlar[i]}");
        }
    }

    static void NotEkle()
    {
        Console.WriteLine("Yeni notunuzu girin:");
        string yeniNot = Console.ReadLine();

        using (StreamWriter writer = File.AppendText(NotDefteriYolu))
        {
            writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {yeniNot}");
        }

        Console.WriteLine("Notunuz başarıyla eklendi!");
    }

    static void NotSil()
    {
        Console.WriteLine("Silmek istediğiniz notun numarasını girin:");
        int notNumarasi;

        if (!int.TryParse(Console.ReadLine(), out notNumarasi) || notNumarasi < 1)
        {
            Console.WriteLine("Geçersiz not numarası!");
            return;
        }

        if (!File.Exists(NotDefteriYolu))
        {
            Console.WriteLine("Not defterinde henüz hiç not yok.");
            return;
        }

        string[] notlar = File.ReadAllLines(NotDefteriYolu);

        if (notNumarasi > notlar.Length)
        {
            Console.WriteLine($"Geçersiz not numarası! Lütfen 1 ile {notlar.Length} arasında bir değer girin.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(NotDefteriYolu))
        {
            for (int i = 0; i < notlar.Length; i++)
            {
                if (i != notNumarasi - 1)
                {
                    writer.WriteLine(notlar[i]);
                }
            }
        }

        Console.WriteLine("Not başarıyla silindi!");
    }

    static void NotDefteriDegistir()
    {
        Console.WriteLine("Yeni not defterinin yolunu girin:");
        string yeniNotDefteriYolu = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(yeniNotDefteriYolu) || !File.Exists(yeniNotDefteriYolu))
        {
            Console.WriteLine("Belirtilen dosya mevcut değil!");
            return;
        }

        NotDefteriYolu = yeniNotDefteriYolu;

        Console.WriteLine("Not defteri başarıyla değiştirildi.");
    }
}
