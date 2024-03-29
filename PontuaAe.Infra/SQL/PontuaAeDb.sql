CREATE DATABASE PONTUAAE
go

USE PONTUAAE

GO
CREATE TABLE USUARIO(
ID int IDENTITY(1,1) PRIMARY KEY,
Email varchar(50) ,
Senha varchar(100) ,
RoleId varchar(30),
Estado bit
);

CREATE TABLE EMPRESA(
ID int IDENTITY(1,1) PRIMARY KEY ,
NomeFantasia varchar(100),
Descricao varchar(300),
NomeResponsavel varchar(50),
Telefone varchar(15),
Email varchar(50),
Documento varchar(18),
Seguimento varchar(25),
Horario varchar(35),
Facebook varchar(60),
WebSite varchar(60),
Instagram varchar(50),
Delivery varchar(30),
IdUsuario int FOREIGN KEY REFERENCES USUARIO(ID),
Bairro varchar(30),
Rua varchar(30),
Numero varchar(20),
Cep varchar(10),
Cidade varchar(28),
Estado varchar(50),
Complemento varchar(70),
Banner varchar(200), 
Logo varchar(200),

);

CREATE TABLE CONFIG_PONTUACAO( 
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Nome varchar(50),
Reais decimal(20,2),
PontosFidelidade decimal(20,2),
ValidadePontos int,
Percentual int,
TipoDeProgramaFidelidade int  
);

CREATE TABLE FUNCIONARIO(
ID int IDENTITY(1,1) PRIMARY KEY,
IdUsuario int FOREIGN KEY REFERENCES USUARIO(ID),
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Contato varchar(11),
NomeCompleto varchar(40)
);

CREATE TABLE CONTA_SMS(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Saldo int,
);

CREATE TABLE CLIENTE(
ID int IDENTITY(1,1) PRIMARY KEY, 
IdUsuario int FOREIGN KEY REFERENCES USUARIO(ID),
NomeCompleto varchar(55),
DataNascimento DateTime,
Sexo varchar(11),
Cidade varchar(33),
Contato varchar(15),
Email varchar(50),
);  

CREATE TABLE PRE_CADASTRO(
ID int IDENTITY(1,1) PRIMARY KEY,
Contato  varchar(11),
);


CREATE TABLE PONTUACAO(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
IdPreCadastro int FOREIGN KEY REFERENCES PRE_CADASTRO(ID),
Saldo decimal(20,2),
SaldoTransacao decimal(20,2),
DataVisita DateTime,
Validade DateTime,
Segmentacao varchar(40),   
--Estado bit,  --REMOVER ESTE ATRIBUTO
SegCustomizado varchar(40)
);


CREATE TABLE PREMIOS(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Nome varchar(40),
Texto varchar(250),
Quantidade int,
Imagem varchar(200),
PontosNecessario decimal(20,2),  

);

CREATE TABLE OFERTAS(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Nome varchar(30),
Descricao varchar(300),
Imagem varchar(200),
Validade varchar(19),
);

CREATE TABLE RECEITA(
ID int IDENTITY(1,1) PRIMARY KEY,
IdUsuario int FOREIGN KEY REFERENCES USUARIO(ID),
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
IdPontuacao int FOREIGN KEY REFERENCES PONTUACAO(ID),  
Valor decimal(20,2), 
DataVenda datetime,
TipoAtividade varchar(20), 
);

CREATE TABLE CONFIG_CLASSIFICACAO_CLIENTE(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
QtdVisitasClassificacaoOuro int,
QtdVisitasClassificacaoPrata int,
QtdVisitasClassificacaoBronze int,
QtdVisitaClassificacaoAtivo int,

TempoEmDiasClienteOuro int,
TempoEmDiasClientePrata int,
TempoEmDiasClienteBronze int,
TempoEmDiasClientePedido int,
TempoEmDiasClienteInativo int
);

CREATE TABLE MENSAGEM(
ID int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
Nome varchar(40),
TipoAutomacao varchar(30),
Segmentacao varchar(30),
SegCustomizado varchar(10),
MeioComunicacao int,
DiasAntesAniversario int, 
TempoPorDiaDaSemana varchar(15),
TempoPorDiaDoMes int,
TempoPorDia int,
DataEnvio varchar(40), 
HoraEnvio varchar(10),
DataEnviada datetime,  
Conteudo varchar(180),
QtdSelecionado int, 
ValorInvestido decimal(20,2),
QtdEnviada int,
EstadoEnvio varchar(30), 
Estado bit,--ativa e desativa automacao
StatusAutomacao varchar(10) --estou adicinando esse um atributo identifica as campanhas automaticas
);

CREATE TABLE SITUACAO_SMS(
ID int IDENTITY(1,1) PRIMARY KEY,
IdMensagem int FOREIGN KEY REFERENCES MENSAGEM(ID),
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(ID),
IdSMS int,
Estado varchar(10),
DataRecebida Datetime, 
DataCompra  DateTime,
ValorRecebido decimal(20,2), 
Contatos varchar(11),
);


CREATE TABLE REGISTRO_RESGATE(
ID INT IDENTITY(1,1) PRIMARY KEY,
IdPreCadastro int FOREIGN key REFERENCES PRE_CADASTRO(ID),
IdEmpresa int FOREIGN key REFERENCES EMPRESA(ID),
IdUsuario int FOREIGN KEY REFERENCES USUARIO(ID),
PontoResgatado int,
DataResgate DATETIME

);




   --comandos adicionar e remover colunas 
-- ALTER TABLE dbo.FUNCIONARIO ADD Contato varchar(11);
-- ALTER TABLE dbo.CLIENTE ADD DataNascimeto varchar(20);


--  ALTER TABLE dbo.MENSAGEM DROP COLUMN StatusAutomacao ;
-- ALTER TABLE dbo.MENSAGEM DROP COLUMN DataEnviada ;


--comandos selects   e delete

-- SELECT  * FROM USUARIO
-- SELECT * FROM EMPRESA

-- SELECT * FROM RECEITA
-- SELECT * FROM CONFIG_PONTUACAO
-- SELECT * from PREMIOS

-- SELECT * FROM MENSAGEM
-- SELECT * FROM SITUACAO_SMS
-- SELECT * FROM PRE_CADASTRO
-- select * from PONTUACAO

-- DELETE FROM MENSAGEM
-- DELETE FROM SITUACAO_SMS


-- DELETE FROM PRE_CADASTRO
-- DELETE FROM CLIENTE
-- DELETE FROM PONTUACAO
-- DELETE from RECEITA
-- DELETE FROM CONTA_SMS
-- DELETE  FROM  USUARIO WHERE ID = 14
-- DELETE FROM EMPRESA
-- DELETE FROM REGISTRO_RESGATE
-- DELETE FROM CONFIG_PONTUACAO
-- DELETE FROM PREMIOS
-- DELETE  FROM FUNCIONARIO

