import { Module } from '@nestjs/common';
import { ComplaintsController } from './infrastructure/complaints.controller';
import { PrismaComplaintRepository } from './infrastructure/prisma-complaint.repository';
import { ComplaintRepositoryPort } from './domain/ports/complaint.repository.port';
import { CreateComplaintUseCase } from './application/use-cases/create-complaint.use-case';

@Module({
  controllers: [ComplaintsController],
  providers: [
    { provide: ComplaintRepositoryPort, useClass: PrismaComplaintRepository },
    CreateComplaintUseCase,
  ],
})
export class ComplaintsModule {}
