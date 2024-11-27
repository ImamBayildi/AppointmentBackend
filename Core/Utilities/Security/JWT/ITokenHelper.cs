using Core.Entities.Security;


namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper// Genellikle JWT olur ama yinede tanımlayabiliriz, test amaçlı uyduruk token kullanabilirim yada başka teknik kullanabilirim mesela
    {
        AccessToken CreateToken(User user, List<OperationClaim> claims);//Kullanıcının claim'lerini bul ve JWT üret
    }
}
