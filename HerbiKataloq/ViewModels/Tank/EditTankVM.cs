namespace HerbiKataloq.ViewModels.Tank
{
    public class EditTankVM
    {
        public string Id { get; set; } = null!;
        public string Adi { get; set; } = null!;
        public string Sinfi { get; set; } = null!;
        public string Zirehi { get; set; } = null!;
        public double YanacaqTutumu { get; set; }
        public double PersonalSayi { get; set; }
        public IstehsalciVM Istehsalci { get; set; } = null!;
        public OlcuVM Olculeri { get; set; } = null!;
        public TankQabiliyyetleriVM Qabiliyyetleri { get; set; } = null!;
        public TankMuherrikiVM Muherriki { get; set; } = null!;
        public List<TankSilahiVM> Silahlari { get; set; } = null!;
        public byte[] Foto { get; set; } = null!;
        public byte[] FotoDublikat { get; set; } = null!;
        public string Qeyd { get; set; } = null!;
        public IFormFile FotoFile { get; set; } = null!;
    }
}
