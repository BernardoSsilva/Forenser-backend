import { ForbiddenException, Injectable } from '@nestjs/common';
import { AppointmentRepositoryPort } from '../../domain/ports/appointment.repository.port';
import { AppointmentNotFoundError } from '../../domain/errors/appointment.errors';
import { UpdateAppointmentDto } from '../dtos/update-appointment.dto';

@Injectable()
export class UpdateAppointmentUseCase {
  constructor(private readonly repository: AppointmentRepositoryPort) {}

  async execute(
    userId: string,
    appointmentId: string,
    dto: UpdateAppointmentDto,
  ) {
    const appointment = await this.repository.findById(appointmentId);
    if (!appointment || appointment.userId !== userId) {
      throw new ForbiddenException(new AppointmentNotFoundError().message);
    }

    return this.repository.update(appointmentId, dto);
  }
}
