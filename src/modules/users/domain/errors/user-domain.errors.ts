export class UserAlreadyExistsError extends Error {
  constructor() {
    super('Já existe um usuário cadastrado com este e-mail, telefone ou CPF.');
  }
}

export class UserNotFoundError extends Error {
  constructor() {
    super('Usuário não encontrado.');
  }
}
