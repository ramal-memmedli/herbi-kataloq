using HerbiKataloq.Models.TeyyareModels;

namespace HerbiKataloq.Common
{
    public static class Helpers
    {
        public static byte[] UploadPhoto(this IFormFile photoFile)
        {
            byte[] photoByteArray;

            using (MemoryStream ms = new MemoryStream())
            {
                photoFile.CopyTo(ms);
                byte[] fotoBayt = ms.ToArray();

                photoByteArray = fotoBayt;
            }

            return photoByteArray;
        }

        public static string ConvertPhotoToBase64(byte[] foto)
        {
            string result = "";

            try
            {
                result = Convert.ToBase64String(foto);
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }
    }
}
