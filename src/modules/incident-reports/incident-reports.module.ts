import { Module } from '@nestjs/common';
import { IncidentReportsController } from './infrastructure/incident-reports.controller';
import { PrismaIncidentReportRepository } from './infrastructure/prisma-incident-report.repository';
import { IncidentReportRepositoryPort } from './domain/ports/incident-report.repository.port';
import { CreateTrafficAccidentReportUseCase } from './application/use-cases/create-traffic-accident-report.use-case';
import { CreateTheftReportUseCase } from './application/use-cases/create-theft-report.use-case';
import { CreateDomesticViolenceReportUseCase } from './application/use-cases/create-domestic-violence-report.use-case';
import { ListUserIncidentReportsUseCase } from './application/use-cases/list-user-incident-reports.use-case';
import { GetUserIncidentReportUseCase } from './application/use-cases/get-user-incident-report.use-case';

@Module({
  controllers: [IncidentReportsController],
  providers: [
    {
      provide: IncidentReportRepositoryPort,
      useClass: PrismaIncidentReportRepository,
    },
    CreateTrafficAccidentReportUseCase,
    CreateTheftReportUseCase,
    CreateDomesticViolenceReportUseCase,
    ListUserIncidentReportsUseCase,
    GetUserIncidentReportUseCase,
  ],
  exports: [IncidentReportRepositoryPort],
})
export class IncidentReportsModule {}
