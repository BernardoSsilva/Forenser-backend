# Forenser API

Backend do **Forenser**, portal digital de serviços da polícia civil. Reescrito em **NestJS**
seguindo os princípios de **Clean Architecture**, substituindo a versão anterior (Express +
acesso direto ao banco via `mysql`), disponível para referência na branch `Old-Code`.

## Stack

- **NestJS 10** + TypeScript
- **Prisma** + PostgreSQL
- **Passport JWT** + **bcrypt** para autenticação
- **OpenAI SDK** (`dall-e-3`) para geração de retratos falados
- **class-validator** / **class-transformer** para validação de DTOs
- **Swagger** (`/docs`) para documentação interativa da API
- **Jest** para testes unitários

## Arquitetura

Cada módulo de domínio é organizado em três camadas independentes:

```
src/modules/<modulo>/
├── domain/            # entidades, portas (interfaces) e erros de negócio — não depende de nada externo
├── application/        # casos de uso (regras de aplicação) e DTOs — depende apenas do domain
└── infrastructure/      # controllers, repositórios Prisma e integrações externas — implementa as portas do domain
```

A regra de dependência é sempre de fora para dentro: `infrastructure` implementa contratos
definidos em `domain`, e é injetada via Nest DI (`{ provide: PortoAbstrato, useClass:
ImplementacaoConcreta }`). Os casos de uso nunca conhecem Prisma, Express ou qualquer detalhe de
infraestrutura diretamente — o que os torna testáveis com mocks simples, sem subir banco ou
framework (veja `src/modules/auth/application/use-cases/*.spec.ts`).

```mermaid
flowchart LR
    subgraph Infra["infrastructure"]
        C[Controller] --> R[Repositório Prisma / Provider externo]
    end
    subgraph App["application"]
        UC[Caso de uso]
    end
    subgraph Dom["domain"]
        P[Porta - interface]
        E[Entidade]
    end

    C --> UC
    UC --> P
    R -. implementa .-> P
    UC --> E
```

### Estrutura de pastas

```
src/
├── main.ts                  # bootstrap: Helmet, CORS, ValidationPipe, Swagger
├── app.module.ts
├── shared/
│   ├── config/                # carregamento e validação de variáveis de ambiente
│   ├── database/               # PrismaService / PrismaModule (global)
│   ├── decorators/               # @CurrentUser()
│   ├── filters/                   # AllExceptionsFilter
│   └── guards/                     # JwtAuthGuard
└── modules/
    ├── auth/
    ├── users/
    ├── incident-reports/
    ├── faces/
    ├── complaints/
    └── appointments/
```

### Módulos

| Módulo | Responsabilidade |
| --- | --- |
| `auth` | Registro e login, hashing de senha (bcrypt) e emissão de JWT |
| `users` | Perfil do usuário autenticado (consulta, edição, exclusão) |
| `incident-reports` | Boletins de ocorrência: acidente de trânsito, roubo/furto e violência doméstica |
| `faces` | Geração de retrato falado via IA (DALL·E 3) e vínculo com um boletim |
| `complaints` | Denúncias anônimas |
| `appointments` | Agendamento de atendimento presencial (CRUD) |

### Modelo de domínio

```mermaid
erDiagram
    User ||--o{ IncidentReport : registra
    User ||--o{ Appointment : agenda
    IncidentReport ||--o| Face : "pode ter"

    User {
        string id
        string name
        string email
        string passwordHash
        string phoneNumber
        string cpf
        enum sex
        datetime birthDate
    }
    IncidentReport {
        string id
        enum type
        datetime occurredAt
        string occurredTime
        enum locationType
        string address
        string informantName
        string narrative
    }
    Face {
        string id
        string imageUrl
        string description
    }
    Appointment {
        string id
        string requesterName
        datetime scheduledDate
        string scheduledTime
    }
    Complaint {
        string id
        string reporterName
        string location
        string description
    }
```

`Complaint` não tem relação com `User`: denúncias são anônimas e não exigem autenticação.

## Referência da API

Todas as rotas (exceto `auth/*` e `complaints`) exigem o header `Authorization: Bearer <token>`
obtido em `/auth/login`. A documentação interativa (Swagger) fica em `/docs` com a aplicação
rodando.

| Método | Rota | Autenticado | Descrição |
| --- | --- | :---: | --- |
| `POST` | `/auth/register` | não | Cria um novo usuário |
| `POST` | `/auth/login` | não | Autentica e retorna `{ accessToken, user }` |
| `GET` | `/users/me` | sim | Perfil do usuário autenticado |
| `PATCH` | `/users/me` | sim | Atualiza e-mail e/ou telefone |
| `DELETE` | `/users/me` | sim | Exclui a conta do usuário |
| `POST` | `/incident-reports/traffic-accidents` | sim | Registra acidente de trânsito sem vítima |
| `POST` | `/incident-reports/thefts` | sim | Registra roubo ou furto |
| `POST` | `/incident-reports/domestic-violence` | sim | Registra caso de violência doméstica |
| `GET` | `/incident-reports` | sim | Lista os boletins do usuário autenticado |
| `GET` | `/incident-reports/:id` | sim | Detalha um boletim (precisa pertencer ao usuário) |
| `POST` | `/faces/generate` | sim | Gera uma imagem via DALL·E 3 a partir de atributos faciais (não persiste) |
| `POST` | `/faces` | sim | Salva a imagem gerada e vincula a um boletim |
| `POST` | `/complaints` | não | Registra uma denúncia anônima |
| `POST` | `/appointments` | sim | Cria um agendamento |
| `GET` | `/appointments` | sim | Lista os agendamentos do usuário |
| `PATCH` | `/appointments/:id` | sim | Edita um agendamento (precisa pertencer ao usuário) |
| `DELETE` | `/appointments/:id` | sim | Exclui um agendamento (precisa pertencer ao usuário) |

## Rodando localmente

Pré-requisitos: Node.js 20+, Docker (ou um PostgreSQL local) e uma chave de API da OpenAI com
acesso ao modelo `dall-e-3`.

```bash
cp .env.example .env          # preencha os valores reais (veja a tabela abaixo)
docker compose up -d          # sobe um Postgres local em :5432
npm install
npm run prisma:migrate        # cria/atualiza o schema do banco a partir de prisma/schema.prisma
npm run start:dev
```

A API sobe em `http://localhost:3001` e a documentação Swagger em `http://localhost:3001/docs`.

### Variáveis de ambiente

Veja também [`.env.example`](./.env.example).

| Variável | Obrigatória | Descrição |
| --- | :---: | --- |
| `DATABASE_URL` | sim | Connection string do PostgreSQL (`postgresql://usuario:senha@host:porta/banco?schema=public`) |
| `JWT_SECRET` | sim | Segredo usado para assinar e validar os tokens JWT. Use um valor longo e aleatório em produção |
| `JWT_EXPIRES_IN` | não (padrão `1h`) | Validade do token, no formato aceito pelo [`ms`](https://github.com/vercel/ms) (ex.: `1h`, `7d`) |
| `OPENAI_API_KEY` | sim | Chave da [API da OpenAI](https://platform.openai.com/api-keys) usada para gerar os retratos falados com `dall-e-3` |
| `PORT` | não (padrão `3001`) | Porta em que a API HTTP escuta |
| `CORS_ORIGIN` | não (padrão: libera qualquer origem) | Origem(ns) do frontend autorizadas a chamar a API (ex.: `http://localhost:3000`), separadas por vírgula. Recomendado em produção |

## Testes

```bash
npm run test        # testes unitários (casos de uso com repositórios/providers mockados)
npm run test:cov     # com relatório de cobertura
```

> O script `test:e2e` do `package.json` ainda não tem uma suíte configurada (`test/jest-e2e.json`)
> — os testes atuais cobrem os casos de uso na camada `application` com portas mockadas.

## Scripts úteis

```bash
npm run start:dev       # servidor com hot-reload
npm run build           # build de produção (dist/)
npm run lint             # eslint --fix
npm run prisma:studio    # interface visual do banco
npm run prisma:deploy    # aplica migrações pendentes (uso em produção/CI)
```
