import { Module } from '@nestjs/common';
import { IncidentReportsModule } from '../incident-reports/incident-reports.module';
import { FacesController } from './infrastructure/faces.controller';
import { OpenAiImageGenerationProvider } from './infrastructure/openai-image-generation.provider';
import { PrismaFaceRepository } from './infrastructure/prisma-face.repository';
import { ImageGenerationPort } from './domain/ports/image-generation.port';
import { FaceRepositoryPort } from './domain/ports/face.repository.port';
import { FacePromptBuilder } from './domain/services/face-prompt-builder.service';
import { GenerateFaceImageUseCase } from './application/use-cases/generate-face-image.use-case';
import { SaveFaceUseCase } from './application/use-cases/save-face.use-case';

@Module({
  imports: [IncidentReportsModule],
  controllers: [FacesController],
  providers: [
    { provide: ImageGenerationPort, useClass: OpenAiImageGenerationProvider },
    { provide: FaceRepositoryPort, useClass: PrismaFaceRepository },
    FacePromptBuilder,
    GenerateFaceImageUseCase,
    SaveFaceUseCase,
  ],
})
export class FacesModule {}
