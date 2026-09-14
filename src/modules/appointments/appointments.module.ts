import { Module } from '@nestjs/common';
import { AppointmentsController } from './infrastructure/appointments.controller';
import { PrismaAppointmentRepository } from './infrastructure/prisma-appointment.repository';
import { AppointmentRepositoryPort } from './domain/ports/appointment.repository.port';
import { CreateAppointmentUseCase } from './application/use-cases/create-appointment.use-case';
import { ListUserAppointmentsUseCase } from './application/use-cases/list-user-appointments.use-case';
import { UpdateAppointmentUseCase } from './application/use-cases/update-appointment.use-case';
import { DeleteAppointmentUseCase } from './application/use-cases/delete-appointment.use-case';

@Module({
  controllers: [AppointmentsController],
  providers: [
    {
      provide: AppointmentRepositoryPort,
      useClass: PrismaAppointmentRepository,
    },
    CreateAppointmentUseCase,
    ListUserAppointmentsUseCase,
    UpdateAppointmentUseCase,
    DeleteAppointmentUseCase,
  ],
})
export class AppointmentsModule {}
