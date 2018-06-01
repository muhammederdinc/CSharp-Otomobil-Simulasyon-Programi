using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otomobil
{
    class Program
    {
        static void Main(string[] args)
        {
            Pedal pedal = new Pedal(); // Pedal Nesnesi Oluşturuyoruz.
       
            pedal.GazPedalinaBas();
            pedal.GazPedalinaBas();
            pedal.GazPedalinaBas();
            pedal.FrenPedalinaBas();
            



            FarKumandaKolu fk = new FarKumandaKolu(); // Far Kumanda Kolu Nesnesi Oluşturuldu.

            fk.FarAc = Farlar.Far.UzunFarlar; // Aracın Farları Açılıyor.

            SinyalKumandaKolu kol = new SinyalKumandaKolu(); // Sinyal Kumanda Kolu Nesnesi Oluşturuldu.

            kol.FarAc = SinyalLambalari.SinyalLambasi.SagSinyal; // Sinyal Lambaları Açıldı.

            Motor motortipi = new Motor(); // Motor Nesnesi Oluşturuldu.
            motortipi.MotorTipi = Motor.motortipi.benzin; // Motor Tipini Seçiyoruz.


            KontakAnahtari kontak = new KontakAnahtari(); // Kontak Anahtarı Nesnesi Oluşturuldu.
            kontak.kontakdurumu = KontakAnahtari.KontakDurum.Acik; //Kontak Açıldı.

            Direksiyon direksiyon = new Direksiyon(); // Direksiyon Nesnesi Oluşturuldu.
            
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SagaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SagaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SagaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SolaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SolaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SolaDondur;
            direksiyon.DireksiyonYon = Direksiyon.DireksiyonYonu.SolaDondur;


            ElektronikBeyin b = new ElektronikBeyin(motortipi.MotorTipi,kontak.kontakdurumu);
          
           // Console.WriteLine(HizGostergesi.hiz);
           
            Console.ReadKey();
        }
    }
}
