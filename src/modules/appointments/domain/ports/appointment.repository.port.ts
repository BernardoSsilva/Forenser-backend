import { AppointmentEntity } from '../entities/appointment.entity';

export interface CreateAppointmentData {
  requesterName: string;
  scheduledDate: Date;
  scheduledTime: string;
  userId: string;
}

export interface UpdateAppointmentData {
  requesterName?: string;
  scheduledDate?: Date;
  scheduledTime?: string;
}

export abstract class AppointmentRepositoryPort {
  abstract create(data: CreateAppointmentData): Promise<AppointmentEntity>;
  abstract findById(id: string): Promise<AppointmentEntity | null>;
  abstract findAllByUserId(userId: string): Promise<AppointmentEntity[]>;
  abstract update(
    id: string,
    data: UpdateAppointmentData,
  ): Promise<AppointmentEntity>;
  abstract delete(id: string): Promise<void>;
}
