<img src="https://i.imgur.com/8UTMM6E.png"/>

# Projeto Bootcamp PneuStore - Back-End RESTful API

API criada para o Projeto de Bootcamp da <a href="https://blueedtech.com.br">Blue Edtech</a> em parceria com a <a href="https://www.pneustore.com.br">PneuStore</a> sobre o re-design da página de compra. Essa API tem como o objeto alimentar o <a href="https://github.com/Caioferrari04/pneustore-front">interface gráfico</a> desenvolvido com informações guardadas no banco de dados. </br>

Documentação do postman pode ser acessada clicando no botão abaixo ou clicando <a href="https://documenter.getpostman.com/view/17178267/UV5XidNW#7af0a7de-91ba-483f-9e25-a73cfbd413f2">aqui</a>:</br>
[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/17178267-11e6eafe-442d-4baa-ba85-1116749023c4?action=collection%2Ffork&collection-url=entityId%3D17178267-11e6eafe-442d-4baa-ba85-1116749023c4%26entityType%3Dcollection)

## Como utilizar

Para utilizar essa API, há duas alternativas: utilizando da nuvem e utilizando localmente. Para utilizar na nuvem, basta utilizar a rota: https://pneustoreapi.azurewebsites.net e acessar qualquer um dos endpoints presentes na API, com os detalhes e a lista de todos eles podendo ser vista <a href="https://documenter.getpostman.com/view/17178267/UV5XidNW#7af0a7de-91ba-483f-9e25-a73cfbd413f2">aqui</a>. É recomendado que se baixe o postman (<a href="https://www.postman.com">aqui</a>) para fazer as requisições.

## Modelos de requisição

### Usuário:
```json
{
    "userName": "string",
    "passwordHash": "string",
    "isAnonymous": boolean,
    "ip": "string"
}
```

### Carrinho - Adicionar:

```json
{
  "quantity": int,
  "productId": int
}
```

### Carrinho - Atualizar:

```json
{
  "quantity": int,
  "productId": int,
  "userId": "string"
}
```

### Estoque:

```json
{
  "quantity": int,
  "productId": int,
  "estabelecimentoId": int
}
```

### Estabelecimento:

```json
{
    "nome": "string",
    "endereco": "string",
    "imagemUrl": "string"
}
```

### Estabelecimento - Atualizar:

```json
{
    "id": int,
    "nome": "string",
    "endereco": "string",
    "imagemUrl": "string"
}
```

### Cupom:

```json
{
    "nome": "string",
    "desconto": double
}
```

## Dependências:

<a href="https://dotnet.microsoft.com/download">.NET 5.0</a></br>
ASP.NET Core 5.0 </br>
<a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore">Microsoft.EntityFrameworkCore</a></br>
<a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/">Microsoft.EntityFrameworkCore.SqlServer</a></br>
<a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools">Microsoft.EntityFrameworkCore.Tools</a></br>
<a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Identity/">Microsoft.AspNetCore.Identity</a></br>
<a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore">Microsoft.AspNetCore.Identity.EntityFrameworkCore</a></br>
<a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.UI">Microsoft.AspNetCore.Identity.UI</a></br>
<a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore/">Swashbuckle.AspNetCore</a></br>
<a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer">Microsoft.AspNetCore.Authentication.JwtBearer</a>
