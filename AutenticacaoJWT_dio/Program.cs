using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleAppAuthentication
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes("suaChaveSecretaComPeloMenos128Bits");

            Console.Write("Digite o nome de usuário: ");
            string username = Console.ReadLine();

            Console.Write("Digite a senha: ");
            string password = Console.ReadLine();

            // Verifica as credenciais
            bool isAuthenticated = Authenticate(username, password);

            if (isAuthenticated)
            {
                // Cria um token JWT
                string token = GenerateJwtToken(username, secretKeyBytes);

                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = tokenHandler.ReadJwtToken(token).Claims;

                Console.WriteLine("Usuário autenticado. Dados do usuário:");
                foreach (var claim in claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }

                // Envio de e-mail
                SendEmail(username);
            }
            else
            {
                Console.WriteLine("Credenciais inválidas. Autenticação falhou.");
            }
        }

        static bool Authenticate(string username, string password)
        {
            return username == "admin" && password == "123";
        }

        static string GenerateJwtToken(string username, byte[] secretKeyBytes)
        {
            var key = new SymmetricSecurityKey(secretKeyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Configuração do token JWT
            var token = new JwtSecurityToken(
                issuer: "aplicacao demo",
                audience: "aplicacao demo",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        static void SendEmail(string recipient)
        {
            // Configurações do servidor SMTP e credenciais
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("willamybragadesenvolvimento@gmail.com", "lmskxaxeqylqykjq"); //caso queira usar o seu => entre no endereço do site https://myaccount.google.com/security => gerar a senha para o app
            smtpClient.EnableSsl = true;

            // Construa o e-mail
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("emaildestino"); //email destino aqui.
            mailMessage.To.Add("emaildestino"); //email destino
            mailMessage.Subject = "Assunto do E-mail";
            mailMessage.Body = "Corpo do E-mail";

            try
            {
                // Envie o e-mail
                smtpClient.Send(mailMessage);
                Console.WriteLine("E-mail enviado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
            }
        }
    }
}
