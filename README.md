# Marketplace de Serviços API

Esta API foi desenvolvida para gerenciar o cadastro de prestadores de serviços, especialidades e serviços oferecidos. Criado na linguagem C# e utilizando tecnologias .NET, MySQL e Entity Framework Core, permitindo a validação de dados e integração entre diferentes bancos de dados, garantindo consistência e evitando duplicidade de informações.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **.NET 8**: Framework para desenvolvimento da aplicação.
- **Entity Framework Core (EF Core)**: ORM para mapeamento dos dados no banco.
- **MySQL**: Banco de dados para persistência de informações.
- **Swagger**: Ferramenta para documentação e teste dos endpoints.

## Funcionalidades da API

### 1. **Cadastro de Prestadores**
- **Endpoint**: `POST /api/prestadores/cadastrar`
- **Validações**:
  - O prestador não pode ser cadastrado se já houver um prestador com o mesmo nome, especialidade e serviço.
  - Validações são feitas para garantir que a especialidade e o serviço existem no banco de dados.
- **Exemplo de Requisição**:
    ```json
    {
      "nome": "João Silva",
      "email": "joao@servicos.com",
      "telefone": "123456789",
      "especialidade": "pintor",
      "servico": "pintura_residencial",
      "descricaoServico": "Pintura de casas e apartamentos",
      "precoServico": 500
    }
    ```

### 2. **Cadastro de Especialidades**
- **Endpoint**: `POST /api/especialidades/cadastrar`
- **Validações**:
  - O nome da especialidade não pode já estar cadastrado.
  - A API verifica se o nome da especialidade a ser cadastrada não é igual a um serviço já existente.
- **Exemplo de Requisição**:
    ```json
    {
      "nome": "carpintaria"
    }
    ```

### 3. **Cadastro de Serviços**
- **Endpoint**: `POST /api/servicos/cadastrar`
- **Validações**:
  - O nome do serviço não pode já estar cadastrado.
  - O serviço não pode ter o mesmo nome de uma especialidade já existente.
- **Exemplo de Requisição**:
    ```json
    {
      "nome": "manutencao_hidraulica"
    }
    ```

### 4. **Alterar Cadastro de Prestador**
- **Endpoint**: `PUT /api/prestadores/alterarcadastro/{id}`
- **Validações**:
  - O ID informado na URL deve corresponder ao prestador a ser alterado.
  - Verifica se o nome, especialidade e serviço já existem no banco de dados, evitando duplicidade.
  - O ID deve existir no banco de dados.
- **Exemplo de Requisição**:
    ```json
    {
      "prestadorId": 1,
      "nome": "João Silva",
      "email": "joao@novoservicos.com",
      "telefone": "987654321",
      "especialidade": "encanador",
      "servico": "manutencao_hidraulica",
      "descricaoServico": "Reparo em sistemas hidráulicos",
      "precoServico": 600
    }
    ```

### 5. **Deletar Especialidade**
- **Endpoint**: `DELETE /api/especialidades/excluircadastro/{id}`
- **Validações**:
  - O ID deve existir no banco de dados.
  - Não pode excluir uma especialidade que esteja associada a algum prestador no banco de dados de prestadores.

### 6. **Consulta de Especialidades**
- **Endpoint**: `GET /api/especialidades/especialidade/{nome}`
- **Validações**:
  - Busca por nome ou parte do nome da especialidade.
  - Retorna uma lista de especialidades que correspondem ao critério informado.

## Exemplos de Uso

Aqui estão alguns exemplos de onde a API de **Marketplace de Serviços** pode ser implementada:

### Plataformas de Serviços Locais

Aplicativos ou sites que conectam prestadores de serviços (como encanadores, eletricistas, pintores, etc.) a clientes que procuram por esses serviços em sua região.

**Exemplo**: Uma plataforma onde usuários podem buscar prestadores por especialidade e serviço, solicitar orçamentos e contratar o serviço diretamente.

### Sistemas de Gestão de Prestadores para Empresas

Empresas que terceirizam serviços podem utilizar a API para gerenciar prestadores de serviços contratados e suas especialidades.

**Exemplo**: Empresas de construção civil podem usar a API para manter um banco de dados de prestadores com diferentes especialidades, facilitando a contratação de serviços específicos conforme a demanda.

### Requisitos

- .NET 8 SDK
- MySQL
- Visual Studio (ou outra IDE para desenvolvimento em .NET)

## Documentação

A API conta com documentação gerada pelo **Swagger**, onde é possível testar todos os endpoints:

- Acesse `/swagger` para visualizar e testar os endpoints da API.
