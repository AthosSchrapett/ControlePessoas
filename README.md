# ControlePessoas

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![Angular](https://img.shields.io/badge/Angular-19-red)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)

Sistema de cadastro, listagem e controle de pessoas com suporte a filtros e paginação. Desenvolvido com arquitetura em camadas utilizando .NET e Angular, seguindo boas práticas como validação com FluentValidation, tratamento de erros via middleware, e separação de responsabilidades.

---

## 🚀 Tecnologias Utilizadas

### 🔧 Backend (.NET)
- ASP.NET Core 9
- Entity Framework Core
- FluentValidation
- SQL Server
- Serilog
- ElasticSearch
- Middleware global para tratamento de exceções
- Middleware global para captura do logs
- Paginação e filtros no backend com resposta estruturada
- Testes utilizando o XUnit

### 🎨 Frontend (Angular)
- Angular 19
- Angular Material
- Reactive Forms
- HttpClient
---

## 📦 Estrutura da API

```txt
ControlePessoas/
├── API/                    # Camada de apresentação
├── Application/            # Serviços, mapeamentos e validators
├── Domain/                 # Entidades, interfaces e exceptions
├── Infra/                  # Persistência e Unit of Work
├── Tests/                  # Testes Unitarios
```

---

## ✅ Funcionalidades

- Cadastro e edição de pessoas
- Validações com mensagens automáticas
- Filtro por tipo de pessoa (idoso, não-idoso)
- Paginação backend com total de registros e páginas
- Modal de confirmação para exclusões
- Feedbacks com `MatSnackBar` e mensagens personalizadas da API

---

## 📦 Exemplo de resposta paginada

```json
{
  "itens": [ ... ],
  "totalRegistros": 35,
  "paginaAtual": 1,
  "totalPaginas": 4
}
```

---

## 📫 Contribuições

Contribuições são bem-vindas!  
Sinta-se à vontade para abrir uma issue ou enviar um PR.

---

## 📝 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
