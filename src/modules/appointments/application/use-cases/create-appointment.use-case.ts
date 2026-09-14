import { Injectable } from '@nestjs/common';
import { AppointmentRepositoryPort } from '../../domain/ports/appointment.repository.port';
import { CreateAppointmentDto } from '../dtos/create-appointment.dto';

@Injectable()
export class CreateAppointmentUseCase {
  constructor(private readonly repository: AppointmentRepositoryPort) {}

  execute(userId: string, dto: CreateAppointmentDto) {
    return this.repository.create({
      userId,
      requesterName: dto.requesterName,
      scheduledDate: dto.scheduledDate,
      scheduledTime: dto.scheduledTime,
    });
  }
}
