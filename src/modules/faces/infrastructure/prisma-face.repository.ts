import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../../shared/database/prisma.service';
import {
  CreateFaceData,
  FaceRepositoryPort,
} from '../domain/ports/face.repository.port';
import { FaceEntity } from '../domain/entities/face.entity';

@Injectable()
export class PrismaFaceRepository implements FaceRepositoryPort {
  constructor(private readonly prisma: PrismaService) {}

  async create(data: CreateFaceData): Promise<FaceEntity> {
    const created = await this.prisma.face.create({
      data: {
        imageUrl: data.imageUrl,
        description: data.description,
        incidentReportId: data.incidentReportId,
      },
    });

    return new FaceEntity(
      created.id,
      created.imageUrl,
      created.description,
      created.incidentReportId,
      created.createdAt,
    );
  }
}
