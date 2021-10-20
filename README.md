<img src="https://i.imgur.com/8UTMM6E.png"/>

# Projeto Bootcamp PneuStore - Back-End REST API

API REST em C# com ASP.NET Core criada para o Projeto de Bootcamp da <a href="https://blueedtech.com.br">Blue Edtech</a> em parceria com a <a href="https://www.pneustore.com.br">PneuStore</a> sobre o re-design da página de compra. Essa API tem como o objeto alimentar o <a href="https://github.com/Caioferrari04/pneustore-front">interface gráfico</a> desenvolvido com informações guardadas no banco de dados. </br>

Documentação do postman pode ser acessada clicando no botão abaixo ou clicando <a href="https://documenter.getpostman.com/view/17178267/UV5XidNW#7af0a7de-91ba-483f-9e25-a73cfbd413f2">aqui</a>:</br>
[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/17178267-11e6eafe-442d-4baa-ba85-1116749023c4?action=collection%2Ffork&collection-url=entityId%3D17178267-11e6eafe-442d-4baa-ba85-1116749023c4%26entityType%3Dcollection)

## Como instalar

Para utilizar essa API, há duas alternativas: utilizando da nuvem e utilizando localmente. Para utilizar na nuvem, basta utilizar a rota: https://pneustoreapi.azurewebsites.net e acessar qualquer um dos endpoints presentes na API, com os detalhes e a lista de todos eles podendo ser vista <a href="https://documenter.getpostman.com/view/17178267/UV5XidNW">aqui</a>. É recomendado que se baixe o postman (<a href="https://www.postman.com">aqui</a>) para fazer as requisições.

Caso queira utilizar localmente, é necessário que baixe e instale o <a href="https://visualstudio.microsoft.com/pt-br/vs/community/">Visual Studio</a>, e o <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads">SQL Server para desenvolvedores</a>. Assim que haver ambos instalados, é necessário que seja alterado a string de conexão localizada no arquivo "appsettings.json" para a string de conexão presente de um dos servers criados pelo SQL Server, e executar por meio do IIS Express.

Após executado, é necessário o uso de algum API Client, como o <a href="https://www.postman.com">Postman</a> para acessar os conteúdos da API. A documentação das rotas e a lista de cada rota está presente <a href="https://documenter.getpostman.com/view/17178267/UV5XidNW">aqui</a>, bastando mudar a rota para o seu localhost.

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
## Participantes do Projeto:

<a href="https://github.com/kindbiscuit">Pedro Henrique</a></br>
<a href="https://github.com/JanioMartins">Janio Martins</a></br>
<a href="https://github.com/MarkoPedriali">Marko Pedriali</a></br>
<a href="https://github.com/AndersonRFerreira">Anderson Ferreira</a></br>
<a href="https://github.com/Caioferrari04">Caio Ferrari</a></br>
<a href="https://github.com/jonathan-sarmento">Jonathan Souza</a>

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
