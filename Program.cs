using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nom d'utilisateur: ");
            string username = Console.ReadLine();

            Console.Write("Mot de passe: ");
            string password = ReadPassword();

            // Console.Write("Nom de domaine (ex: monDomaine.local): ");
            // string domain = Console.ReadLine();

            if (AuthenticateUser(domain, username, password))
            {
                Console.WriteLine("\nAuthentification réussie !");
            }
            else
            {
                Console.WriteLine("\nÉchec de l'authentification.");
            }
        }

        private static bool AuthenticateUser(string domain, string username, string password)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, /*domain*/))
                {
                    // Vérifie si l'utilisateur est authentifié
                    return context.ValidateCredentials(username, password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return false;
            }
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
