# API - Processamento CSV

Efetua a leitura de um arquivo CSV e retorna resultado

## 🚀 Objetivo

A API tem como principal objetivo efetuar a leitra de um arquivo CSV de premiação de filmes e retornar o que tem um intervalo de premiação maior e menor

Consulte **[Implantação](#-implanta%C3%A7%C3%A3o)** para saber como implantar o projeto.

### 📋 Pré-requisitos

O Arquivo a ser processado deve estar no formato CSV com separador ;
O Header da planilha deve estar com os nomes iguais aos abaixo
- year
- title
- studios
- producers
- winner

A coluna year deve conter somente números e não pode estar vázia


### 🔧 Instalação

O Projeto deve ser compilado e publicado em ambiente que rode o .NETCORE / .NETCORE.  Ao rodar o projeto local ele ira criar uma execução local igual a:
https://localhost:44397/swagger/index.html.  Nesta execução irá aparecer a página do swagger para que se possa interagir


## ⚙️ Executando os testes
Os testes podem ser feitos de duas formas:
- Via Swagger https://localhost:44397/swagger/index.html
- Via Postmam  https://localhost:44397/File/Readfilecsv (POST).  No Body na opção form-data deverá ser informada o campo Key da seguinte forma:
- Key = File
- Value = selecionar o arquivo que irá processar

### 🔩 Analise os testes de ponta a ponta
O resultado da execução deverá ser um arquivo json neste formato:
{
    "min": [
        {
            "producer": "Frank Yablans",
            "interval": 2,
            "previousWin": 1987,
            "followingWin": 1989
        }
    ],
    "max": [
        {
            "producer": "Robert R. Weston",
            "interval": 10,
            "previousWin": 1983,
            "followingWin": 1993
        },
        {
            "producer": "Steven Perry and Joel Silver",
            "interval": 10,
            "previousWin": 1990,
            "followingWin": 2000
        }
    ]
}

Dando erro, o mesmo será retornada como mensagem de erro

## 📦 Implantação

Para a implantação o projeto deve ser compilado e publicado no servidor desejado.

## 🛠️ Construído com

Este projeto foi construido com Visual Studio 2019 na linguagem 
* C#

## ✒️ Autores

* **Marcos Antonio Paes**


## 📄 Licença

Este é um projeto de teses.  Sem utilização de licença

