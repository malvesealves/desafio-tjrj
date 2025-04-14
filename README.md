# Desafio TJRJ

Este projeto é um sistema CRUD completo para gerenciamento de livros e criação de relatórios, composto por:

- **Backend:** ASP.NET Core 9 com Minimal API, Entity Framework Core e PostgreSQL.
- **Frontend:** Angular 19 com tela de listagem, criação e edição de livros além de criação de relatórios.

---

##  Estrutura do Projeto

```
/back/
│
├── src/ 
    └── API/
├── tests/
    └── Test/

/front/
├── book-management/         
    └── book-catalog-angular/
```

---

##  Como executar o projeto

###  Pré-requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [PostgreSQL](https://www.postgresql.org/)

---

## Configuração do Backend (.NET Core)

1. **Acesse a pasta do backend:**
   ```bash
   cd backend/BookCatalog.API
   ```

2. **Configure a string de conexão no `appsettings.json`:**
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=BookCatalogDb;Username=postgres;Password=yourpassword"
   }
   ```

3. **Rode as migrações e aplique no banco:**
   ```bash
   dotnet ef database update
   ```

4. **Execute a API:**
   ```bash
   dotnet run
   ```

   A API estará disponível em: [https://localhost:5001](https://localhost:5001)

---

##  Configuração do Frontend (Angular)

1. **Acesse a pasta do frontend:**
   ```bash
   cd frontend/book-catalog-angular
   ```

2. **Instale as dependências:**
   ```bash
   npm install
   ```

3. **Configure o endpoint da API em `src/environments/environment.ts`:**
   ```ts
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:5001/api'
   };
   ```

4. **Execute o frontend:**
   ```bash
   ng serve
   ```

   O frontend estará disponível em: [http://localhost:4200](http://localhost:4200)

---

## Rodando os Testes

### Backend

```bash
cd backend/BookCatalog.API.Tests
dotnet test
```

### Frontend

```bash
cd frontend/book-catalog-angular
ng test
```

---

## Seed de Dados

A base já é populada automaticamente com alguns livros no `DbInitializer.cs`. Isso é executado na inicialização da aplicação.

---

## Funcionalidades

- Listagem de livros
- Cadastro de novo livro
- Edição de livro
- Exclusão de livro
- Geração de relatório
- Testes automatizados (xUnit + Jasmine)

   
