# Vault

Sistema desktop para gerenciamento de uma biblioteca pessoal de filmes e séries.

O **Vault** foi desenvolvido com o objetivo de centralizar e organizar uma biblioteca pessoal de mídia em uma aplicação desktop simples, rápida e totalmente local. O sistema permite cadastrar filmes e séries, organizá-los em franquias, registrar avaliações, controlar o status de acompanhamento e visualizar uma visão geral da biblioteca por meio de um Dashboard.

A aplicação foi desenvolvida em **C# utilizando Windows Forms e arquitetura em camadas**, mantendo uma separação entre a interface do usuário, as regras de negócio, o acesso aos dados e os modelos da aplicação.

Para a persistência das informações, o Vault utiliza **arquivos JSON**, armazenados no diretório de dados do usuário do Windows. Dessa forma, os dados permanecem disponíveis localmente, sem a necessidade de um banco de dados ou servidor externo.

O sistema oferece uma experiência prática para gerenciar filmes e séries, permitindo acompanhar quais conteúdos já foram assistidos, quais ainda estão em andamento e manter um histórico organizado da biblioteca pessoal.


---

## Funcionalidades

### Dashboard

O Dashboard apresenta uma visão geral da biblioteca, incluindo:

- Quantidade total de filmes;
- Quantidade total de séries;
- Quantidade total de franquias;
- Filmes favoritos;
- Séries favoritas;
- Melhores avaliações.

O Dashboard é carregado automaticamente ao iniciar o sistema.

### Filmes

Permite:

- Cadastrar filmes;
- Editar filmes;
- Excluir filmes;
- Pesquisar filmes;
- Registrar avaliação;
- Marcar filme como assistido;
- Marcar filme como favorito;
- Associar filme a uma franquia;
- Visualizar informações dos filmes em uma tabela;
- Ordenar e consultar os conteúdos cadastrados.

### Séries

Permite:

- Cadastrar séries;
- Editar séries;
- Excluir séries;
- Pesquisar séries;
- Informar quantidade de temporadas;
- Registrar avaliação;
- Marcar série como assistida;
- Marcar série como finalizada;
- Associar série a uma franquia;
- Identificar séries em andamento;
- Identificar séries finalizadas;
- Gerenciar séries favoritas.

### Franquias

Permite:

- Cadastrar franquias;
- Editar franquias;
- Excluir franquias;
- Pesquisar franquias;
- Associar filmes a franquias;
- Associar séries a franquias;
- Visualizar a quantidade de filmes e séries associados.

O sistema impede a exclusão de uma franquia enquanto existirem filmes ou séries vinculados a ela.

---

## Regras de negócio

O Vault possui algumas validações implementadas na camada de serviços.

### Filmes

Filmes podem ser marcados como favoritos de acordo com sua avaliação.

Os favoritos são obtidos utilizando a avaliação como critério principal, com ordenação decrescente.

### Séries

Uma série finalizada precisa estar marcada como assistida.

O sistema também diferencia séries assistidas e não finalizadas, permitindo identificar conteúdos em andamento.

### Franquias

Os nomes das franquias são tratados de forma padronizada para evitar duplicidades.

Uma franquia não pode ser excluída enquanto possuir filmes ou séries associados.

---

## Arquitetura

O projeto utiliza uma arquitetura em camadas:

```text
┌───────────────────────────────┐
│          Interface            │
│       Windows Forms           │
│                               │
│  FormPrincipal / Telas /      │
│  Formulários de cadastro      │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│           Services            │
│                               │
│ Regras de negócio e operações │
│ de cada entidade              │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│         Repositories          │
│                               │
│ Persistência e leitura dos    │
│ dados                         │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│             JSON              │
│                               │
│ filmes.json                   │
│ series.json                   │
│ franquias.json                │
└───────────────────────────────┘
