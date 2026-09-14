export type IncidentReportType =
  'TRAFFIC_ACCIDENT' | 'THEFT' | 'DOMESTIC_VIOLENCE';
export type LocationType = 'RAILWAY' | 'PUBLIC_ROAD' | 'OTHER';

export interface IncidentReportFace {
  id: string;
  imageUrl: string;
  description: string;
}

export class IncidentReportEntity {
  constructor(
    public readonly id: string,
    public readonly type: IncidentReportType,
    public readonly occurredAt: Date,
    public readonly occurredTime: string,
    public readonly locationType: LocationType,
    public readonly address: string,
    public readonly informantName: string,
    public readonly narrative: string,
    public readonly userId: string,
    public readonly createdAt: Date,
    public readonly driverName: string | null,
    public readonly vehiclesInvolved: string | null,
    public readonly propertyTaken: boolean | null,
    public readonly stolenItems: string | null,
    public readonly involvedViolence: boolean | null,
    public readonly victimName: string | null,
    public readonly face: IncidentReportFace | null,
  ) {}
}
