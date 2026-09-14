import { Injectable } from '@nestjs/common';
import { IncidentReportRepositoryPort } from '../../domain/ports/incident-report.repository.port';
import { CreateTrafficAccidentDto } from '../dtos/create-traffic-accident.dto';

@Injectable()
export class CreateTrafficAccidentReportUseCase {
  constructor(private readonly repository: IncidentReportRepositoryPort) {}

  execute(userId: string, dto: CreateTrafficAccidentDto) {
    return this.repository.create({
      type: 'TRAFFIC_ACCIDENT',
      userId,
      occurredAt: dto.occurredAt,
      occurredTime: dto.occurredTime,
      locationType: dto.locationType,
      address: dto.address,
      informantName: dto.informantName,
      narrative: dto.narrative,
      driverName: dto.driverName,
      vehiclesInvolved: dto.vehiclesInvolved,
    });
  }
}
