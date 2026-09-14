import { ComplaintEntity } from '../entities/complaint.entity';

export interface CreateComplaintData {
  reporterName: string;
  location: string;
  description: string;
}

export abstract class ComplaintRepositoryPort {
  abstract create(data: CreateComplaintData): Promise<ComplaintEntity>;
}
