# 📘 Plano de Contas API

## 🛠️ Tecnologias Utilizadas
Esta API foi desenvolvida utilizando as seguintes tecnologias:

- **Linguagem**: C#
- **Framework**: .NET 8
- **Banco de Dados**: SQL Server
- **ORM**: Entity Framework Core
- **Containerização**: Docker
- **Ferramenta de Teste**: Swagger

---

## 🚀 Passo a Passo para Configurar e Executar a API

### 🔹 1. Clonar o Repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd PlanoDeContasAPI
```

### 🔹 2. Configurar o Banco de Dados (Docker)
Se ainda não tiver um SQL Server rodando, execute o seguinte comando para criar um container com o banco:

```bash
docker run --name sqlserver-plano-contas -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

📌 **Caso já tenha um SQL Server rodando**, altere a `ConnectionString` no arquivo `appsettings.json`.

---

### 🔹 3. Configurar a String de Conexão no `appsettings.json`
No projeto `PlanoDeContas.API`, edite o arquivo `appsettings.json` e defina a conexão com o banco:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=PlanoDeContasDB;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=False"
}
```

---

### 🔹 4. Restaurar Dependências e Compilar o Projeto

Execute os seguintes comandos para restaurar os pacotes e compilar o projeto:

```bash
dotnet restore
dotnet build
```

---

### 🔹 5. Rodar as Migrations para Criar o Banco de Dados

```bash
dotnet ef database update --project PlanoDeContas.Infrastructure --startup-project PlanoDeContas.API
```

📌 **Se precisar criar uma nova migration manualmente:**
```bash
dotnet ef migrations add NomeDaMigration --project PlanoDeContas.Infrastructure --startup-project PlanoDeContas.API
```

---

### 🔹 6. Executar a API
Para rodar a API e acessar os endpoints, utilize:

```bash
dotnet run --project PlanoDeContas.API
```

A API estará disponível em: `http://localhost:5000/swagger`

---

## ✅ Testando a API via Swagger
1. Acesse `http://localhost:5000/swagger`
2. Utilize os endpoints disponíveis para testar as funcionalidades

---

## 🔗 **Endpoints Disponíveis**

### 📌 **Plano de Contas**

#### **Criar uma Conta**
- **`POST /api/PlanoDeConta`**
- **Descrição:** Cria uma nova conta no plano de contas.
- **Body:**
```json
{
  "codigo": "1.1",
  "descricao": "Taxa Condominial",
  "aceitaLancamentos": true,
  "paiId": 1,
  "tipo": 0
}
```

#### **Listar todas as Contas**
- **`GET /api/PlanoDeConta`**
- **Descrição:** Retorna todas as contas cadastradas.

#### **Buscar Conta por ID**
- **`GET /api/PlanoDeConta/{id}`**
- **Descrição:** Retorna os detalhes de uma conta específica pelo seu ID.

#### **Atualizar uma Conta**
- **`PUT /api/PlanoDeConta/{id}`**
- **Descrição:** Atualiza uma conta existente.
- **Body:**
```json
{
  "id": 3,
  "codigo": "1.1.1",
  "descricao": "Dinheiro no caixa",
  "aceitaLancamentos": true,
  "paiId": 2,
  "tipo": 0
}
```

#### **Remover uma Conta**
- **`DELETE /api/PlanoDeConta/{id}`**
- **Descrição:** Remove uma conta do plano de contas.

#### **Obter o Próximo Código Disponível**
- **`GET /api/PlanoDeConta/proximo-codigo/{paiId}`**
- **Descrição:** Retorna o próximo código disponível para uma conta filha do `paiId` informado.

---

## 🛠️ Principais Funcionalidades da API

✅ Cadastro de contas contábeis (`Receita` e `Despesa`)
✅ Sugestão automática do próximo código
✅ Regras de validação de hierarquia de contas
✅ Testes via Swagger e Postman

---

## ❓ Dúvidas ou Problemas
Caso encontre algum problema ao rodar a API, verifique:
- Se o **SQL Server** está rodando corretamente (`docker ps`)
- Se o banco foi criado (`SELECT * FROM sys.databases;` no SQL Server)
- Se há **erros de conexão** no `appsettings.json`

Se precisar de mais suporte, entre em contato! 🚀

