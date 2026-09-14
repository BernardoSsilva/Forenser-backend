import { Injectable } from '@nestjs/common';
import { ComplaintRepositoryPort } from '../../domain/ports/complaint.repository.port';
import { CreateComplaintDto } from '../dtos/create-complaint.dto';

@Injectable()
export class CreateComplaintUseCase {
  constructor(private readonly repository: ComplaintRepositoryPort) {}

  execute(dto: CreateComplaintDto) {
    return this.repository.create(dto);
  }
}
