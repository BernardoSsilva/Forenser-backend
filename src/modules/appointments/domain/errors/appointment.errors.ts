export class AppointmentNotFoundError extends Error {
  constructor() {
    super('Agendamento não encontrado.');
  }
}
