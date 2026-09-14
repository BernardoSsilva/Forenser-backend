import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import configuration from './shared/config/configuration';
import { validate } from './shared/config/env.validation';
import { PrismaModule } from './shared/database/prisma.module';
import { AuthModule } from './modules/auth/auth.module';
import { UsersModule } from './modules/users/users.module';
import { IncidentReportsModule } from './modules/incident-reports/incident-reports.module';
import { FacesModule } from './modules/faces/faces.module';
import { ComplaintsModule } from './modules/complaints/complaints.module';
import { AppointmentsModule } from './modules/appointments/appointments.module';

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      load: [configuration],
      validate,
    }),
    PrismaModule,
    AuthModule,
    UsersModule,
    IncidentReportsModule,
    FacesModule,
    ComplaintsModule,
    AppointmentsModule,
  ],
})
export class AppModule {}
