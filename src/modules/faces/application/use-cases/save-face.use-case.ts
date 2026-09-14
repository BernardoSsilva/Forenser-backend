import {
  ConflictException,
  ForbiddenException,
  Injectable,
} from '@nestjs/common';
import { FaceRepositoryPort } from '../../domain/ports/face.repository.port';
import { IncidentReportRepositoryPort } from '../../../incident-reports/domain/ports/incident-report.repository.port';
import { IncidentReportNotFoundError } from '../../../incident-reports/domain/errors/incident-report.errors';
import { SaveFaceDto } from '../dtos/save-face.dto';

@Injectable()
export class SaveFaceUseCase {
  constructor(
    private readonly faceRepository: FaceRepositoryPort,
    private readonly incidentReportRepository: IncidentReportRepositoryPort,
  ) {}

  async execute(userId: string, dto: SaveFaceDto) {
    const report = await this.incidentReportRepository.findById(
      dto.incidentReportId,
    );

    if (!report || report.userId !== userId) {
      throw new ForbiddenException(new IncidentReportNotFoundError().message);
    }

    if (report.face) {
      throw new ConflictException(
        'Este boletim já possui um retrato falado vinculado.',
      );
    }

    return this.faceRepository.create({
      imageUrl: dto.imageUrl,
      description: dto.description,
      incidentReportId: dto.incidentReportId,
    });
  }
}
