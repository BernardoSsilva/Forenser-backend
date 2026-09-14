export class AppointmentEntity {
  constructor(
    public readonly id: string,
    public readonly requesterName: string,
    public readonly scheduledDate: Date,
    public readonly scheduledTime: string,
    public readonly userId: string,
    public readonly createdAt: Date,
  ) {}
}
