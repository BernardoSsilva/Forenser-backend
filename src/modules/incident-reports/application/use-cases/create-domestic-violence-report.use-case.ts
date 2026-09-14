import { Injectable } from '@nestjs/common';
import { IncidentReportRepositoryPort } from '../../domain/ports/incident-report.repository.port';
import { CreateDomesticViolenceDto } from '../dtos/create-domestic-violence.dto';

@Injectable()
export class CreateDomesticViolenceReportUseCase {
  constructor(private readonly repository: IncidentReportRepositoryPort) {}

  execute(userId: string, dto: CreateDomesticViolenceDto) {
    return this.repository.create({
      type: 'DOMESTIC_VIOLENCE',
      userId,
      occurredAt: dto.occurredAt,
      occurredTime: dto.occurredTime,
      locationType: dto.locationType,
      address: dto.address,
      informantName: dto.informantName,
      narrative: dto.narrative,
      involvedViolence: dto.involvedViolence,
      victimName: dto.victimName,
    });
  }
}
