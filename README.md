# Forenser API

Backend do **Forenser**, portal digital de serviços da polícia civil. Reescrito em **NestJS** seguindo os princípios de **Clean Architecture**, substituindo a versão anterior (Express + acesso direto ao banco), disponível para referência na branch `Old-Code`.

## Arquitetura

Cada módulo de domínio é organizado em três camadas independentes:

```
src/modules/<modulo>/
├── domain/          # entidades, portas (interfaces) e erros de negócio — não depende de nada externo
├── application/      # casos de uso (regras de aplicação) e DTOs — depende apenas do domain
└── infrastructure/    # controllers, repositórios Prisma e integrações externas — implementa as portas do domain
```

A regra de dependência é sempre de fora para dentro: `infrastructure` implementa contratos definidos em `domain`, e é injetada via Nest DI (`{ provide: PortoAbstrato, useClass: ImplementacaoConcreta }`). Os casos de uso nunca conhecem Prisma, Express ou qualquer detalhe de infraestrutura diretamente.

### Módulos

| Módulo | Responsabilidade |
| --- | --- |
| `auth` | Registro e login, hashing de senha (bcrypt) e emissão de JWT |
| `users` | Perfil do usuário autenticado (consulta, edição, exclusão) |
| `incident-reports` | Boletins de ocorrência: acidente de trânsito, roubo/furto e violência doméstica |
| `faces` | Geração de retrato falado via IA (DALL·E 3) e vínculo com um boletim |
| `complaints` | Denúncias anônimas |
| `appointments` | Agendamento de atendimento presencial (CRUD) |

## Stack

- **NestJS 10** + TypeScript
- **Prisma** + PostgreSQL
- **Passport JWT** para autenticação
- **OpenAI SDK** (`dall-e-3`) para geração de retratos falados
- **Swagger** (`/docs`) para documentação da API

## Rodando localmente

```bash
cp .env.example .env
docker compose up -d          # sobe o Postgres local
npm install
npm run prisma:migrate        # cria/atualiza o schema do banco
npm run start:dev
```

A API sobe em `http://localhost:3001` e a documentação Swagger em `http://localhost:3001/docs`.

### Variáveis de ambiente

| Variável | Descrição |
| --- | --- |
| `DATABASE_URL` | connection string do PostgreSQL |
| `JWT_SECRET` | segredo usado para assinar os tokens |
| `JWT_EXPIRES_IN` | validade do token (ex.: `1h`) |
| `OPENAI_API_KEY` | chave da API da OpenAI usada para gerar as imagens |
| `CORS_ORIGIN` | origem(s) permitida(s) para CORS (frontend) |

## Testes

```bash
npm run test        # testes unitários
npm run test:e2e     # testes end-to-end
```
