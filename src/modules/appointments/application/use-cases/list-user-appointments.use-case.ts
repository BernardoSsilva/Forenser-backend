import { Injectable } from '@nestjs/common';
import { AppointmentRepositoryPort } from '../../domain/ports/appointment.repository.port';

@Injectable()
export class ListUserAppointmentsUseCase {
  constructor(private readonly repository: AppointmentRepositoryPort) {}

  execute(userId: string) {
    return this.repository.findAllByUserId(userId);
  }
}
