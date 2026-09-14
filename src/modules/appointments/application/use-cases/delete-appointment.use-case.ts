import { ForbiddenException, Injectable } from '@nestjs/common';
import { AppointmentRepositoryPort } from '../../domain/ports/appointment.repository.port';
import { AppointmentNotFoundError } from '../../domain/errors/appointment.errors';

@Injectable()
export class DeleteAppointmentUseCase {
  constructor(private readonly repository: AppointmentRepositoryPort) {}

  async execute(userId: string, appointmentId: string) {
    const appointment = await this.repository.findById(appointmentId);
    if (!appointment || appointment.userId !== userId) {
      throw new ForbiddenException(new AppointmentNotFoundError().message);
    }

    await this.repository.delete(appointmentId);
  }
}
