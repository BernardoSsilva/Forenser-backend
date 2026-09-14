import { Injectable } from '@nestjs/common';
import { IncidentReportRepositoryPort } from '../../domain/ports/incident-report.repository.port';

@Injectable()
export class ListUserIncidentReportsUseCase {
  constructor(private readonly repository: IncidentReportRepositoryPort) {}

  execute(userId: string) {
    return this.repository.findAllByUserId(userId);
  }
}
