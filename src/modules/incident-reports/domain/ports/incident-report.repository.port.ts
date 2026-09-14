import {
  IncidentReportEntity,
  IncidentReportType,
  LocationType,
} from '../entities/incident-report.entity';

export interface CreateIncidentReportData {
  type: IncidentReportType;
  userId: string;
  occurredAt: Date;
  occurredTime: string;
  locationType: LocationType;
  address: string;
  informantName: string;
  narrative: string;
  driverName?: string;
  vehiclesInvolved?: string;
  propertyTaken?: boolean;
  stolenItems?: string;
  involvedViolence?: boolean;
  victimName?: string;
}

export abstract class IncidentReportRepositoryPort {
  abstract create(
    data: CreateIncidentReportData,
  ): Promise<IncidentReportEntity>;
  abstract findById(id: string): Promise<IncidentReportEntity | null>;
  abstract findAllByUserId(userId: string): Promise<IncidentReportEntity[]>;
}
