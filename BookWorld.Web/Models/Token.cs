
namespace BookWorld.Web.Models
{
    public static class Token
    {
        //token Runtime'da değişebileceği için static olarak tanımladık.
        //const olarak tanımlasaydık sadece başlangıçta bir değer vermemiz gerekirdi ve
        //Runtime'da bu değeri değiştiremezdik.
        public static string token;
    }
}