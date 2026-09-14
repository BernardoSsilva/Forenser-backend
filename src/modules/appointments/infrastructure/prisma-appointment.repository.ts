import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../../shared/database/prisma.service';
import {
  AppointmentRepositoryPort,
  CreateAppointmentData,
  UpdateAppointmentData,
} from '../domain/ports/appointment.repository.port';
import { AppointmentEntity } from '../domain/entities/appointment.entity';
import { Appointment as PrismaAppointment } from '@prisma/client';

@Injectable()
export class PrismaAppointmentRepository implements AppointmentRepositoryPort {
  constructor(private readonly prisma: PrismaService) {}

  async create(data: CreateAppointmentData): Promise<AppointmentEntity> {
    const created = await this.prisma.appointment.create({ data });
    return this.toEntity(created);
  }

  async findById(id: string): Promise<AppointmentEntity | null> {
    const appointment = await this.prisma.appointment.findUnique({
      where: { id },
    });
    return appointment ? this.toEntity(appointment) : null;
  }

  async findAllByUserId(userId: string): Promise<AppointmentEntity[]> {
    const appointments = await this.prisma.appointment.findMany({
      where: { userId },
      orderBy: { scheduledDate: 'asc' },
    });
    return appointments.map((appointment) => this.toEntity(appointment));
  }

  async update(
    id: string,
    data: UpdateAppointmentData,
  ): Promise<AppointmentEntity> {
    const updated = await this.prisma.appointment.update({
      where: { id },
      data,
    });
    return this.toEntity(updated);
  }

  async delete(id: string): Promise<void> {
    await this.prisma.appointment.delete({ where: { id } });
  }

  private toEntity(appointment: PrismaAppointment): AppointmentEntity {
    return new AppointmentEntity(
      appointment.id,
      appointment.requesterName,
      appointment.scheduledDate,
      appointment.scheduledTime,
      appointment.userId,
      appointment.createdAt,
    );
  }
}
