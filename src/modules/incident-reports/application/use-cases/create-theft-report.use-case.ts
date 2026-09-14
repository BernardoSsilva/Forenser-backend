import { Injectable } from '@nestjs/common';
import { IncidentReportRepositoryPort } from '../../domain/ports/incident-report.repository.port';
import { CreateTheftDto } from '../dtos/create-theft.dto';

@Injectable()
export class CreateTheftReportUseCase {
  constructor(private readonly repository: IncidentReportRepositoryPort) {}

  execute(userId: string, dto: CreateTheftDto) {
    return this.repository.create({
      type: 'THEFT',
      userId,
      occurredAt: dto.occurredAt,
      occurredTime: dto.occurredTime,
      locationType: dto.locationType,
      address: dto.address,
      informantName: dto.informantName,
      narrative: dto.narrative,
      involvedViolence: dto.involvedViolence,
      propertyTaken: dto.propertyTaken,
      victimName: dto.victimName,
      stolenItems: dto.stolenItems,
    });
  }
}
