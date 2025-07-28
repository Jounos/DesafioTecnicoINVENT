# #DesafioTecnicoINVENT

Entregar em ate 5 dias corridos um dos dois projetos no github:
Criar o processo de CRUD (Create, Read, Update, Delete) com uma api em C#
1. Conexão com banco de dados não é obrigatória, os dados podem ser salvos em memoria, mas se alguma base de dados for utilizada dê preferencia a bases NoSql
2. Utilize padrões de projetos para organização do mesmo
Criar o processo de CRUD (Create, Read, Update, Delete) em um app Angular serverless com Typescript
1. Conexao com banco de dados não é obrigatoria, os dados podem ser salvos no localstorage
2. Utilize as ferramentas que o framework permite na criação das telas e seu comportamento
 
Para os 2 desafios considere o objeto EquipamentoEletronico com os seguintes campos:
1. Nome: Campo obrigatório
2. Tipo: podendo ser, PC, Notebook, Mouse, teclado etc Campo obrigatório
3. Quantidade em estoque: Campo obrigatório
4. Data de inclusão: Campo obrigatório
5. Tem Estoque: Campo somente leitura

Diferencial:
1. Efetuar testes unitarios no projeto do C# e/ou Angular
2. Criar os 2 projetos conectados um como back-end e outro como front-end

O prazo para entrega seria até quarta-feira da próxima semana 

# Implementação

Implementado um processo CRUD utilizando .NET 9 no backend e angular 20 no frontend

# Aprimoração

Aprimorar a qualidade de sua API .NET, adicionando conexão com o banco de 
dados e introduzindo validações e testes unitários. Em paralelo, iniciar o 
aprendizado de uma nova tecnologia de front-end SAPUI5 através do tutorial 
oficial.

## Conexão com o banco de dados

Para efetuar essa conexão dê preferência para uma base noSql  
Exemplos: **RavenDB**

## Validação com FluentValidation

Leia a documentação oficial do **FluentValidation**: [FluentValidation -- FluentValidation documantation](https://docs.fluentvalidation.net/en/latest/)  
Crie suas validações baseados nas regras do seu modelo de dados.

## Testes Unitários com xUnit

Crie seu projeto de testes: **xUnit Test Project**  

Entenda os conceitos de testes unitários e a estrutura "Arrange-Act-Assert" AAA. Veja a documentação do **xUnit**: [Home](https://xunit.net/?tabs=cs)

Garanta a cobertura de testes para cada regra de validação do modelo. É 
fundamental validar tanto os cenários de sucesso (dados válidos) quanto os de 
falha (dados inválidos para cada regra).