# WeChip

### 🎲  Pré-requisitos 

Execute esses dois comandos antes de rodar o projeto
```
dotnet ef migrations add InitialMigration1
dotnet ef database update
```

### 🎲  Testar 

#### WebApi

POST /api/Account
```
{
    "Login": "Teste", 
    "Senha": "1234"
}
# retorna um token
```

POST api/WeChips/CadastrarProduto
```
header authorization: Bearer token
{
    "CodigoProduto": 200,
    "Descricao": "Monitor 17'",
    "Preco": 350.00,
    "Tipo": 0
}
```

GET api/WeChips/ConsultarVendas
```
header authorization: Bearer token
```
