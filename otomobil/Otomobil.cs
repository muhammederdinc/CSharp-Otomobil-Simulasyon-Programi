using System;


namespace otomobil
{
    interface IPedal
    {
        void GazPedalinaBas();
        void FrenPedalinaBas();
    }

    interface IOtomobil
    {
        bool CalismaDurumu { get; set; }
    }
    class ElektronikBeyin
    {
        // ************************** Elektronik Beyinde Tüm Parçalar Kontrol Ediliyor **************************
        public ElektronikBeyin(Motor.motortipi m , KontakAnahtari.KontakDurum k)    // Elektronik beyin kurucu metodu motortipi ve kontakdurumu olmak üzere 2 tane parametre almaktadır.
        {
            if (m == Motor.motortipi.benzin)    Motor.BenzlinliMotorHiz();   // Motor tipi kontrol edilip motor tipine göre hız değişimi yapılmıştır(Gaz pedalına her basıldığında Benzinli 10 , Dizel 8 artacak)

            else if (m == Motor.motortipi.dizel)    Motor.DizelMotorHiz();


            if (k == KontakAnahtari.KontakDurum.Acik)   // Kontak anahtarı kontrol ediliyor.
            {
                Console.WriteLine("Kontak Açık\nHız Göstergesi :"+ HizGostergesi.hiz);
                Direksiyon.DireksiyonHareket();
                Tekerlekler.TekerlekDurum();// Direksiyon hareketine göre tekerlekler 5 derece hareket ettiriliyor.
            }
            else if (k == KontakAnahtari.KontakDurum.Kapali && Pedal.GazPedaliBasilmaSayisi-Pedal.FrenPedaliBasilmaSayisi>0)
            {
                Console.WriteLine("!!Araç Hareket Halindeyken Kontak Kapatılamaz...\nKontak Açık");
            }
            else
            {
                Console.WriteLine("Kontak Kapalı\nDireksiyon Simidi Kilitli\nHız Gösgergesi Kapalı");
            }


            Farlar.FarKontrol();    // Farların Durumu Kontrol Ediliyor.
            SinyalLambalari.SinyalLambalariKontrol();   // Sinyal Lambalarının Durumları Kontrol Ediliyor.
            
        }

    }
    
 
    class  Pedal:IPedal
    {
        public static int GazPedaliBasilmaSayisi=0;
        public static int FrenPedaliBasilmaSayisi =0;

        public void GazPedalinaBas()
        {

            GazPedaliBasilmaSayisi++;
        }
        public void FrenPedalinaBas()
        {
            FrenPedaliBasilmaSayisi++;
        }

        
      
    }
    
     class HizGostergesi
    {
        public static int hiz=0;
     
        public static void HizKontrol()
        {
            if (hiz < 0) hiz = 0;
            if (hiz > 220) hiz = 220;
        }
      
       
    }
  
    class Otomobil : IOtomobil
    {
        public bool CalismaDurumu { get; set; }
    }
  
    class Motor
    {
        public enum motortipi { benzin,dizel};
        public  motortipi MotorTipi { get; set; }

        public static void BenzlinliMotorHiz()
        {
            HizGostergesi.hiz = (10 * Pedal.GazPedaliBasilmaSayisi) - Pedal.FrenPedaliBasilmaSayisi * 10;
            HizGostergesi.HizKontrol();
        }

        public static void DizelMotorHiz()
        {
            HizGostergesi.hiz = 8 * Pedal.GazPedaliBasilmaSayisi - Pedal.FrenPedaliBasilmaSayisi * 10;
           HizGostergesi.HizKontrol();
        }

    }

   
    class KontakAnahtari
    {
        public enum KontakDurum { Acik,Kapali};

        public KontakDurum kontakdurumu { get; set; }
    }

    abstract class Farlar
    {
        
        public static Far SecilenFar=Far.Kapali;

       
        public enum Far { UzunFarlar,KisaFarlar,Kapali}

        public Far FarAc {set { SecilenFar = value; } }

        public static void FarKontrol()
        {
            if (SecilenFar == Far.KisaFarlar) Console.WriteLine("Kısa Farlar Açık");
            else if (SecilenFar == Far.UzunFarlar) Console.WriteLine("Uzun Farlar Açık");
            else Console.WriteLine("Farlar Kapalı");
        }

    }

    class FarKumandaKolu:Farlar
    {
        
    }

    abstract class SinyalLambalari
    {
        public static SinyalLambasi SecilenSinyalLambasi = SinyalLambasi.Kapali;


        public enum SinyalLambasi { SolSinyal, SagSinyal, DortluFlasor , Kapali }

        public SinyalLambasi FarAc { set { SecilenSinyalLambasi = value; } }

        public static void SinyalLambalariKontrol()
        {
            if (SecilenSinyalLambasi == SinyalLambasi.SagSinyal) Console.WriteLine("Sağ Sinyal Lambası Çalışıyor");
            else if (SecilenSinyalLambasi == SinyalLambasi.SolSinyal) Console.WriteLine("Sol Sinyal Lambası Çalışıyor");
            else if (SecilenSinyalLambasi == SinyalLambasi.DortluFlasor) Console.WriteLine("Dörtlü Flaşör Açık");
            else Console.WriteLine("Sinyal Lambaları Kapalı");
        }

    }

    class SinyalKumandaKolu:SinyalLambalari
    {

    }
    class Direksiyon
    {
        public static DireksiyonYonu Durum;
        public static int DireksiyonDonusu = 0;
            public enum DireksiyonYonu { SagaDondur , SolaDondur , Duz}
            
            public DireksiyonYonu DireksiyonYon {
            set
            {
                if (value == DireksiyonYonu.SagaDondur && DireksiyonDonusu<9) { Durum = DireksiyonYonu.SagaDondur; DireksiyonDonusu++; /*DireksiyonAcisi +=5; Console.WriteLine(DireksiyonAcisi); */}

                else if (value == DireksiyonYonu.SolaDondur && DireksiyonDonusu>-9) { Durum = DireksiyonYonu.SolaDondur; DireksiyonDonusu--;/* DireksiyonAcisi -= 5; Console.WriteLine(DireksiyonAcisi);*/ } 
            }
            
        }

             public static void DireksiyonHareket()
         {
            if (Direksiyon.Durum == Direksiyon.DireksiyonYonu.SagaDondur) { Tekerlekler.TekerlegiSagaDondur(); }

            else if (Direksiyon.Durum == Direksiyon.DireksiyonYonu.SolaDondur) { Tekerlekler.TekerlegiSolaDondur(); }

         }

    }

    class Tekerlekler
    {
        public static int TekerlekAcilari=0;

        public static void TekerlegiSagaDondur()
        {
            if (TekerlekAcilari < 45)
                TekerlekAcilari = Direksiyon.DireksiyonDonusu * 5;
            //Console.WriteLine("Tekerlek Açısı : " + TekerlekAcilari);
        }
        public static void TekerlegiSolaDondur()
        {
            if (TekerlekAcilari > -45)
                TekerlekAcilari = Direksiyon.DireksiyonDonusu * 5;
           // Console.WriteLine("Tekerlek Açısı : " + TekerlekAcilari);
        }

        public static void TekerlekDurum()
        {
            if (Direksiyon.DireksiyonDonusu > 0) Console.Write("Tekerlekler Sağ Yönde :" + TekerlekAcilari + " Derece\n");
            else if (Direksiyon.DireksiyonDonusu<0)
                Console.Write("Tekerlekler Sol Yönde :" + -1*TekerlekAcilari + " Derece\n");
            else
               Console.Write("Tekerlekler Düz :" + TekerlekAcilari + " Derece\n");
        }


       

    }



}
