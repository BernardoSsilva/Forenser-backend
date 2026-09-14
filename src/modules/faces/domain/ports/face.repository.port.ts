import { FaceEntity } from '../entities/face.entity';

export interface CreateFaceData {
  imageUrl: string;
  description: string;
  incidentReportId: string;
}

export abstract class FaceRepositoryPort {
  abstract create(data: CreateFaceData): Promise<FaceEntity>;
}
