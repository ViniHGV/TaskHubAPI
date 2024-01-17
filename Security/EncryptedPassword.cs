using System;
using System.Security.Cryptography;
using System.Text;

namespace TaskHubAPI.Security
{
    public class EncryptedPassword
    {
        public static string GeneratedEncryptedPassword(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converte a senha em bytes
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);

                // Calcula o hash SHA-256 da senha
                byte[] hashSenha = sha256.ComputeHash(bytesSenha);

                // Converte o hash em uma string hexadecimal
                string senhaCriptografada = BitConverter.ToString(hashSenha).Replace("-", "").ToLower();

                return senhaCriptografada;
            }
        }
    }
}