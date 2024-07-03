namespace BaseAPI.Services
{
    public interface IUserService
    {
    /// <summary>
    /// APIYI KULLANAN AKTIF KULLANICIYI TESPIT ETMEK ICIN KULLANILACAK 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
        public int GetUserIdWithToken(string token);

    }
}
