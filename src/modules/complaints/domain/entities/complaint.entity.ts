export class ComplaintEntity {
  constructor(
    public readonly id: string,
    public readonly reporterName: string,
    public readonly location: string,
    public readonly description: string,
    public readonly createdAt: Date,
  ) {}
}
