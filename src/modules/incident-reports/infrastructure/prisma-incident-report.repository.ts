import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../../shared/database/prisma.service';
import {
  CreateIncidentReportData,
  IncidentReportRepositoryPort,
} from '../domain/ports/incident-report.repository.port';
import { IncidentReportEntity } from '../domain/entities/incident-report.entity';
import {
  IncidentReport as PrismaIncidentReport,
  Face as PrismaFace,
} from '@prisma/client';

type IncidentReportWithFace = PrismaIncidentReport & {
  face: PrismaFace | null;
};

@Injectable()
export class PrismaIncidentReportRepository implements IncidentReportRepositoryPort {
  constructor(private readonly prisma: PrismaService) {}

  async create(data: CreateIncidentReportData): Promise<IncidentReportEntity> {
    const created = await this.prisma.incidentReport.create({
      data: {
        type: data.type,
        userId: data.userId,
        occurredAt: data.occurredAt,
        occurredTime: data.occurredTime,
        locationType: data.locationType,
        address: data.address,
        informantName: data.informantName,
        narrative: data.narrative,
        driverName: data.driverName,
        vehiclesInvolved: data.vehiclesInvolved,
        propertyTaken: data.propertyTaken,
        stolenItems: data.stolenItems,
        involvedViolence: data.involvedViolence,
        victimName: data.victimName,
      },
      include: { face: true },
    });
    return this.toEntity(created);
  }

  async findById(id: string): Promise<IncidentReportEntity | null> {
    const report = await this.prisma.incidentReport.findUnique({
      where: { id },
      include: { face: true },
    });
    return report ? this.toEntity(report) : null;
  }

  async findAllByUserId(userId: string): Promise<IncidentReportEntity[]> {
    const reports = await this.prisma.incidentReport.findMany({
      where: { userId },
      include: { face: true },
      orderBy: { createdAt: 'desc' },
    });
    return reports.map((report) => this.toEntity(report));
  }

  private toEntity(report: IncidentReportWithFace): IncidentReportEntity {
    return new IncidentReportEntity(
      report.id,
      report.type,
      report.occurredAt,
      report.occurredTime,
      report.locationType,
      report.address,
      report.informantName,
      report.narrative,
      report.userId,
      report.createdAt,
      report.driverName,
      report.vehiclesInvolved,
      report.propertyTaken,
      report.stolenItems,
      report.involvedViolence,
      report.victimName,
      report.face
        ? {
            id: report.face.id,
            imageUrl: report.face.imageUrl,
            description: report.face.description,
          }
        : null,
    );
  }
}
