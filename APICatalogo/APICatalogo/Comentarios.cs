/* Estruturação da API

** exclusão dos arquivos controllers controllers

** criação pasta Models e adição de classes Categoria e Produto

** neste caso vamos usar a abordagem CODE-FIRST
   Podemos realizar o mapeamento das classes e das entidades do dominio e gerar o banco de dados e as tabelas
   Primeiro criamos as entidades e a partir delas geramos o banco e as tabelas(migrations)
   O EF CORE adota algumas convenções para realizar o mapeamento das entidades

** então temos que instalar as referências ao EF CORE e ao provedor POMELO para o MYSQL
   Pomelo.EntityFrameworkCore.MySql
   Microsoft.EntityFrameworkCore.Design
   Microsoft.AspNetCore.OpenAI
   comandos são 
   Package Manager Console - install-package <nome_pacote>
   NET CLI - dotnet add package <nome_pacote>

  além disso precisa instalar 
  Ferramenta EF Core Tools (Microsoft.EntityFrameworkCore.Tools)
  (ferramenta para gerar as migrations)
  dotnet tool install --global dotnet-ef (instalar)
  dotnet tool update --global dotnet-ef (atualizar)
  dotnet ef  (verificar se esta instalada)
  
   Essas instalações podem ser feitas pelo Nuget-solução apenas com alguns clicks

** Criar arquivo de contexto do EF CORE 
   pasta Context > AppDbContext
   esse arquivo de contexto que vai linkar as classes criadas(Categoria e Produto)
   com as tabelas do banco de dados que serão criadas(Categorias e Produtos)
   observe que as tabelas estão no plural e as propriedades no singular

** após criar o contexto ir no appsetting.json e definir qual a string de conexao 
   com banco de dados que será usada para se comunicar com o db

*/