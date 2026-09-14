export class IncidentReportNotFoundError extends Error {
  constructor() {
    super('Boletim de ocorrência não encontrado.');
  }
}
