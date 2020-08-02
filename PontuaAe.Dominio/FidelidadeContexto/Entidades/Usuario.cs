using PontuaAe.Compartilhado.Entidades;
using System.Text;
using System;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using FluentValidator;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Usuario :  Notifiable
    {
        public Usuario()
        {

        }

        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public Usuario( int idUsuario,  string email , string senha )
        {
            ID= idUsuario;
            Email = email;
            Senha = EncriptaSenha(senha);
            Estado = true;


        }


        public Usuario(  string email = "", string senha = "", string roleId="")
        {
            
            Email = email;
            Senha = EncriptaSenha(senha);
            RoleId = roleId;
            Estado = true;
        }

        public int ID { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Estado { get; private set; }
        public string RoleId { get; private set; }
        public string Contato { get; private set; }


        public bool Autenticar(string email, string senha)
        {
            if (Email == email && Senha == EncriptaSenha(senha))
                return true;

            AddNotification("Usuario", "Email ou Senha Invalido");
            return false;
        }   

        private void Ativar() => Estado = true;
        public void Desativar() => Estado = false;

        public string EncriptaSenha(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            ///var _senha = (pass += "|2d331cca-f6c0-40c0-bbp3-6e32909c2560");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(pass));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
