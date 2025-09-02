# 📚 Web API com .NET 8 e SQL Server - Repositório de Estudo

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

**Projeto para aprendizado - API com .NET 8 e SQL Server**

[🚀 Começar](#-configuração-rápida) • [📖 Documentação](#-documentação-da-api) • [🔧 Tecnologias](#-tecnologias-utilizadas)

</div>

---

## 📋 Sobre o Projeto

Este repositório foi criado para **fins de estudo e aprendizado** de desenvolvimento de APIs Web utilizando **.NET 8** e **SQL Server**. O projeto implementa uma API REST completa para gerenciamento de uma biblioteca, demonstrando conceitos fundamentais de desenvolvimento backend moderno.

### 🎯 **Objetivos de Aprendizado**

- ✅ **Arquitetura em Camadas** - Separação clara de responsabilidades
- ✅ **Entity Framework Core 8** - ORM moderno para acesso a dados
- ✅ **API REST** - Endpoints seguindo padrões HTTP
- ✅ **Injeção de Dependência** - DI container nativo do .NET
- ✅ **Async/Await** - Programação assíncrona para performance
- ✅ **DTOs** - Padrão Data Transfer Object
- ✅ **Swagger/OpenAPI** - Documentação automática da API
- ✅ **Migrations** - Controle de versão do banco de dados

---

## 🏗️ Arquitetura do Projeto

O projeto segue uma **arquitetura em camadas** bem definida, preparada para evolução futura:

```
📁 WebApi/
├── 📁 Application/          # Camada de Aplicação (preparada para futuro)
├── 📁 Domain/              # Camada de Domínio (preparada para futuro)
├── 📁 Infrastructure/      # Camada de Infraestrutura (preparada para futuro)
└── 📁 Web/        # Camada de Apresentação (API)
    ├── 📁 Controllers/     # Controladores da API REST
    ├── 📁 Services/        # Serviços de negócio
    ├── 📁 Models/          # Modelos de domínio
    ├── 📁 DTOs/           # Data Transfer Objects
    ├── 📁 Data/           # Contexto do Entity Framework
    └── 📁 Migrations/     # Migrações do banco de dados
```

### 🔄 **Fluxo de Dados**

```
Controller → Service → DbContext → SQL Server
    ↑                                    ↓
ResponseModel ← Service ← Entity ← Database
```

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Versão | Propósito |
|------------|--------|-----------|
| **.NET 8.0** | 8.0.0 | Framework de desenvolvimento |
| **Entity Framework Core** | 8.0.3 | ORM para acesso a dados |
| **SQL Server** | - | Banco de dados relacional |
| **Swagger/OpenAPI** | 6.6.2 | Documentação da API |
| **C#** | 12.0 | Linguagem de programação |

---

## 📊 Modelos de Dados

### 📖 **AutorModel**
```csharp
public class AutorModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    
    [JsonIgnore] // Evita referência circular
    public ICollection<LivroModel> Livros { get; set; }
}
```

### 📚 **LivroModel**
```csharp
public class LivroModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Titulo { get; set; } = string.Empty;
    public AutorModel Autor { get; set; }
}
```

### 🔄 **ResponseModel<T>**
```csharp
public class ResponseModel<T>
{
    public T? Dados { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
}
```

---

## 🔌 Documentação da API

### 📍 **Base URL**
```
https://localhost:5041/api/v1
```

### 👨‍💼 **Autores** (`/api/v1/Autor`)

| Método HTTP | Endpoint | Descrição | Status Code |
|-------------|----------|-----------|-------------|
| `GET` | `/ListarAutores` | Lista todos os autores | 200, 404 |
| `GET` | `/BuscarAutorPorId/{id}` | Busca autor por ID | 200, 404 |
| `GET` | `/BuscarAutorPorIdLivro/{idLivro}` | Busca autor de um livro | 200, 404 |
| `POST` | `/CriarAutor` | Cria um novo autor | 200, 400 |
| `PUT` | `/EditarAutor/{id}` | Edita um autor existente | 200, 404 |
| `DELETE` | `/ExcluirAutor/{id}` | Remove um autor | 200, 404 |

### 📚 **Livros** (`/api/v1/Livro`)

| Método HTTP | Endpoint | Descrição | Status Code |
|-------------|----------|-----------|-------------|
| `GET` | `/ListarLivros` | Lista todos os livros com autores | 200, 404 |
| `POST` | `/CriarLivro` | Cria um novo livro | 200, 400 |

---

## 📝 DTOs (Data Transfer Objects)

### 👨‍💼 **AutorDTO**
```csharp
public class AutorDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
}
```

### 📚 **LivroDTO**
```csharp
public class LivroDTO
{
    public string Titulo { get; set; } = string.Empty;
    public int AutorId { get; set; }
}
```

---

## 🛠️ Configuração Rápida

### ⚡ **Pré-requisitos**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (local ou Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### 🗄️ **Configuração do Banco de Dados**

1. **Atualize a string de conexão** em `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=biblioteca;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;"
  }
}
```

2. **Execute as migrações**:
```bash
cd WebApi/WebApi
dotnet ef database update
```

### 🚀 **Executando o Projeto**

```bash
# Navegue para o diretório da API
cd WebApi/WebApi

# Restaure as dependências
dotnet restore

# Execute o projeto
dotnet run
```

### 🌐 **Acessos**
- **API**: `https://localhost:5041`
- **Swagger UI**: `https://localhost:5041/swagger`
- **Health Check**: `https://localhost:5041/health`

---

## 🧪 Testando a API

### 📝 **Exemplo de Criação de Autor**

```bash
curl -X POST "https://localhost:5041/api/v1/Autor/CriarAutor" \
     -H "Content-Type: application/json" \
     -d '{
       "nome": "João",
       "sobrenome": "Silva"
     }'
```

**Resposta esperada:**
```json
{
  "dados": [...],
  "mensagem": "Autor criado com sucesso.",
  "status": true
}
```

### 📚 **Exemplo de Criação de Livro**

```bash
curl -X POST "https://localhost:5041/api/v1/Livro/CriarLivro" \
     -H "Content-Type: application/json" \
     -d '{
       "titulo": "Aventuras de João",
       "autorId": 1
     }'
```

### 🔍 **Exemplo de Listagem**

```bash
# Listar autores
curl "https://localhost:5041/api/v1/Autor/ListarAutores"

# Listar livros
curl "https://localhost:5041/api/v1/Livro/ListarLivros"
```

---

## 📚 Conceitos Demonstrados

### 🏗️ **Padrões de Arquitetura**
- **Repository Pattern** - Implementado através dos serviços
- **Dependency Injection** - Uso de interfaces e DI container
- **Service Layer** - Separação de lógica de negócio

### ⚡ **Programação Moderna**
- **Async/Await** - Operações assíncronas com Entity Framework
- **LINQ** - Consultas eficientes ao banco de dados
- **Nullable Reference Types** - Segurança de tipos do C# 12

### 🛡️ **Boas Práticas**
- **Error Handling** - Tratamento padronizado de erros
- **Response Models** - Respostas consistentes da API
- **Data Validation** - Validação através de DTOs
- **Database Relationships** - Relacionamentos EF Core

---

<div align="center">

**🎓 Aprenda, Pratique, Evolua! 🚀**

*Este repositório é mantido para fins educacionais*

[⬆️ Voltar ao topo](#-web-api-com-net-8-e-sql-server---repositório-de-estudo)

</div>
