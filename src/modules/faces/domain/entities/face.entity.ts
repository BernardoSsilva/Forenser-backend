export class FaceEntity {
  constructor(
    public readonly id: string,
    public readonly imageUrl: string,
    public readonly description: string,
    public readonly incidentReportId: string,
    public readonly createdAt: Date,
  ) {}
}
