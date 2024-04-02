namespace HerbiKataloq.ViewModels.Teyyare
{
    public class AddTeyyareVM
    {
        public string Adi { get; set; } = null!;
        public string Tipi { get; set; } = null!;
        public DateTime IlkUcusTarixi { get; set; }
        public bool IstifadesiDavamEdirmi { get; set; }
        public IstehsalciVM Istehsalci { get; set; } = null!;
        public OlcuVM Olculeri { get; set; } = null!;
        public TeyyareQabiliyyetleriVM Qabiliyyetleri { get; set; } = null!;
        public string Qeyd { get; set; } = null!;
        public TeyyareMuherrikiVM Muherriki { get; set; } = null!;
        public List<TeyyareSilahiVM> Silahlari { get; set; } = null!;
        public byte[] Foto { get; set; } = null!;
        public IFormFile FotoFile { get; set; } = null!;
    }
}
