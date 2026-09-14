import { ForbiddenException, Injectable } from '@nestjs/common';
import { IncidentReportRepositoryPort } from '../../domain/ports/incident-report.repository.port';
import { IncidentReportNotFoundError } from '../../domain/errors/incident-report.errors';

@Injectable()
export class GetUserIncidentReportUseCase {
  constructor(private readonly repository: IncidentReportRepositoryPort) {}

  async execute(userId: string, reportId: string) {
    const report = await this.repository.findById(reportId);
    if (!report) {
      throw new ForbiddenException(new IncidentReportNotFoundError().message);
    }
    if (report.userId !== userId) {
      throw new ForbiddenException(new IncidentReportNotFoundError().message);
    }
    return report;
  }
}
