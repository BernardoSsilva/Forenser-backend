import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../../shared/database/prisma.service';
import {
  ComplaintRepositoryPort,
  CreateComplaintData,
} from '../domain/ports/complaint.repository.port';
import { ComplaintEntity } from '../domain/entities/complaint.entity';

@Injectable()
export class PrismaComplaintRepository implements ComplaintRepositoryPort {
  constructor(private readonly prisma: PrismaService) {}

  async create(data: CreateComplaintData): Promise<ComplaintEntity> {
    const created = await this.prisma.complaint.create({ data });
    return new ComplaintEntity(
      created.id,
      created.reporterName,
      created.location,
      created.description,
      created.createdAt,
    );
  }
}
