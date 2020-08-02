using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor
{
    public class Documento: Notifiable
    {
        public Documento(string numero)
        {
            Cnpj = numero;

            AddNotifications(new ValidationContract()
                  .IsTrue(Validar(numero), "Documento", "Inválido")
              );
        }
        public string Cnpj { get; private set; }

        public override string ToString()
        {
            return Cnpj; 
        }

        public static bool Validar(string numero)
        {
            if (numero.Length != 14 && numero.Length != 11)
            {
                return false;

            }

            if (numero.Length == 14)
            {

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                numero = numero.Trim();
                numero = numero.Replace(".", "").Replace("-", "").Replace("/", "");
                if (numero.Length != 14)
                    return false;
                tempCnpj = numero.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return numero.EndsWith(digito);

            } else if (numero.Length == 11)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                numero = numero.Trim();
                numero = numero.Replace(".", "").Replace("-", "");
                tempCpf = numero.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return numero.EndsWith(digito);
            }
            return true;
        }
    }
}
