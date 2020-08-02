using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{

    public class Empresa
    {


        //Dados da empresa Create
        public Empresa(
           int idUsuario,
           string nomeFantasia,
           string descricao,
           string nomeResponsavel,
           string telefone,
           string email,
           Documento documento,
           string seguimento,
           string horario,
           string facebook,
           string website,
           string instagram,
           string delivery,
           string bairro,
           string rua,
           string numero,
           string cep,
           string estado,
           string complemento,
           string logo,
           string cidade
           )

        {
            IdUsuario = idUsuario;
            Documento = documento;
            NomeFantasia = nomeFantasia;
            Descricao = descricao;
            NomeResponsavel = nomeResponsavel;
            Email = email;
            Telefone = telefone;
            Seguimento = seguimento;
            Horario = horario;
            Facebook = facebook;
            Instagram = instagram;
            Delivery = delivery;
            Logo = logo;
            Website = website;
            Rua = rua;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            Complemento = complemento;

        }

        //Dados da empresa Edit
        public Empresa(
        int idUsuario,
        string nomeFantasia,
        string descricao,
        string nomeResponsavel,
        string telefone,
        string email,
        string seguimento,
        string horario,
        string facebook,
        string website,
        string instagram,
        string delivery,
        string bairro,
        string rua,
        string numero,
        string cep,
        string estado,
        string complemento,
        string logo,
        string cidade
        )

        {
            IdUsuario = idUsuario;
            NomeFantasia = nomeFantasia;
            Descricao = descricao;
            NomeResponsavel = nomeResponsavel;
            Email = email;
            Telefone = telefone;
            Seguimento = seguimento;
            Horario = horario;
            Facebook = facebook;
            Instagram = instagram;
            Delivery = delivery;
            Logo = logo;
            Website = website;
            Rua = rua;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            Complemento = complemento;

        }




        public Empresa()
        {

        }

        public int ID { get; private set; }
        public int IdUsuario { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Descricao { get; private set; }
        public string NomeResponsavel { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public Documento Documento { get; private set; }
        public string Seguimento { get; private set; }
        public string Horario { get; private set; }
        public string Facebook { get; private set; }
        public string Website { get; private set; }
        public string Instagram { get; private set; }
        public string Delivery { get; private set; }
        public string Bairro { get; private set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Logo { get; private set; }
        public string Complemento { get; private set; }

        public bool validarCadastro { get; set; }


    }



}
